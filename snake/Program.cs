namespace Snake
{
    public static class Program
    {
        public static void Main()
        {
            Pixel game = new Pixel(0, 0, System.ConsoleColor.Red);
            game.Snake();
        }
    }
}