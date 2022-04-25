using UnityEngine;
using System;

public class MainCar : MonoBehaviour
{
    public static float speed;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;

    void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
        speed = 0.3f * stageSizes.y;
    }

    void Update()
    {
        if (speed < 2 * stageSizes.y && !Game.IsGameOver)
        {
            //speed += (float)Math.Log(speed, 50 * stageSizes.y);
            speed += 0.01f * stageSizes.y * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Money")
        {
            Game.Balance += 10;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("DeadObstacle"))
        {
            speed = 0;
            Game.IsGameOver = true;
        }
    }
}
