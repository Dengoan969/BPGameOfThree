using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private Vector3 _stageSizes;

    private void Start()
    {
        _stageSizes = StageSizes.GetStageSizes();
    }
    void Update()
    {
        if (gameObject.CompareTag("Car"))
        {
            transform.Translate(Vector3.down * (0.5f * MainCar.speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.down * (MainCar.speed * Time.deltaTime));
        }
        
        if (transform.position.y < -_stageSizes.y)
        {
            Destroy(gameObject);
        }
    }
}
