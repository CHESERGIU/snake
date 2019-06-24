using System;

namespace Snake
{
    public interface IPixel
    {
        ConsoleColor ScreenColor { get; set; }
        int XPos { get; set; }
        int YPos { get; set; }
    }
}