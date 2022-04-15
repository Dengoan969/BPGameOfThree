using UnityEngine;

public class MoveObject : MonoBehaviour
{

    private float speed = 25f;
    void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
        if (transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
    }
}
