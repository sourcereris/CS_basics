public class MainMenuLayout : Layout
{
    //buttons
    private Button _startBtn;
    private Button _exitBtn;
    private ButtonGraph _navGraph;

    public MainMenuLayout()
    {
        Initialize();
    }
    void Initialize() 
    {
        SetupButtons();
        SetupButtonGraph();
    }

    private void SetupButtons() 
    {
        string startTxt = "Start Game";
        string exitTxt = "Exit Game";

        int btnWidth = Math.Max(startTxt.Length, exitTxt.Length) + 4;
        int btnHeight = 3;

        // X coordinate (assuming centerX and centerY come from the base Layout class)
        int xPos = centerX - (btnWidth / 2);

        // Construct the actual Button objects
        _startBtn = new Button(startTxt, xPos, centerY - 3, btnWidth, btnHeight);
        _exitBtn = new Button(exitTxt, xPos, centerY, btnWidth, btnHeight);

        _startBtn.OnClick = LoadGameScene;
        _exitBtn.OnClick = QuitGame;
    }
    
    private void SetupButtonGraph() 
    {
        UINode startNode = new UINode(_startBtn);
        UINode exitNode = new UINode(_exitBtn);

        // 3. Link the Graph (Start is above Exit)
        startNode.Down = exitNode;
        startNode.Up = exitNode;

        exitNode.Down = startNode;
        exitNode.Up = startNode;

        // 4. Initialize the Graph Manager
        _navGraph = new ButtonGraph();
        _navGraph.SetStartNode(startNode);

        // 5. Subscribe to your Input Events!
        InputEvents.Key_W_Event += _navGraph.OnNavigate;
        InputEvents.Key_S_Event += _navGraph.OnNavigate;
        InputEvents.Key_A_Event += _navGraph.OnNavigate;
        InputEvents.Key_D_Event += _navGraph.OnNavigate;
        InputEvents.Key_Spacebar_Event += _navGraph.OnSubmit;
    }

    public override void Render()
    {
        _startBtn.Render();
        _exitBtn.Render();
    }

    private void LoadGameScene() 
    {
        GameData.currentState = GameState.Playing;
    }

    private void QuitGame() 
    {
        GameData.IsPlaying = false;
    }
}