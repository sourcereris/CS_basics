using System.Numerics;

public class UINode
{
    public Button Btn { get; private set; }


    public UINode Up { get; set; }
    public UINode Down { get; set; }
    public UINode Left { get; set; }
    public UINode Right { get; set; }

    public UINode(Button btn)
    {
        Btn = btn;
    }
}

public class ButtonGraph
{
    public UINode Current { get; private set; }

    public void SetStartNode(UINode startNode)
    {
        Current = startNode;
        Current.Btn.IsSelected = true;
    }

    // This signature exactly matches your Action<Vector2> event
    public void OnNavigate(Vector2 direction)
    {
        if (Current == null) return;

        UINode nextNode = null;

        // Map the Vector2 to the correct neighbor
        if (direction.Y == 1) nextNode = Current.Up;    // W
        if (direction.Y == -1) nextNode = Current.Down;  // S
        if (direction.X == -1) nextNode = Current.Left;  // A
        if (direction.X == 1) nextNode = Current.Right; // D

        // If a connected button exists in that direction, move to it
        if (nextNode != null)
        {
            Current.Btn.IsSelected = false; // Deselect old
            Current = nextNode;             // Move pointer
            Current.Btn.IsSelected = true;  // Select new
        }
    }

    public void OnSubmit()
    {
        if (Current != null)
        {
            // Trigger the button's action (we will need to add this to the Button class)
            Current.Btn.Click();
        }
    }
}