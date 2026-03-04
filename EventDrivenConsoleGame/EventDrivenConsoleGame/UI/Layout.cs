using System;
using System.Collections.Generic;
using System.Text;

public abstract class Layout
{
    protected readonly int centerX = GameData.WIDTH/2;
    protected readonly int centerY = GameData.HEIGHT/2;
    public virtual void Render() { }
}
