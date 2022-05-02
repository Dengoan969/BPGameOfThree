using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] public string buffer;
    [SerializeField] private float maxTimeDif = 1f;
    private readonly List<string> validPatterns = new List<string> {"HESOYAM"};
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
            buffer = "";
        }

        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
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
            ImplementHesoyamCode();
            buffer = "";
        }
    }

    private static void ImplementHesoyamCode()
    {
        GameStatistics.Balance += 250000;
        GameStatistics.Endurance = float.MaxValue;
        GameStatistics.Fuel = float.MaxValue;
    }
}
