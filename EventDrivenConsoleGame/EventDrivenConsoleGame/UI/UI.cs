using System;
using System.Collections.Generic;
using System.Text;

static class UI 
{
    public static void RenderUI() 
    {
        Utils_UI.ClearBackBuffer();

        Utils_UI.DrawBox(0, 0, GameData.WIDTH - 1, GameData.HEIGHT - 1);

        Console.ForegroundColor = ConsoleColor.Yellow;
        string scoreText = $"Score: {GameData.Score}";
        Utils_UI.DrawText(2, 2, scoreText);
        Utils_UI.DrawBox(1, 1, scoreText.Length + 1, 2);

        //Console.ForegroundColor = ConsoleColor.White;

        Utils_UI.Render();
    }
}