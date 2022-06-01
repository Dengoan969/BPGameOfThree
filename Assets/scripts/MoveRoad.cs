using UnityEngine;


public class MoveRoad : MonoBehaviour
{
    public GameObject road;
    private bool isDown;
    public static Vector3 stageSizes;

    void Start()
    {
        stageSizes = StageSizes.GetStageSizes();
        CloneRoad(transform.position.y);
    }

    void Update()
    {
        if (!GameStatistics.IsGameOver)
        {
            var translation = Vector3.down * MainCar.Speed * Time.deltaTime;
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
            Instantiate(road, 
                new Vector3(0f, currentY + stageSizes.y, 0f), 
                Quaternion.identity).name 
                = road.name;
            isDown = true;
        }
    }
}
