using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    public GameObject road;
    private bool isDown = false;

    void Start()
    {
        CloneRoad(transform.position.y);
    }

    void Update()
    {
        var translation = Vector3.down * MainCar.speed * Time.deltaTime;
        CloneRoad(transform.position.y + translation.y);
        transform.Translate(translation);
        if (transform.position.y < -15f)
        {
            Destroy(gameObject);
        }
    }

    private void CloneRoad(float currentY)
    {
        if (!isDown && currentY < 7f)
        {
            Instantiate(road, new Vector3(0f, currentY + 14.67f, 0f), Quaternion.identity).name = road.name;
            isDown = true;
        }
    }
}
