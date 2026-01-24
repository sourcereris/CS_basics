public struct Vector2 
{
    public float X { get; set; }
    public float Y { get; set; }

    public float Length
    {
        get
        {
            return MathF.Sqrt(X * X + Y * Y);
        }
    } 

    public Vector2 Normalized 
    {
        get 
        {
            if (Length < float.Epsilon) return new Vector2 { X = 0, Y = 0 };
            return new Vector2 {X = X/Length, Y = Y/Length };
        }
    }

    public static float Dot(Vector2 a, Vector2 b) 
    {
        return a.X * b.X + a.Y * b.Y;
    }
}

