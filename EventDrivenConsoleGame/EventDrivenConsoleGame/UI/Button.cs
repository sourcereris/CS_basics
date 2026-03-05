public class Button
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public string Text { get; private set; }
    public bool IsSelected { get; set; }
    public bool markOnLeft { get; set; } = true;
    public Action OnClick { get; set; }

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
        Utils_UI.DrawBox(X, Y, Width, Height);

        int textY = Y + Height / 2;
        int textX = X + (Width - Text.Length) / 2;

        Utils_UI.DrawText(textX, textY, Text);

        Console.ResetColor();

        if (IsSelected)
        {
            if (markOnLeft)
            {
                Utils_UI.SetPixel(X - 1, textY, '>');
            }
            else
            {
                Utils_UI.SetPixel(X + Width, textY, '<');
            }
        }
    }
    public void Click() 
    {
        OnClick?.Invoke();
    }
}
