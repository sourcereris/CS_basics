using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Numerics;
using Windows.System;
using Windows.UI;

namespace App1
{
    public sealed partial class MainWindow : Window
    {
        // State
        private Vector2 origin = new Vector2(520, 380);
        private Vector2 a = new Vector2(240, -140); // tip relative to origin
        private Vector2 b = new Vector2(260, 40);

        private float zoom = 1.0f;
        private const float MinZoom = 0.25f;
        private const float MaxZoom = 4.0f;
        private const float GridStep = 25.0f;
        private const float HandleRadius = 10.0f;
        private bool draggingA, draggingB, draggingOrigin;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.Title = "Vector Projection Playground";

            // Give keyboard focus to the canvas so KeyDown fires
            Canvas.Loaded += (_, __) => Canvas.Focus(FocusState.Programmatic);
            this.Activated += (_, __) => Canvas.Invalidate();
        }

        // ===== Keyboard =====
        private void Canvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.R)
            {
                origin = new Vector2(520, 380);
                a = new Vector2(240, -140);
                b = new Vector2(260, 40);
                zoom = 1.0f;
                Canvas.Invalidate();
            }
            else if (e.Key == VirtualKey.G)
            {
                GridCheck.IsChecked = !(GridCheck.IsChecked ?? true);
                Canvas.Invalidate();
            }
            else if (e.Key == VirtualKey.S)
            {
                SnapCheck.IsChecked = !(SnapCheck.IsChecked ?? false);
            }
            else if (e.Key == VirtualKey.N)
            {
                NormalizeCheck.IsChecked = !(NormalizeCheck.IsChecked ?? false);
                Canvas.Invalidate();
            }
            else if (e.Key == VirtualKey.P)
            {
                ModeBox.SelectedIndex = 1 - ModeBox.SelectedIndex;
                Canvas.Invalidate();
            }
            else if (e.Key == VirtualKey.Add)
            {
                ZoomAtCenter(1.1f);
            }
            else if (e.Key == VirtualKey.Subtract)
            {
                ZoomAtCenter(1 / 1.1f);
            }
        }

        private void Canvas_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            var pt = e.GetCurrentPoint(Canvas);
            int delta = pt.Properties.MouseWheelDelta; // multiples of 120
            float factor = (float)Math.Pow(1.1, delta / 120.0);
            var pivot = new Vector2((float)pt.Position.X, (float)pt.Position.Y);
            ZoomAt(factor, pivot);
        }

        private void ZoomAtCenter(float factor)
        {
            var size = new Vector2((float)Canvas.Size.Width, (float)Canvas.Size.Height);
            var center = new Vector2(size.X * 0.5f, size.Y * 0.5f);
            ZoomAt(factor, center);
        }

        private void ZoomAt(float factor, Vector2 screenPivot)
        {
            float oldZoom = zoom;
            zoom = Math.Clamp(zoom * factor, MinZoom, MaxZoom);
            factor = zoom / oldZoom;
            // Keep pivot stable by moving origin accordingly
            origin = screenPivot + (origin - screenPivot) * factor;
            a *= factor;
            b *= factor;
            Canvas.Invalidate();
        }

        private void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var pt = e.GetCurrentPoint(Canvas);
            var p = new Vector2((float)pt.Position.X, (float)pt.Position.Y);
            bool alt = e.KeyModifiers.HasFlag(VirtualKeyModifiers.Menu);
            bool middle = pt.Properties.IsMiddleButtonPressed;

            if (alt || middle)
            {
                draggingOrigin = true;
            }
            else
            {
                if (Near(p, origin + a)) draggingA = true;
                else if (Near(p, origin + b)) draggingB = true;
                else if (Near(p, origin)) draggingOrigin = true;
            }
            Canvas.CapturePointer(e.Pointer);
        }

        private void Canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pt = e.GetCurrentPoint(Canvas);
            var p = new Vector2((float)pt.Position.X, (float)pt.Position.Y);
            if (draggingA)
            {
                var snapped = MaybeSnap(p);
                a = snapped - origin;
                Canvas.Invalidate();
            }
            else if (draggingB)
            {
                var snapped = MaybeSnap(p);
                b = snapped - origin;
                Canvas.Invalidate();
            }
            else if (draggingOrigin)
            {
                origin = MaybeSnap(p);
                Canvas.Invalidate();
            }
        }

        private void Canvas_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            draggingA = draggingB = draggingOrigin = false;
            Canvas.ReleasePointerCaptures();
        }

        private Vector2 MaybeSnap(Vector2 p)
        {
            if (!(SnapCheck.IsChecked ?? false)) return p;
            float step = GridStep * zoom;
            p.X = (float)Math.Round(p.X / step) * step;
            p.Y = (float)Math.Round(p.Y / step) * step;
            return p;
        }

        private bool Near(Vector2 p, Vector2 q)
        {
            float r = HandleRadius + 3;
            return Vector2.DistanceSquared(p, q) <= r * r;
        }

        // ===== Draw =====
        private void Canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            // No GPU resources to preload; just redraw.
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var d = args.DrawingSession;
            var size = new Vector2((float)sender.Size.Width, (float)sender.Size.Height);

            if (GridCheck.IsChecked ?? true)
            {
                DrawGrid(d, size, origin, GridStep * zoom, Colors.DimGray, Colors.Gray);
            }

            // Axes
            d.DrawLine(new Vector2(0, origin.Y), new Vector2(size.X, origin.Y), Colors.DarkGray, 1);
            d.DrawLine(new Vector2(origin.X, 0), new Vector2(origin.X, size.Y), Colors.DarkGray, 1);

            // Vectors
            var A = origin + a;
            var B = origin + b;
            DrawArrow(d, origin, A, Colors.White, 3);
            DrawArrow(d, origin, B, ColorFromHex("#C0C0C0"), 2);

            // Projection/rejection depending on mode
            bool projAontoB = ModeBox.SelectedIndex == 0;
            Vector2 from = projAontoB ? a : b;
            Vector2 onto = projAontoB ? b : a;
            bool normalizeOnto = (NormalizeCheck.IsChecked ?? false);

            Vector2 proj = VectorMath.ProjectOnto(from, onto, normalizeOnto);
            Vector2 rej = from - proj;

            // Draw projection + rejection
            var foot = origin + proj;         // foot of perpendicular on onto-axis
            var tipFrom = origin + from;      // tip of from-vector

            DrawArrow(d, origin, foot, Colors.Lime, 3);            // projection along onto
            DrawArrow(d, foot, tipFrom, Colors.Orange, 3);         // rejection from foot to tip
            DrawRightAngleMarker(d, foot, origin + onto, tipFrom, Colors.Orange);

            // Handles
            DrawHandle(d, origin, Colors.CornflowerBlue);
            DrawHandle(d, A, Colors.White);
            DrawHandle(d, B, ColorFromHex("#C0C0C0"));

            // Text overlay with math
            var (dot, ang) = VectorMath.DotAndAngleDeg(a, b);
            var (lenA, lenB) = (a.Length(), b.Length());
            var (lenP, lenR) = (proj.Length(), rej.Length());

            var modeLabel = projAontoB ? "proj_b(a)" : "proj_a(b)";

            string formula = normalizeOnto? $"proj = (from · û) ûrej = from − proj"
                : $"proj = ((from · onto) / |onto|²) ontorej = from − proj";

            string values =
                $"a = ({a.X:0.##}, {a.Y:0.##})  |a|={lenA:0.##}" +
                $"b = ({b.X:0.##}, {b.Y:0.##})  |b|={lenB:0.##}" +
                $"a·b = {dot:0.###}    angle(a,b) = {ang:0.##}°" +
                $"{modeLabel}: |proj|={lenP:0.##}, |rej|={lenR:0.##}";

            var layout = new CanvasTextFormat { FontSize = 16, WordWrapping = CanvasWordWrapping.WholeWord };
            d.DrawText("Vector playground", new Vector2(16, 70), Colors.White);
            d.DrawText(formula, new Vector2(16, 96), Colors.LightGreen, layout);
            d.DrawText(values, new Vector2(16, 160), Colors.LightGray, layout);
        }

        private void DrawGrid(CanvasDrawingSession d, Vector2 size, Vector2 origin, float step, Color fine, Color bold)
        {
            // fine grid
            for (float x = origin.X % step; x < size.X; x += step)
                d.DrawLine(x, 0, x, size.Y, fine, 1);
            for (float y = origin.Y % step; y < size.Y; y += step)
                d.DrawLine(0, y, size.X, y, fine, 1);

            // bold axes every 4 steps
            float big = step * 4f;
            for (float x = origin.X % big; x < size.X; x += big)
                d.DrawLine(x, 0, x, size.Y, bold, 1);
            for (float y = origin.Y % big; y < size.Y; y += big)
                d.DrawLine(0, y, size.X, y, bold, 1);
        }

        private void DrawHandle(CanvasDrawingSession d, Vector2 p, Color c)
        {
            d.FillCircle(p, HandleRadius, c);
            d.DrawCircle(p, HandleRadius, Colors.Black, 1);
        }

        private void DrawArrow(CanvasDrawingSession d, Vector2 from, Vector2 to, Color c, float thickness)
        {
            d.DrawLine(from, to, c, thickness);

            Vector2 dir = to - from;
            if (dir.LengthSquared() < 1e-4f) return;
            dir = Vector2.Normalize(dir);
            var left = Rotate(dir, +150f * MathF.PI / 180f);
            var right = Rotate(dir, -150f * MathF.PI / 180f);
            float head = 12 * thickness;
            d.DrawLine(to, to + left * head, c, thickness);
            d.DrawLine(to, to + right * head, c, thickness);
        }

        private Vector2 Rotate(Vector2 v, float radians)
        {
            float cs = MathF.Cos(radians), sn = MathF.Sin(radians);
            return new Vector2(v.X * cs - v.Y * sn, v.X * sn + v.Y * cs);
        }

        private void DrawRightAngleMarker(CanvasDrawingSession d, Vector2 foot, Vector2 along, Vector2 tip, Color c)
        {
            // tiny square to indicate perpendicular at the foot point
            var dirAlong = along - origin; // onto direction from origin
            if (dirAlong.LengthSquared() < 1e-6f) return;
            dirAlong = Vector2.Normalize(dirAlong);
            var dirPerp = new Vector2(-dirAlong.Y, dirAlong.X);
            float s = 12 * MathF.Sqrt(zoom);
            var p1 = foot;
            var p2 = foot + dirAlong * s;
            var p3 = p2 + dirPerp * s;
            var p4 = foot + dirPerp * s;
            d.DrawLine(p1, p2, c, 2);
            d.DrawLine(p2, p3, c, 2);
            d.DrawLine(p3, p4, c, 2);
        }

        private static Color ColorFromHex(string hex)
        {
            if (hex.StartsWith("#")) hex = hex.Substring(1);
            byte a = 0xFF;
            if (hex.Length == 8)
            {
                a = Convert.ToByte(hex.Substring(0, 2), 16);
                hex = hex.Substring(2);
            }
            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
    }
}