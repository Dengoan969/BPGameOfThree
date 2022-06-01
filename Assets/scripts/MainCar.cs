using UnityEngine;
using System;
using System.Collections;

public class MainCar : MonoBehaviour
{
    public static float Speed;
    public static GameObject Player;
    public GameObject playerRef;
    public static bool IsInCar;
    public static float DeltaX;
    public static Vector3 StagesSizes;

    void Start()
    {
        StagesSizes = StageSizes.GetStageSizes();
        Player = playerRef;
        Speed = 0.3f * StagesSizes.y;
        StartCoroutine(FuelControl());
    }

    void Update()
    {
        if (!GameStatistics.IsGameOver && !PauseMenu.GameIsPaused)
        {
            if (Math.Abs(transform.position.x) > 0.28 * StagesSizes.x)
            {
                GameStatistics.Endurance -= 0.1f * Time.deltaTime;
            }

            if (GameStatistics.Fuel <= 0 || GameStatistics.Endurance <= 0 || transform.position.y < -300f)
            {
                Speed = 0;
                GameStatistics.IsGameOver = true;
            }

            if (Speed < 2 * StagesSizes.y && !GameStatistics.IsGameOver)
            {
                Speed += 0.01f * StagesSizes.y * Time.deltaTime;
            }
        }
    }

    private IEnumerator FuelControl()
    {
        while (!GameStatistics.IsGameOver)
        {
            yield return Distance.WaitForDistance(0.01f*StagesSizes.y);
            GameStatistics.Fuel -= 0.05f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Money":
                GameObject.FindGameObjectWithTag("MainMoneySound").GetComponent<AudioSource>().Play();
                GameStatistics.Balance += 10;
                PlayerPrefs.SetInt("Money", GameStatistics.Balance);
                Destroy(collision.gameObject);
                break;
            case "Fuel":
                if (GameStatistics.Fuel <= 1f)
                    GameStatistics.Fuel = 1f;
                GameObject.FindGameObjectWithTag("Fuel").GetComponent<AudioSource>().Play();
                Destroy(collision.gameObject);
                break;
            case "Repair":
                GameObject.FindGameObjectWithTag("FixSound").GetComponent<AudioSource>().Play();
                if (GameStatistics.Endurance + 0.5f * Speed / (2 * StagesSizes.y) > 1)
                    GameStatistics.Endurance = 1f;
                else
                    GameStatistics.Endurance += 0.75f * Speed / (2 * StagesSizes.y);
                Destroy(collision.gameObject);
                break;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameObject.FindGameObjectWithTag("ObstacleCrash").GetComponent<AudioSource>().Play();
            GameStatistics.Endurance -= 0.2f * Speed / (2 * StagesSizes.y);
            collision.gameObject.transform.rotation = Quaternion.Euler(70, 0, 0);
        }
        
        if (collision.gameObject.CompareTag("LampObstacle"))
        {
            GameStatistics.Endurance -= 0.8f * Speed / (2 * StagesSizes.y);
            if (collision.gameObject.transform.position.x < 0)
            {
                collision.gameObject.transform.position += new Vector3(-45f, 0f, 0f);
                transform.position += new Vector3(15f, 0, 0);
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 30f);
            }
            else
            {
                collision.gameObject.transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
                collision.gameObject.transform.position += new Vector3(-30f, 0f, 0f);
                transform.position += new Vector3(-15f, 0, 0);
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 180, 30f);
            }
        }
        
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            Speed = 0;
            GameStatistics.IsGameOver = true;
            GameObject.FindGameObjectWithTag("DeadCrashAudio").GetComponent<AudioSource>().Play();
        }

        if (collision.gameObject.CompareTag("Car"))
        {
            GameStatistics.Endurance -= 0.1f * Speed / (2 * StagesSizes.y);
            if (!GameStatistics.IsGameOver)
            {
                var parent = collision.gameObject.transform.parent;
                IsInCar = true;
                if (collision.gameObject.name == "minicar_black")
                {
                    DeltaX = 35f;
                }
                else
                {
                    DeltaX = 40f;
                }
                if (Math.Abs(parent.position.x - transform.position.x) <= DeltaX && parent.position.y > transform.position.y)
                {
                    Speed = 0;
                    GameStatistics.IsGameOver = true;
                    GameObject.FindGameObjectWithTag("DeadCrashAudio").GetComponent<AudioSource>().Play();
                }
                else if (Math.Abs(parent.position.y - transform.position.y) > 75f
                    && parent.position.y > transform.position.y)
                {
                    PlayerControl.DeltaSpeed = 0.3f * StagesSizes.y / 500f;
                    if (parent.position.x < transform.position.x)
                    {
                        parent.Rotate(0f, 0f, -PlayerControl.DeltaAngle / 1.75f);
                    }
                    else if (parent.position.x > transform.position.x)
                    {
                        parent.Rotate(0f, 0f, PlayerControl.DeltaAngle / 1.75f);
                    }
                }
                else if (Math.Abs(parent.position.y - transform.position.y) < 75f
                         || parent.position.y < transform.position.y)
                {
                    PlayerControl.DeltaSpeed = 0.3f * StagesSizes.y / 250f;
                    if (parent.position.x < transform.position.x)
                    {
                        parent.Rotate(0f, 0f, PlayerControl.DeltaAngle / 1.75f);
                    }
                    else if (parent.position.x > transform.position.x)
                    {
                        parent.Rotate(0f, 0f, -PlayerControl.DeltaAngle / 1.75f);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Car"))
        {
            var parent = collision.gameObject.transform.parent;
            
            GameStatistics.Endurance -= 0.01f * Speed / (2 * StagesSizes.y);
            if (parent.position.x < transform.position.x)
            {
                parent.position -= new Vector3(PlayerControl.DeltaSpeed, 0, 0);
            }
            else
            {
                parent.position += new Vector3(PlayerControl.DeltaSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            PlayerControl.DeltaSpeed = 0.01f * Speed;
            var parent = collision.gameObject.transform.parent;
            if (parent.rotation != Quaternion.identity)
            {
                parent.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            IsInCar = false;
        }
    }
}
