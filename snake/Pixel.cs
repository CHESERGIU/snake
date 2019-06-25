using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Console;


namespace Snake
{
    public class Pixel : IPixel
    {        
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
            var activeGame = new Pixel(0, 0, ConsoleColor.Yellow);

            int score = activeGame.Score();
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine($"Game over, Score: {score - 5}");
            SetCursorPosition(WindowWidth / 5, (WindowHeight / 2) + 1);
            ReadKey();
        }

        public int Score()
        {
            WindowHeight = 32;
            WindowWidth = 64;

            var random = new Random();

            var score = 10;

            IPixel head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Magenta);
            IPixel berry = new Pixel(random.Next(1, WindowWidth), random.Next(1, WindowHeight), ConsoleColor.Cyan);

            List<Pixel> body = new List<Pixel>();

            Direction.Movement currentMovement = Direction.Movement.Right;

            bool gameover = false;

            while (true)
            {
                Clear();

                gameover |= (head.XPosition == WindowWidth - 1 || head.XPosition == 0 || head.YPosition == WindowHeight - 1 || head.YPosition == 0);

                head.DrawBorder();

                EatBerry(random, ref score, head, ref berry);

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

                var stopWatch = Stopwatch.StartNew();
                while (stopWatch.ElapsedMilliseconds <= 500)
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

            return score;
        }

        public void EatBerry(Random random, ref int score, IPixel head, ref IPixel berry)
        {
            if (berry.XPosition == head.XPosition && berry.YPosition == head.YPosition)
            {
                berry.Berry(random, ref score);
            }
        }

        public IPixel Berry(Random random, ref int score)
        {
            score++;
            return new Pixel(random.Next(1, WindowWidth - 1), random.Next(1, WindowHeight - 1), ConsoleColor.Cyan);
        }

        void IPixel.DrawPixel(IPixel pixel)
        {
            SetCursorPosition(pixel.XPosition, pixel.YPosition);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0);
        }

        void IPixel.DrawBorder()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                SetCursorPosition(i, 0);
                Write("■");

                SetCursorPosition(i, WindowHeight - 1);
                Write("■");
            }

            for (int i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition(0, i);
                Write("■");

                SetCursorPosition(WindowWidth - 1, i);
                Write("■");
            }
        }
    }
}