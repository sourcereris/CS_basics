using System;
using System.Collections.Generic;

Stack<string> UIScreens = new();

UIScreens.Push("Main Menu");
UIScreens.Push("Settings");
UIScreens.Push("Audio");

while(UIScreens.Count > 0)
{
    Console.WriteLine($"Closing Screen: {UIScreens.Pop()}");
}