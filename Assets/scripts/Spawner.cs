using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 stageDimensions;
    private float[] roadPositions; // -130 -45 45 130 0.15 0.05
    public float[] roadSide;
    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] fuel;
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
        roadPositions = new []{-0.15f * stageSizes.x, -0.05f * stageSizes.x,
                                    0.15f * stageSizes.x, 0.05f * stageSizes.x};
        roadSide = new[] {-0.25f * stageSizes.x, 0.25f * stageSizes.x};
        StartCoroutine(Spawn(cars, roadPositions, 2));
        StartCoroutine(Spawn(bonuses, roadPositions, 2));
        StartCoroutine(Spawn(fuel, roadSide, 5));
        // StartCoroutine(Spawn(obstacles));
    }

    private IEnumerator Spawn(GameObject[] objects, float[] positions, float time)
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[Random.Range(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[Random.Range(0, positions.Length)], stageSizes.y, -2),
                Quaternion.Euler(0, 0, nextObject.transform.rotation.z + 90)).name = nextObject.name;
            yield return new WaitForSeconds(time);
        }
    }
}