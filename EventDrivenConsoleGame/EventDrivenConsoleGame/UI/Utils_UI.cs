using System;
using System.Collections.Generic;
using System.Text;

static class Utils_UI 
{
    public static char[,] _backBuffer = new char[Info.WIDTH, Info.HEIGHT];


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

    public static void Render()
    {
        // Write line by line to bypass auto-wrap and newline bugs
        for (int y = 0; y < Info.HEIGHT; y++)
        {
            Console.SetCursorPosition(0, y);

            StringBuilder row = new StringBuilder(Info.WIDTH);
            for (int x = 0; x < Info.WIDTH; x++)
            {
                // THE MAGIC FIX: Do not write to the absolute bottom-right corner.
                // This prevents the console from automatically scrolling down.
                //if (x == Info.WIDTH - 1 && y == Info.HEIGHT - 1)
                //{
                //    // Just leave it blank/skip it
                //    continue;
                //}

                row.Append(_backBuffer[x, y]);
            }

            // Print the row WITHOUT a newline character
            Console.Write(row.ToString());
        }
    }

    public static void ClearBackBuffer() 
    {
        for (int x = 0; x < Info.WIDTH; x++)
        {
            for (int y = 0; y < Info.HEIGHT; y++)
            {
                _backBuffer[x, y] = ' ';
            }
        }
    }
}