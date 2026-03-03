using System;
using System.Collections.Generic;
using System.Text;

static class UI 
{
    public static void RenderScore() 
    {
        Console.WriteLine($"Score: {GameData.Score}");
        Console.WriteLine($"Time: {GameData.time:F1}");
        Console.SetCursorPosition( 0, 0 );
    }
}