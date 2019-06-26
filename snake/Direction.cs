using System;
using static System.Console;

namespace Snake
{
    public static class Direction
    {
        public enum Movement
        {
            Up = 0,
            Down = 1,
            Right = 2,
            Left = 3
        }

        public static Movement ReadMovement(Movement movement)
        {
            if (KeyAvailable)
            {
                switch (ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow when movement != Movement.Down:
                        movement = Movement.Up;
                        break;
                    case ConsoleKey.DownArrow when movement != Movement.Up:
                        movement = Movement.Down;
                        break;
                    case ConsoleKey.LeftArrow when movement != Movement.Right:
                        movement = Movement.Left;
                        break;
                    case ConsoleKey.RightArrow when movement != Movement.Left:
                        movement = Movement.Right;
                        break;
                }
            }

            return movement;
        }

        public static void Choice(IPixel head, Direction.Movement currentMovement)
        {
            switch (currentMovement)
            {
                case Direction.Movement.Up:
                    {
                        head.YPosition--;
                        break;
                    }

                case Direction.Movement.Down:
                    {
                        head.YPosition++;
                        break;
                    }

                case Direction.Movement.Left:
                    {
                        head.XPosition--;
                        break;
                    }

                case Direction.Movement.Right:
                    {
                        head.XPosition++;
                        break;
                    }
            }
        }
    }
}