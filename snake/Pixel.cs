using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;


namespace Snake
{
    public class Pixel : IPixel
    {
        private const int WindowHeight = 32;
        private const int WindowWidth = 64;
        private static readonly Random random = new Random();
        private List<Pixel> body = new List<Pixel>();
        private Direction.Movement currentMovement = Direction.Movement.Right;

        public Pixel(int xPos, int yPos, ConsoleColor color)
        {
            XPosition = xPos;
            YPosition = yPos;
            ScreenColor = color;
        }

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public ConsoleColor ScreenColor { get; set; }

        public void Snake()
        {            
            int score = 10;
            bool gameover = false;
            IPixel head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Magenta);
            IPixel berry = new Pixel(random.Next(1, WindowWidth), random.Next(1, WindowHeight), ConsoleColor.Red);
            head.Play(ref score, ref gameover, head, ref berry);
            WriteLine($"Game over, Score: {score - 5}");
            ReadKey();
        }

        void IPixel.Play(ref int score, ref bool gameover, IPixel head, ref IPixel berry)
        {
            while (true)
            {
                Clear();
                gameover |= (head.XPosition == WindowWidth - 1 || head.XPosition == 0 || head.YPosition == WindowHeight - 1 || head.YPosition == 0);
                head.Score(ref score, head, ref berry);
                for (int i = 0; i < body.Count; i++)
                {
                    head.DrawPixel(body[i]);
                    gameover |= (body[i].XPosition == head.XPosition && body[i].YPosition == head.YPosition);
                }
                if (gameover)
                {
                    break;
                }
                head.DrawPixel(head);
                berry.DrawPixel(berry);
                for (var stopWatch = Stopwatch.StartNew(); stopWatch.ElapsedMilliseconds <= 500;)
                {
                    currentMovement = Direction.ReadMovement(currentMovement);
                }
                body.Add(new Pixel(head.XPosition, head.YPosition, ConsoleColor.Green));
                Direction.Choice(head, currentMovement);
                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }
        }

        void IPixel.Score(ref int score, IPixel head, ref IPixel berry)
        {
            if (berry.XPosition == head.XPosition && berry.YPosition == head.YPosition)
            {
                score++;
                berry = new Pixel(random.Next(1, WindowWidth - 1), random.Next(1, WindowHeight - 1), ConsoleColor.Cyan);
            }
        }      

        void IPixel.DrawPixel(IPixel pixel)
        {
            SetCursorPosition(pixel.XPosition, pixel.YPosition);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0);
        }        
    }
}