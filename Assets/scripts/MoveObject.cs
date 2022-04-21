using UnityEngine;

public class MoveObject : MonoBehaviour
{
    void Update()
    {
        if (gameObject.name == "Money")
        {
            transform.Translate(Vector3.left * (MainCar.speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.left * (0.5f * MainCar.speed * Time.deltaTime));
        }
        
        if (transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
    }
}
