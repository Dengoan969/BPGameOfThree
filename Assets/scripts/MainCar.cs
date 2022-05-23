using UnityEngine;
using System;
using System.Collections;

public class MainCar : MonoBehaviour
{
    public static float speed;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;
    public static GameObject Player;
    public GameObject PlayerRef;

    void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        Player = PlayerRef;
        speed = 0.3f * stageSizes.y;
        StartCoroutine(FuelControl());
    }

    void Update()
    {
        if (!GameStatistics.IsGameOver)
        {
            if (Math.Abs(transform.position.x) > 0.28 * stageSizes.x)
            {
                GameStatistics.Endurance -= 0.1f * Time.deltaTime;
            }

            if (GameStatistics.Fuel <= 0 || GameStatistics.Endurance <= 0 || transform.position.y < -300f)
            {
                speed = 0;
                GameStatistics.IsGameOver = true;
            }

            if (speed < 2 * stageSizes.y && !GameStatistics.IsGameOver)
            {
                //speed += (float)Math.Log(speed, 50 * stageSizes.y);
                speed += 0.01f * stageSizes.y * Time.deltaTime;
            }
        }
    }

    private IEnumerator FuelControl()
    {
        while (!GameStatistics.IsGameOver)
        {
            yield return Distance.WaitForDistance(0.01f*stageSizes.y);
            GameStatistics.Fuel -= 0.05f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Money":
                GameStatistics.Balance += 10;
                //coinSound.Play();
                Destroy(collision.gameObject);
                break;
            case "Fuel":
                if (GameStatistics.Fuel <= 1f)
                    GameStatistics.Fuel = 1f;
                Destroy(collision.gameObject);
                break;
        }

        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            speed = 0;
            GameStatistics.IsGameOver = true;
            PlayerPrefs.SetString("CurrentMusic", AllMusic.CurrentTrack);
        }
    }
    
    private void ShakeCar()
    {
        
    }
}
