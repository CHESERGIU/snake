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
                var key = ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && movement != Movement.Down)
                {
                    movement = Movement.Up;
                }
                else if (key == ConsoleKey.DownArrow && movement != Movement.Up)
                {
                    movement = Movement.Down;
                }
                else if (key == ConsoleKey.LeftArrow && movement != Movement.Right)
                {
                    movement = Movement.Left;
                }
                else if (key == ConsoleKey.RightArrow && movement != Movement.Left)
                {
                    movement = Movement.Right;
                }
            }

            return movement;
        }
    }
}