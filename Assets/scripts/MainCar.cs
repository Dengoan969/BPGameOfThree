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
    public static bool isInCar;
    public static float deltaX;

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
            isInCar = true;
            if (Math.Abs(collision.gameObject.transform.position.y - transform.position.y) < 35)
            {
                PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 250f;
            }
            else
            {
                if (collision.gameObject.name == "minicar_black")
                {
                    deltaX = 35f;
                }
                else
                {
                    deltaX = 40f;
                }
                PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 500f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            if (GameStatistics.Endurance <= 0)
            {
                speed = 0;
                GameStatistics.IsGameOver = true;
            }
            GameStatistics.Endurance -= 0.01f * speed / (2 * stageSizes.y);
            
            if (Math.Abs(collision.gameObject.transform.position.x - transform.position.x) <= deltaX
                && collision.gameObject.transform.position.y > transform.position.y)
            {
                speed = 0;
                GameStatistics.IsGameOver = true;
            }
            else
            {
                if (collision.gameObject.transform.position.x < transform.position.x) 
                    collision.gameObject.transform.position -= new Vector3(PlayerControl.deltaSpeed, 0, 0);
                else 
                    collision.gameObject.transform.position += new Vector3(PlayerControl.deltaSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D colission)
    {
        isInCar = false;
        PlayerControl.deltaSpeed = 0.01f * speed;
    }

    private void ShakeCar()
    {
        
    }
}
