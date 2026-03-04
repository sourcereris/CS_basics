using System;
using System.Collections.Generic;
using System.Text;

static class UI 
{
    public static void RenderScore() 
    {
        Utils_UI.ClearBackBuffer();

        Utils_UI.DrawBox(0, 0, Info.WIDTH - 1, Info.HEIGHT - 1);
        Console.SetCursorPosition( 0, 0 );

        Utils_UI.Render();
    }
}