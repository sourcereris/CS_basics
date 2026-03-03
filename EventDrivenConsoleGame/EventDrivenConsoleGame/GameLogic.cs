using System;
using System.Collections.Generic;
using System.Text;

class GameLogic
{
    public void TimeIncrement() 
    {
        GameData.time += KTime.DeltaTime;
    }
    public void AddScore()
    {
        GameData.Score += 10;
    }
}