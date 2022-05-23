using UnityEngine;

public static class GameStatistics
{
    public static int Balance = PlayerPrefs.GetInt("Money", 0);
    public static bool IsGameOver;
    public static float Endurance = 1f;
    public static float Fuel = 100f;

    public static void Reset()
    {
        IsGameOver = false;
        Endurance = 1f;
        Fuel = 100f;
    }
}
