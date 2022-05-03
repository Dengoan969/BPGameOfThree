using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float[] roadSide;
    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] fuel;
    public GameObject[] obstacles;
    public static Vector3 stageSizes;
    public static bool isStageSizesSet;
    
    
    private Vector3 stageDimensions;
    private float[] roadPositions; // -130 -45 45 130 0.15 0.05

    void Start()
    {
        if (!isStageSizesSet)
        {
            isStageSizesSet = true;
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }
        roadPositions = new []{-0.1522f * stageSizes.x, -0.0527f * stageSizes.x,
                                    0.1522f * stageSizes.x, 0.0527f * stageSizes.x};
        roadSide = new[] {-0.25f * stageSizes.x, 0.25f * stageSizes.x};
        StartCoroutine(SpawnRoadObjects(cars, bonuses, roadPositions));
        //StartCoroutine(SpawnMoney(bonuses[0], roadPositions[Random.Range(0, roadPositions.Length)], 5));
        //StartCoroutine(Spawn(fuel, roadSide, 2));
    }

    // private IEnumerator Spawn(GameObject[] objects, float[] positions, float time)
    // {
    //     while (!GameStatistics.IsGameOver)
    //     {
    //         var nextObject = objects[Random.Range(0, objects.Length)];
    //         Instantiate(
    //             nextObject,
    //             new Vector3(positions[Random.Range(0, positions.Length)], stageSizes.y, -2),
    //             Quaternion.Euler(0, 0, nextObject.transform.rotation.z + 90)).name = nextObject.name;
    //         yield return new WaitForSeconds(time);
    //     }
    // }

    private float? moneyPos;
    private float[] previousCarsSpeeds;
    private int spawnCounter;
    private IEnumerator SpawnRoadObjects(GameObject[] cars, GameObject[] bonuses, float[] positions)
    {
        var objects = new HashSet<GameObject>();
        if (previousCarsSpeeds == null)
            previousCarsSpeeds = new float[4];
        for (var i = 0; i < objects.Count; i++)
        {
            if (moneyPos == null || Math.Abs(positions[i] - moneyPos.Value) < 1e-9)
            {
                if (Random.Range(0, 3) == 0 && !objects.Contains(bonuses[0]))
                {
                    objects.Add(bonuses[0]);
                    moneyPos = positions[i];
                }

                if (spawnCounter > 10)
                {
                    spawnCounter = 0;
                    previousCarsSpeeds = new float[4];
                }

                var spawnCar = Random.Range(0, 3) == 0;
                if (spawnCar)
                {
                    var car = cars[Random.Range(0, cars.Length)];
                    var currentCarSpeed = MoveObject.carsSpeeds[car.name];
                    if (previousCarsSpeeds[i] == 0f)
                    {
                        previousCarsSpeeds[i] = currentCarSpeed;
                        objects.Add(car);
                    }
                    else if (currentCarSpeed > previousCarsSpeeds[i])
                    {
                        objects.Add(car);
                    }
                }
            }
            
        }
        var counter = 0;
        foreach (var elem in objects)
        {
            if (gameObject.name == "Money")
            {
                StartCoroutine(SpawnMoney(elem, positions[counter], Random.Range(3,9)));
            }
            else
            {
                Instantiate(
                    elem,
                    new Vector3(positions[counter], stageSizes.y, -2),
                    Quaternion.Euler(0, 0, elem.transform.rotation.z + 90)).name = elem.name;
            }
        }

        spawnCounter++;
        yield return new WaitForSeconds(stageSizes.y / (6 * MainCar.speed));
    }

    private IEnumerator SpawnMoney(GameObject money, float position, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Instantiate(money,
                new Vector3(position, stageSizes.y, -2),
                Quaternion.Euler(0, 0, money.transform.rotation.z + 90)).name = money.name;
            yield return new WaitForSeconds(0.25f);
        }

        moneyPos = null;
    }
}