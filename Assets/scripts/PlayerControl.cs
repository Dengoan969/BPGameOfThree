using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Transform player;
    public static float deltaSpeed;
    public static float deltaAngle = 10f;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;
    public static float speed = 6f;

    private void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
        //delta = 0.0025f * stageSizes.x;
        deltaSpeed = 0.01f * MainCar.speed;
    }
    void Update()
    {
        // TODO make PAUSE at escape
        if (deltaSpeed < 0.01f * stageSizes.y && !MainCar.isInCar)
        {
            deltaSpeed = 0.01f * MainCar.speed;
        }
        if (deltaAngle > 1f)
        {
            deltaAngle -= 0.002f;
        }

        if (!GameStatistics.IsGameOver)
        {
            if (player.position.x > 245f
                || player.position.x < -245f)
            {
                player.position += new Vector3(0, -0.5f, 0);
            }
            else
            {
                if (player.position.y < -155f)
                {
                    player.position += new Vector3(0, 1f, 0);
                }
            }
            
            if (MainCar.speed % 10 == 0 && Math.Abs(MainCar.speed - 50f) > 10e-9)
                deltaSpeed += 0.005f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 90f + deltaAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, -deltaSpeed, 30f + -stageSizes.x / 2);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 90f - deltaAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, deltaSpeed, -30f + stageSizes.x / 2);
            }
            else
            {
                var defaultRot = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, speed * Time.deltaTime);
            }
        }
    }

    private Vector3 MoveInsideBounds(Vector3 pos, float inpDelta, float bound)
    {
        if (bound < 0)
            return (pos + new Vector3(inpDelta, 0, 0)).x >= bound
                ? player.position + new Vector3(inpDelta, 0, 0)
                : player.position + Vector3.zero;
        return (pos + new Vector3(inpDelta, 0, 0)).x <= bound
            ? player.position + new Vector3(inpDelta, 0, 0)
            : player.position + Vector3.zero;
    }
}
