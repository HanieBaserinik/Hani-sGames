namespace GameCollection
{
    public static class Settings
    {
        public static int Width { get; } = 20;
        public static int Height { get; } = 20;
        public static int Speed { get; } = 15;
        public static int Score { get; set; } = 0;
        public static int Points { get; } = 10;
        public static bool GameOver { get; set; } = false;
    }
}
