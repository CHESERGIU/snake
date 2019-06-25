using System;

namespace Snake
{
    public interface IPixel
    {
        ConsoleColor ScreenColor { get; set; }
        int XPosition { get; set; }
        int YPosition { get; set; }

        void DrawPixel(IPixel pixel);

        void DrawBorder();

        IPixel Berry(Random random, ref int score);
    }

}