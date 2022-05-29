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
    private static float deltaAngle;

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
            case "Repair":
                if (GameStatistics.Endurance + 0.5f * speed / (2 * stageSizes.y) > 1)
                    GameStatistics.Endurance = 1f;
                else
                    GameStatistics.Endurance += 0.75f * speed / (2 * stageSizes.y);
                Destroy(collision.gameObject);
                break;
        }

        // if (collision.gameObject.CompareTag("DeadObstacle"))
        // {
        //     speed = 0;
        //     GameStatistics.IsGameOver = true;
        // }

        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            if (!GameStatistics.IsGameOver)
            {
                var parent = collision.gameObject.transform.parent;
                isInCar = true;
                if (collision.gameObject.name == "minicar_black")
                {
                    deltaX = 35f;
                }
                else
                {
                    deltaX = 40f;
                }
                if (Math.Abs(parent.position.x - transform.position.x) <= deltaX && parent.position.y > transform.position.y)
                {
                    speed = 0;
                    GameStatistics.IsGameOver = true;
                }
                else if (Math.Abs(parent.position.y - transform.position.y) > 75f
                    && parent.position.y > transform.position.y)
                {
                    PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 500f;
                    if (parent.position.x < transform.position.x)
                    {
                        parent.Rotate(0f, 0f, -PlayerControl.deltaAngle / 1.5f);
                    }
                    else if (parent.position.x > transform.position.x)
                    {
                        parent.Rotate(0f, 0f, PlayerControl.deltaAngle / 1.5f);
                    }
                }
                else if (Math.Abs(parent.position.y - transform.position.y) < 75f
                         || parent.position.y < transform.position.y)
                {
                    PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 250f;
                    if (parent.position.x < transform.position.x)
                    {
                        parent.Rotate(0f, 0f, PlayerControl.deltaAngle / 1.5f);
                    }
                    else if (parent.position.x > transform.position.x)
                    {
                        parent.Rotate(0f, 0f, -PlayerControl.deltaAngle / 1.5f);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("DeadObstacle"))
        {
            var parent = collision.gameObject.transform.parent;
            
            GameStatistics.Endurance -= 0.01f * speed / (2 * stageSizes.y);
            if (parent.position.x < transform.position.x)
            {
                parent.position -= new Vector3(PlayerControl.deltaSpeed, 0, 0);
            }
            else
            {
                parent.position += new Vector3(PlayerControl.deltaSpeed, 0, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            PlayerControl.deltaSpeed = 0.01f * speed;
            var parent = collision.gameObject.transform.parent;
            if (parent.rotation != Quaternion.identity)
            {
                // if (parent.rotation.z < 0)
                //     parent.Rotate(0f, 0f, PlayerControl.deltaAngle / 2f);
                // else
                //     parent.Rotate(0f, 0f, -PlayerControl.deltaAngle / 2f);
                parent.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
            isInCar = false;
        }
    }

    private void ShakeCar()
    {
        
    }
}
