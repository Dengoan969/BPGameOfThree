using UnityEngine;


public class MoveRoad : MonoBehaviour
{
    public GameObject road;
    private bool isDown;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;

    void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
        CloneRoad(transform.position.y);
    }

    void Update()
    {
        if (!GameStatistics.IsGameOver)
        {
            var translation = Vector3.down * MainCar.speed * Time.deltaTime;
            CloneRoad(transform.position.y + translation.y);
            transform.Translate(translation);
            if (transform.position.y < -stageSizes.y)
            {
                Destroy(gameObject);
            }
        }
    }

    private void CloneRoad(float currentY)
    {
        if (!isDown && currentY < stageSizes.y)
        {
            Instantiate(road, new Vector3(0f, currentY + stageSizes.y, 0f), Quaternion.identity).name = road.name;
            isDown = true;
        }
    }
}
