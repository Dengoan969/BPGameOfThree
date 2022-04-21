using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        // var a = GameObject.Find("MoveRoad").transform.Find("roadSpeed").GetComponent<MoveRoad>();
        if (speed <= 25f)
        {
            speed += 1f * Time.deltaTime;
        }
        // TODO UpgradeSpeed() 

        transform.Translate(Vector3.left * (speed * Time.deltaTime));
        if (transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
    }
}
