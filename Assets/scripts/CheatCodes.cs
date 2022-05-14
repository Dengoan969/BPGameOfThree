using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] public string buffer;
    [SerializeField] private float maxTimeDif = 1f;
    private readonly List<string> validPatterns = new List<string> {"HESOYAM", "SPEEDUP"};
    private float timeDif;
    
    private void Start()
    {
        timeDif = maxTimeDif;
    }

    private void Update()
    {
        timeDif -= Time.deltaTime;
        if (timeDif <= 0)
        {
            ClearBuffer();
        }

        if (Input.anyKeyDown)
        {
            foreach (var c in Input.inputString)
            {
                AddToBuffer(c.ToString());
            }
        }
        CheckPatterns();
    }

    void AddToBuffer(string cheat)
    {
        timeDif = maxTimeDif;
        buffer += cheat;
    }

    void CheckPatterns()
    {
        if (buffer.ToUpper().EndsWith(validPatterns[0]))
        {
            Debug.Log("HESOYAM code was initialized");
            if (!GameStatistics.IsGameOver)
                ImplementHesoyamCode();
            ClearBuffer();
        }
        else if (buffer.ToUpper().EndsWith(validPatterns[1]))
        {
            Debug.Log("SPEEDUP code was initialized");
            if (!GameStatistics.IsGameOver)
                ImplementSpeedUpCode();
            ClearBuffer();
        }
    }

    private static void ImplementHesoyamCode()
    {
        GameStatistics.Balance += 250000;
        GameStatistics.Endurance = float.MaxValue;
        GameStatistics.Fuel = float.MaxValue;
    }

    private static void ImplementSpeedUpCode() => MainCar.speed = 750f;

    private void ClearBuffer() => buffer = string.Empty;
}

