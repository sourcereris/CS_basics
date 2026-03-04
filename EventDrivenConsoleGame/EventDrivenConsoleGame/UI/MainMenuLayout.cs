public class MainMenuLayout : Layout
{
    //buttons
    private Button _startBtn;
    private Button _exitBtn;

    public MainMenuLayout()
    {
        Initialize();
    }
    void Initialize() 
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

        // Set the start button as the default selected option
        _startBtn.IsSelected = true;
    }
    public override void Render() 
    {
        _startBtn.Render();
        _exitBtn.Render();
    }

    private void SelectButton(int vertical) 
    {
    }
    
}