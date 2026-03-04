using System;
using System.Collections.Generic;
using System.Text;

class GameLogic
{

    public GameLogic()
    {
        InputEvents.Key_Spacebar_Event += AddScore;
    }

    public void TimeIncrement() 
    {
        GameData.time += KTime.DeltaTime;
    }
    public void AddScore()
    {
        GameData.Score += 10;
    }
}