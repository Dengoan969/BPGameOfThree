using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly float[] roadPositions = {-4f, -1.35f, 1.35f, 4f};
    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] obstacles;

    void Start()
    {
        StartCoroutine(Spawn(cars));
        StartCoroutine(Spawn(bonuses));
    }

    private IEnumerator Spawn(GameObject[] objects)
    {
        while (true)
        {
            var nextObject = objects[Random.Range(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(roadPositions[Random.Range(0, 4)], 9, -2), 
                Quaternion.Euler(0, 0, 90)).name = nextObject.name;
            yield return new WaitForSeconds(Random.Range(2, 2));
        }
    }
}