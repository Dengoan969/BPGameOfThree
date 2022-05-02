using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] public string buffer;
    [SerializeField] private float maxTimeDif = 1f;
    private readonly List<string> validPatterns = new List<string> {"HESOYAM"};
    private float timeDif;
    
    void Start()
    {
        timeDif = maxTimeDif;
    }
    
    void Update()
    {
        timeDif -= Time.deltaTime;
        if (timeDif <= 0)
        {
            buffer = "";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddToBuffer("H");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            AddToBuffer("E");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddToBuffer("S");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            AddToBuffer("O");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            AddToBuffer("Y");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AddToBuffer("A");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            AddToBuffer("M");
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
        if (buffer.EndsWith(validPatterns[0]))
        {
            Debug.Log("HESOYAM code was initialized");
            ImplementInfinityCode();
            buffer = "";
        }
    }

    private void ImplementInfinityCode()
    {
        GameStatistics.Balance += 250000;
        GameStatistics.Endurance = float.MaxValue;
        GameStatistics.Fuel = float.MaxValue;
    }
}
