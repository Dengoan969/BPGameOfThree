using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CheatCodes : MonoBehaviour
{
    [SerializeField] public string buffer;
    [SerializeField] private float maxTimeDif = 1f;
    private readonly List<string> validPatterns = new List<string> {"HESOYAM", "SPEEDUP", "EVANGELION"};
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
        else if (buffer.ToUpper().EndsWith(validPatterns[2]))
        {
            Debug.Log("You're EVA-1");
            ImplementEvangelion();
        }
    }

    private static void ImplementHesoyamCode()
    {
        GameStatistics.Balance += 250000;
        GameStatistics.Endurance = float.MaxValue;
        GameStatistics.Fuel = float.MaxValue;
        ((PolygonCollider2D) MainCar.Player.GetComponent(typeof(PolygonCollider2D))).enabled = false;
    }

    private static void ImplementSpeedUpCode() => MainCar.Speed = 750f;

    private void ImplementEvangelion()
    {
        var square = GameObject.FindGameObjectWithTag("EvaSpec").GetComponent<SpriteRenderer>();
        StartCoroutine(MakeTransparency(square));
        // PlayerPrefs.DeleteKey("CurrentMusic");
        GameObject
            .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic"))
            .GetComponent<AudioSource>()
            .Pause();
        GameObject.FindGameObjectWithTag("EvaGO").GetComponent<VideoPlayer>().Play();
    }
    
    private void ClearBuffer() => buffer = string.Empty;

    private IEnumerator MakeTransparency(SpriteRenderer renderer)
    {
        for (var i = 0; i < 100; i++)
        {
            renderer.color = new Color(255f, 255f, 255f, i);
            yield return new WaitForSeconds(1f);
        }
        
    }
}

