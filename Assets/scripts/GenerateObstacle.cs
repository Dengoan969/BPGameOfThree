using System.Collections;
using UnityEngine;

public class GenerateObstacle : MonoBehaviour
{
    private float[] positions = {-4f, -1.35f, 1.35f, 4f};
    public GameObject[] cars;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(
                cars[Random.Range(0, cars.Length)],
                new Vector3(positions[Random.Range(0, 4)], 9, 0), 
                Quaternion.Euler(0, 0, 90));
            yield return new WaitForSeconds(Random.Range(2, 2));
        }
    }
}
