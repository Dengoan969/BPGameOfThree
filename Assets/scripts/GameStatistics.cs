using UnityEngine;

public static class GameStatistics
{
    public static int Balance;
    public static bool IsGameOver;
    public static float Endurance = 1f;
    public static float Fuel = 1f;

    public static void Reset()
    {
        IsGameOver = false;
        Endurance = 1f;
        Fuel = 1f;
    }
}
