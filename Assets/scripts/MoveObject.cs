using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private float carSpeed = 2f;
    void Update()
    {
        // var a = GameObject.Find("MoveRoad").transform.Find("roadSpeed").GetComponent<MoveRoad>();
        if (carSpeed <= 25f)
        {
            carSpeed += 0.5f * Time.deltaTime;
        }

        transform.Translate(Vector3.left * (carSpeed * Time.deltaTime));
        if (transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
    }
}
