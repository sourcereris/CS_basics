using System;
using System.Collections.Generic;
using System.Text;


class InputManager
{
    private GameLogic _gameLogic;
    public InputManager(GameLogic gl) 
    {
        _gameLogic = gl;
    }
    public void ManageKey(ConsoleKeyInfo consoleKeyInfo) 
    {
        switch (consoleKeyInfo.Key)
        {
            case ConsoleKey.Spacebar:
                _gameLogic.AddScore();
                break;
            default:
                Console.WriteLine($"Unhandled key: {consoleKeyInfo.Key}");
                break;
        }
    } 
}