using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

static class InputEvents
{
    public static Action<Vector2> Key_W_Event;
    public static Action<Vector2> Key_D_Event;
    public static Action<Vector2> Key_A_Event;
    public static Action<Vector2> Key_S_Event;

    public static Action Key_Spacebar_Event;
}