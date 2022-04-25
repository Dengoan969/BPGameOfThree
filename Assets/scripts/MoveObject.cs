using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;

    private void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
    }
    void Update()
    {
        if (gameObject.name == "Money" || gameObject.name == "Fuel")
        {
            transform.Translate(Vector3.left * (MainCar.speed * Time.deltaTime));
        }
        else
        {
            transform.Translate(Vector3.left * (0.5f * MainCar.speed * Time.deltaTime));
        }
        
        if (transform.position.y < -stageSizes.y)
        {
            Destroy(gameObject);
        }
    }
}
