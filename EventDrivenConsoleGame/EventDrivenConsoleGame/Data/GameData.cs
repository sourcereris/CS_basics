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

    public static GameState currentState;

    public static bool IsPlaying = true;

    public const int WIDTH = 120;
    public const int HEIGHT = 30;
}
