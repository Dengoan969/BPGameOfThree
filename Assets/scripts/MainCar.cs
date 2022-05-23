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
                if (GameStatistics.Endurance + 0.5f * speed / (2 * stageSizes.y) > 1)
                    GameStatistics.Endurance = 1f;
                else
                    GameStatistics.Endurance += 0.75f * speed / (2 * stageSizes.y);
                Destroy(collision.gameObject);
                break;
        }

        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            // speed = 0;
            // GameStatistics.IsGameOver = true;
            PlayerPrefs.SetString("CurrentMusic", AllMusic.CurrentTrack);
            if (!GameStatistics.IsGameOver)
            {
                var collisionCar = collision.gameObject;
                var parent = collisionCar.transform.parent;
                isInCar = true;
                if (Math.Abs(collision.gameObject.transform.position.y - transform.position.y) < 60f
                    && collision.gameObject.transform.position.y > transform.position.y)
                {
                    PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 250f;
                    if (collisionCar.transform.position.x < transform.position.x
                        && parent.rotation.z == 0)
                    {
                        parent.Rotate(0f, 0f, -3f);
                        // var rotation = Quaternion.Euler(0f, 0f, -3f);
                        // parent.rotation = Quaternion.Lerp(parent.rotation, rotation,
                        //     PlayerControl.speed * Time.deltaTime);
                    }
                    else if (collisionCar.transform.position.x > transform.position.x
                             && parent.rotation.z == 0)
                    {
                        parent.Rotate(0f, 0f, 3f);
                        // var rotation = Quaternion.Euler(0f, 0f, 3f);
                        // parent.rotation = Quaternion.Lerp(parent.rotation, rotation,
                        //     PlayerControl.speed * Time.deltaTime);
                    }
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

                    if (Math.Abs(collisionCar.transform.position.x - transform.position.x) <= deltaX
                        && collisionCar.transform.position.y > transform.position.y)
                    {
                        speed = 0;
                        GameStatistics.IsGameOver = true;
                    }
                    else
                    {

                        if (collisionCar.transform.position.x < transform.position.x
                            && parent.rotation.z == 0)
                        {
                            parent.Rotate(0f, 0f, 3f);
                            // var rotation = Quaternion.Euler(0f, 0f, 3f);
                            // parent.rotation = Quaternion.Lerp(parent.rotation, rotation,
                            //     PlayerControl.speed * Time.deltaTime);
                        }
                        else if (collisionCar.transform.position.x > transform.position.x
                                 && parent.rotation.z == 0)
                        {
                            parent.Rotate(0f, 0f, -3f);
                            // var rotation = Quaternion.Euler(0f, 0f, -3f);
                            // parent.rotation = Quaternion.Lerp(parent.rotation, rotation,
                            //     PlayerControl.speed * Time.deltaTime);
                        }
                    }

                    PlayerControl.deltaSpeed = 0.3f * stageSizes.y / 500f;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var collisionCar = collision.gameObject;
        var parent = collisionCar.transform.parent;
        if (collisionCar.CompareTag("DeadObstacle"))
        {
            GameStatistics.Endurance -= 0.01f * speed / (2 * stageSizes.y);
            
            if (Math.Abs(collisionCar.transform.position.x - transform.position.x) <= deltaX
                && collisionCar.transform.position.y > transform.position.y)
            {
                speed = 0;
                GameStatistics.IsGameOver = true;
            }
            else
            {
                if (collisionCar.transform.position.x < transform.position.x)
                {
                    // parent.Rotate(0f, 0f, 3f);
                    // var rotation = Quaternion.Euler(0f, 0f, 3);
                    // parent.rotation = Quaternion.Lerp(parent.rotation, rotation, PlayerControl.speed * Time.deltaTime);
                    collisionCar.transform.position -= new Vector3(PlayerControl.deltaSpeed, 0, 0);
                }
                else
                {
                    // parent.Rotate(0f, 0f, -3f);
                    // var rotation = Quaternion.Euler(0f, 0f, -3);
                    // parent.rotation = Quaternion.Lerp(parent.rotation, rotation, PlayerControl.speed * Time.deltaTime);
                    collisionCar.transform.position += new Vector3(PlayerControl.deltaSpeed, 0, 0);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            var tmpTransform = collision.gameObject.transform.parent;
            isInCar = false;
            PlayerControl.deltaSpeed = 0.01f * speed;
            
            if (tmpTransform.transform.rotation.z < 0)
                tmpTransform.Rotate(new Vector3(0f, 0f, 3f));
            else
                tmpTransform.Rotate(new Vector3(0f, 0f, -3f));
        }
        
        // var defaultRot = Quaternion.Euler(0f, 0f, 0f);
        // hui.rotation = Quaternion.Lerp(hui.rotation, defaultRot, PlayerControl.speed * Time.deltaTime);
    }

    private void ShakeCar()
    {
        
    }
}
