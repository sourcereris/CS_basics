public class Button
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public string Text { get; private set; }
    public bool IsSelected { get; set; }

    public Button(string text, int x, int y, int width, int height)
    {
        Text = text;
        X = x;
        Y = y;
        Width = width;
        Height = height;
        IsSelected = false;
    }
    public void Render()
    {
        Utils_UI.DrawBox(X, Y, Width,  Height);

        int textX = X + (Width - Text.Length) / 2;
        Utils_UI.DrawText(textX, Y + Height / 2, Text);

        Console.ResetColor();
        
        if(IsSelected)
        {
            Utils_UI.SetPixel(textX - 3, Y+1, '>');
        }
    }
}
