using System;
using System.Numerics;

namespace App1
{
    public static class VectorMath
    {
        public static Vector2 ProjectOnto(in Vector2 from, in Vector2 onto, bool normalizeOnto)
        {
            if (normalizeOnto)
            {
                var u = SafeNormalize(onto);
                return Vector2.Dot(from, u) * u; // proj = (a·û) û
            }
            float denom = Vector2.Dot(onto, onto);
            if (denom < 1e-9f) return Vector2.Zero;
            return (Vector2.Dot(from, onto) / denom) * onto; // proj = ((a·b)/|b|^2) b
        }

        public static (float dot, float angleDeg) DotAndAngleDeg(in Vector2 a, in Vector2 b)
        {
            float dot = Vector2.Dot(a, b);
            float la = a.Length();
            float lb = b.Length();
            float cos = (la < 1e-9f || lb < 1e-9f) ? 1f : Clamp(dot / (la * lb), -1f, 1f);
            float ang = (float)(Math.Acos(cos) * 180.0 / Math.PI);
            return (dot, ang);
        }

        public static Vector2 SafeNormalize(in Vector2 v)
        {
            float len = v.Length();
            return len < 1e-9f ? Vector2.Zero : v / len;
        }

        private static float Clamp(float x, float lo, float hi) => x < lo ? lo : (x > hi ? hi : x);
    }
}