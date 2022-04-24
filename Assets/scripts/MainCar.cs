using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCar : MonoBehaviour
{
    public static float speed;

    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 50 && !GameStatistics.IsGameOver)
        {
            speed += 0.1f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Money")
        {
            GameStatistics.Balance += 10;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("DeadObstacle"))
        {
            speed = 0;
            GameStatistics.IsGameOver = true;
        }
    }
}
