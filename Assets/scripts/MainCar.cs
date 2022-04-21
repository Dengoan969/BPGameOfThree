using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCar : MonoBehaviour
{
    public static float speed;

    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 50 && !GameStatistics.isGameOver)
        {
            speed += 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Money")
        {
            GameStatistics.balance += 10;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("DeadObstacle"))
        {
            speed = 0;
            GameStatistics.isGameOver = true;
        }
    }
}
