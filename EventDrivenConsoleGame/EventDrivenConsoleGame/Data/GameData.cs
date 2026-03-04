using System;
using System.Collections.Generic;
using System.Text;

enum GameState
{
    MainMenu,
    Playing,
    GameOver
}
 
static class GameData
{
    public static int Score = 0;
    public static double time = 0;

    public const int WIDTH = 120;
    public const int HEIGHT = 30;
}

struct Layout
{
    public int X;
    public int Y;
    public Layout(int x, int y)
    {
        X = x;
        Y = y;
    }
}