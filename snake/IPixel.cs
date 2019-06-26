using System;

namespace Snake
{
    public interface IPixel
    {
        ConsoleColor ScreenColor { get; set; }

        int XPosition { get; set; }

        int YPosition { get; set; }

        void DrawPixel(IPixel pixel);

        void Score(ref int score, IPixel head, ref IPixel berry);

        void Play(ref int score, ref bool gameover, IPixel head, ref IPixel berry);
    }
}