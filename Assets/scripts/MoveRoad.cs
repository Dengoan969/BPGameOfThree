using UnityEngine;
using UnityEngine.Serialization;

public class MoveRoad : MonoBehaviour
{
    public GameObject road;
    [FormerlySerializedAs("speed")] public float roadSpeed = 5f;
    private bool isDown;
    
    void Start()
    {
        CloneRoad(transform.position.y);
    }

    void Update()
    {
        if (roadSpeed <= 25)
        {
            roadSpeed += 3f * Time.deltaTime;
        }
        // TODO UpgradeSpeed() 
        
        var translation = Vector3.down * roadSpeed * Time.deltaTime;
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
            Instantiate(road, new Vector3(0f, currentY + 14.67f, 0f), Quaternion.identity);
            isDown = true;
        }
    }
}
