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
            XPos = xPos;
            YPos = yPos;
            ScreenColor = color;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor ScreenColor { get; set; }

        public static void Snake()
        {
            int score = Pixel.Score();
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine($"Game over, Score: {score - 5}");
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
            ReadKey();
        }

        public static int Score()
        {
            WindowHeight = 32;
            WindowWidth = 64;

            var rand = new Random();

            var score = 10;

            var head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Magenta);
            var berry = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Cyan);

            var body = new List<Pixel>();

            var currentMovement = Direction.Movement.Right;

            var gameover = false;

            while (true)
            {
                Clear();

                gameover |= (head.XPos == WindowWidth - 1 || head.XPos == 0 || head.YPos == WindowHeight - 1 || head.YPos == 0);

                DrawBorder();

                if (berry.XPos == head.XPos && berry.YPos == head.YPos)
                {
                    score++;
                    berry = new Pixel(rand.Next(1, WindowWidth - 1), rand.Next(1, WindowHeight - 1), ConsoleColor.Cyan);
                }

                for (int i = 0; i < body.Count; i++)
                {
                    DrawPixel(body[i]);
                    gameover |= (body[i].XPos == head.XPos && body[i].YPos == head.YPos);
                }

                if (gameover)
                {
                    break;
                }

                DrawPixel(head);
                DrawPixel(berry);

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 500)
                {
                    currentMovement = Direction.ReadMovement(currentMovement);
                }

                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                switch (currentMovement)
                {
                    case Direction.Movement.Up:
                        head.YPos--;
                        break;
                    case Direction.Movement.Down:
                        head.YPos++;
                        break;
                    case Direction.Movement.Left:
                        head.XPos--;
                        break;
                    case Direction.Movement.Right:
                        head.XPos++;
                        break;
                }

                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }

            return score;
        }

        public static void DrawPixel(Pixel pixel)
        {
            SetCursorPosition(pixel.XPos, pixel.YPos);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0);
        }

        public static void DrawBorder()
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