using System;
using System.Collections.Generic;
using System.Text;

static class Utils_UI 
{
    public static char[,] _backBuffer = new char[GameData.WIDTH, GameData.HEIGHT];


    public static void DrawBox(int x, int y, int width, int height) 
    {
        _backBuffer[x, y] = '┌';
        _backBuffer[x + width, y] = '┐';
        _backBuffer[x, y + height] = '└';
        _backBuffer[x + width, y + height] = '┘';

        // Top and Bottom Walls
        for (int i = 1; i < width; i++)
        {
            _backBuffer[x + i, y] = '─';
            _backBuffer[x + i, y + height] = '─';
        }

        // Side Walls
        for (int i = 1; i < height; i++)
        {
            _backBuffer[x, y + i] = '│';
            _backBuffer[x + width, y + i] = '│';
        }
    }

    public static void DrawText(int x, int y, string text) 
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (x + i < GameData.WIDTH) // Prevent overflow
            {
                _backBuffer[x + i, y] = text[i];
            }
        }
    }

    public static void Render() //uses string builder for better performance
    {
        for (int y = 0; y < GameData.HEIGHT; y++)
        {

            Console.SetCursorPosition(0, y); //works as /n

            StringBuilder row = new StringBuilder(GameData.WIDTH);

            for (int x = 0; x < GameData.WIDTH; x++)
            {
                row.Append(_backBuffer[x, y]);
            }

            // row WITHOUT /n
            Console.Write(row.ToString());
        }
    }

    public static void ClearBackBuffer() 
    {
        for (int x = 0; x < GameData.WIDTH; x++)
        {
            for (int y = 0; y < GameData.HEIGHT; y++)
            {
                _backBuffer[x, y] = ' ';
            }
        }
    }
}