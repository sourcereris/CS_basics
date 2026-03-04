const int Main_FPS = 30;
const int UI_FPS = 30;
double LogicFrameTime = 1.0 / Main_FPS;
double UIFrameTime = 1.0 / UI_FPS;

GameLogic gameLogic = new GameLogic();
InputManager inputManager = new InputManager(gameLogic);

SetupConsole();

while (GameData.IsPlaying) 
{
    KTime.Update(); // calculates Deltatime

    if (KTime.ElapsedTime_Main >= LogicFrameTime)
    {
        while (Console.KeyAvailable) 
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            inputManager.ManageKey(keyInfo);

        }
        KTime.ConsumeElapsedTime_Main(LogicFrameTime);    
    }
    
    gameLogic.TimeIncrement();
    
    if (KTime.ElapsedTime_UI >= UIFrameTime) 
    {
        UI.RenderUI();
        KTime.ConsumeElapsedTime_UI(UIFrameTime);
    }

    System.Threading.Thread.Sleep(1);
}

void SetupConsole() 
{
    GameData.currentState = GameState.MainMenu;
    Console.CursorVisible = false;
    Console.Clear();
}