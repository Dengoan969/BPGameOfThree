using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 stageDimensions;
    private float[] roadPositions; // -130 -45 45 130 0.15 0.05
    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] obstacles;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;

    void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
        roadPositions = new float[]{-0.15f * stageSizes.x, -0.05f * stageSizes.x,
                                    0.15f * stageSizes.x, 0.05f * stageSizes.x};
        StartCoroutine(Spawn(cars));
        StartCoroutine(Spawn(bonuses));
        // StartCoroutine(Spawn(obstacles));
    }

    private IEnumerator Spawn(GameObject[] objects)
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[Random.Range(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(roadPositions[Random.Range(0, 4)], stageSizes.y, -2),
                Quaternion.Euler(0, 0, nextObject.transform.rotation.z + 90)).name = nextObject.name;
            yield return new WaitForSeconds(Random.Range(2, 2));
        }
    }
}