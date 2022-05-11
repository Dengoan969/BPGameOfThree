using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;
    // public static Dictionary<string, float> carsSpeeds;

    private void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        // carsSpeeds = new Dictionary<string, float>()
        // {
        //     ["4x4_blue"] = 0.5f, 
        //     ["cabrio_blue"] = 0.4f,
        //     ["cabrio_yellow"] = 0.4f, 
        //     ["minicar_black"] = 0.5f, 
        //     ["pickup_gray"] = 0.5f,
        //     ["pickup_red"] = 0.5f,
        //     ["touringcar_white"] = 0.5f
        // };
    }
    void Update()
    {
        if (Spawner.CarsSpeeds.TryGetValue(gameObject.name, out var objectSpeed))
        {
            transform.Translate(Vector3.left * objectSpeed * MainCar.speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * (MainCar.speed * Time.deltaTime));
        }
        
        if (transform.position.y < -stageSizes.y)
        {
            Destroy(gameObject);
        }
    }
}
