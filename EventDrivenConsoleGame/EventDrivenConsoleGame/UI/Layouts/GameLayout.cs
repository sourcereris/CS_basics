using System;
using System.Collections.Generic;
using System.Text;

public class GameLayout : Layout
{
    private Button _btn1;
    private Button _btn2;
    private Button _btn3;
    private Button _btn4;
    private Button _btn5;
    private Button _btn6;

    private ButtonGraph _navGraph;

    public GameLayout()
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
        int btnWidth = 12;
        int btnHeight = 3;

        int startX = 1;
        int startY = 1;

        int pading = 1;
        int stepY = btnHeight + pading;

        //Initialize buttons by multiplying the step by their index
        _btn1 = new Button(" ", startX, startY + (stepY * 0), btnWidth, btnHeight);
        _btn2 = new Button(" ", startX, startY + (stepY * 1), btnWidth, btnHeight);
        _btn3 = new Button(" ", startX, startY + (stepY * 2), btnWidth, btnHeight);
        _btn4 = new Button(" ", startX, startY + (stepY * 3), btnWidth, btnHeight);
        _btn5 = new Button(" ", startX, startY + (stepY * 4), btnWidth, btnHeight);
        _btn6 = new Button(" ", startX, startY + (stepY * 5), btnWidth, btnHeight);
    }

    private void SetupButtonGraph()
    {
        // 1. Instantiate the nodes
        UINode n1 = new UINode(_btn1);
        UINode n2 = new UINode(_btn2);
        UINode n3 = new UINode(_btn3);
        UINode n4 = new UINode(_btn4);
        UINode n5 = new UINode(_btn5);
        UINode n6 = new UINode(_btn6);

        // 2. Wire the vertical connections (a straight line down)
        n1.Down = n2; n2.Up = n1;
        n2.Down = n3; n3.Up = n2;
        n3.Down = n4; n4.Up = n3;
        n4.Down = n5; n5.Up = n4;
        n5.Down = n6; n6.Up = n5;

        // (Left and Right remain null, so horizontal input is ignored)

        // 3. Initialize the graph
        _navGraph = new ButtonGraph();
        _navGraph.SetStartNode(n1);

        // 4. Bind the input events
        InputEvents.Key_W_Event += _navGraph.OnNavigate;
        InputEvents.Key_S_Event += _navGraph.OnNavigate;
        InputEvents.Key_A_Event += _navGraph.OnNavigate; // Will safely do nothing
        InputEvents.Key_D_Event += _navGraph.OnNavigate; // Will safely do nothing
        InputEvents.Key_Spacebar_Event += _navGraph.OnSubmit;
    }

    public override void Render()
    {
        _btn1.Render();
        _btn2.Render();
        _btn3.Render();
        _btn4.Render();
        _btn5.Render();
        _btn6.Render();
    }
}