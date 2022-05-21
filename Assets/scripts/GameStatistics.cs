public static class GameStatistics
{
    public static int Balance;
    public static bool IsGameOver;
    public static float Endurance = 1f;
    public static float Fuel = 100f;

    public static void Reset()
    {
        Balance = 0;
        IsGameOver = false;
        Endurance = 1f;
        Fuel = 100f;
    }
}
