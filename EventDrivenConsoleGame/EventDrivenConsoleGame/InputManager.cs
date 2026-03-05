using System.Numerics;
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
                InputEvents.Key_Spacebar_Event?.Invoke();
                break;
            case ConsoleKey.W:
                InputEvents.Key_W_Event?.Invoke(new Vector2(0, 1));
                break;
            case ConsoleKey.A:
                    InputEvents.Key_A_Event?.Invoke(new Vector2(-1, 0));
                break;
            case ConsoleKey.D:
                    InputEvents.Key_D_Event?.Invoke(new Vector2(1, 0));
                break;
            case ConsoleKey.S:
                    InputEvents.Key_S_Event?.Invoke(new Vector2(0, -1));
                break;
            default:
                break;
        }
    } 
}