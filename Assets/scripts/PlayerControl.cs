using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform player;
    public static float DeltaSpeed;
    public static float DeltaAngle = 10f;
    public static Vector3 StagesSizes;
    public static float Speed = 6f;

    private void Start()
    {
        StagesSizes = StageSizes.GetStageSizes();
        DeltaSpeed = 0.01f * MainCar.Speed;
    }
    void Update()
    {
        if (!GameStatistics.IsGameOver && !PauseMenu.GameIsPaused)
        {
            if (DeltaSpeed < 0.01f * StagesSizes.y && !MainCar.IsInCar)
            {
                DeltaSpeed = 0.01f * MainCar.Speed;
            }
            if (DeltaAngle > 1f)
            {
                DeltaAngle -= 0.002f;
            }
            if (player.position.y < -0.323f* StagesSizes.y)
            {
                player.position += new Vector3(0, 1f, 0);
            }

            if (MainCar.Speed % 10 == 0 && Math.Abs(MainCar.Speed - 50f) > 10e-9)
                DeltaSpeed += 0.005f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 90f + DeltaAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, -DeltaSpeed, 30f + -StagesSizes.x / 2);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 90f - DeltaAngle);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, DeltaSpeed, -30f + StagesSizes.x / 2);
            }
            else
            {
                var defaultRot = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, Speed * Time.deltaTime);
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
