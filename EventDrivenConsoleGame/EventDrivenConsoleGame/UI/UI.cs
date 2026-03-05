using System;
using System.Collections.Generic;
using System.Text;

static class UI 
{
    static Layout mainMenu = new MainMenuLayout();
    static Layout gameLayout = new GameLayout();
    public static void RenderUI() 
    {
        Utils_UI.ClearBackBuffer();
        Utils_UI.DrawBox(0, 0, GameData.WIDTH - 1, GameData.HEIGHT - 1); // Screen Outline

        switch (GameData.currentState)
        {
            case GameState.MainMenu:
                mainMenu.Render();
                break;
            case GameState.Playing:
                Console.ForegroundColor = ConsoleColor.Yellow;
                gameLayout.Render();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }

        Utils_UI.RenderBuffer();
    }
}