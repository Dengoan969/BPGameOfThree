using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public float[] roadSide;
    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] fuel;
    public GameObject[] obstacles;
    private static Vector3 stageSizes;
    private static bool isStageSizesSet;
    private readonly Random randomGen = new Random();
    /*public static readonly Dictionary<string, float> CarsSpeeds = new Dictionary<string, float>()
    {
        ["4x4_blue"] = 0.5f, 
        ["cabrio_blue"] = 0.4f,
        ["cabrio_yellow"] = 0.4f, 
        ["minicar_black"] = 0.5f, 
        ["pickup_gray"] = 0.5f,
        ["pickup_red"] = 0.5f,
        ["touringcar_white"] = 0.5f
    };*/
    public static readonly Dictionary<string, float> CarsSpeeds = new Dictionary<string, float>()
    {
        ["4x4_blue"] = 0.5f, 
        ["cabrio_blue"] = 0.5f,
        ["cabrio_yellow"] = 0.5f, 
        ["minicar_black"] = 0.5f, 
        ["pickup_gray"] = 0.5f,
        ["pickup_red"] = 0.5f,
        ["touringcar_white"] = 0.5f
    };
    
    // private Vector3 stageDimensions;
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
        //StartCoroutine(SpawnRoadObjects(cars, bonuses, roadPositions));
        StartCoroutine(Spawn(cars, roadPositions, 1.5f));
        StartCoroutine(SpawnMoney(bonuses[0], roadPositions, 5));
        StartCoroutine(Spawn(fuel, roadSide, 2));
    }

    private IEnumerator Spawn(GameObject[] objects, float[] positions, float time)
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[randomGen.Next(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[randomGen.Next(0, positions.Length)], stageSizes.y, -2),
                Quaternion.Euler(0, 0, nextObject.transform.rotation.z + 90)).name = nextObject.name;
            yield return new WaitForSeconds(time);
        }
    }

    /*private float? moneyPos;
    private float[] previousCarsSpeeds;
    private int spawnCounter;*/
    /*private IEnumerator SpawnRoadObjects(GameObject[] carsPrev, GameObject[] bonusesPrev, float[] positions)
    {
        while (!GameStatistics.IsGameOver)
        {
            var objects = new GameObject[4];
            previousCarsSpeeds ??= new float[4];
            var spawnObjects = randomGen.Next(4, 8); // (0, 4)
            
            for (var i = 0; i < spawnObjects; i++)
            {
                var carPosition = randomGen.Next(0, 4);
                if (moneyPos == null || Math.Abs(positions[carPosition] - moneyPos.Value) < 1e-9)
                {
                    if (randomGen.Next(0, 2) == 0 && !objects.Contains(bonusesPrev[0]))
                    {
                        objects[carPosition] = bonusesPrev[0];
                        moneyPos = positions[carPosition];
                    }
                    else
                    {
                        if (spawnCounter > objects.Length) //TODO
                        {
                            spawnCounter = 0;
                            previousCarsSpeeds = new float[4];
                        }

                        var car = carsPrev[randomGen.Next(0, carsPrev.Length)];
                        var currentCarSpeed = CarsSpeeds[car.name];
                        if (previousCarsSpeeds[carPosition] == 0f)
                        {
                            previousCarsSpeeds[carPosition] = currentCarSpeed;
                            objects[carPosition] = car;
                        }
                        else if (currentCarSpeed > previousCarsSpeeds[carPosition])
                        {
                            objects[carPosition] = car;
                            previousCarsSpeeds[carPosition] = currentCarSpeed;
                        }
                    }
                }
            }
            
            var counter = 0;
            foreach (var elem in objects)
            {
                if (elem != null)
                {
                    if (elem.name == "Money")
                    {
                        StartCoroutine(SpawnMoney(elem, positions[counter], randomGen.Next(3,9)));
                    }
                    else
                    {
                        Instantiate(
                            elem,
                            new Vector3(positions[counter], stageSizes.y, -2),
                            Quaternion.Euler(0, 0, elem.transform.rotation.z + 90)).name = elem.name;
                    }
                }
                
                counter++;
            }
    
            spawnCounter++;
            //yield return new WaitForSeconds(stageSizes.y / (6 * MainCar.speed));
            yield return new WaitForSeconds(1);
        }

    }*/

    private IEnumerator SpawnMoney(GameObject money, float[] position, int count)
    {
        while (!GameStatistics.IsGameOver)
        {
            var позиция = position[randomGen.Next(0, position.Length)];
            for (var i = 0; i < count; i++)
            {
                Instantiate(money,
                    new Vector3(позиция, stageSizes.y, -2),
                    Quaternion.Euler(0, 0, money.transform.rotation.z + 90)).name = money.name;
                // yield return new WaitForSeconds(1 / (0.25f * (MainCar.speed / (0.3f * stageSizes.y))));
                yield return new WaitForSeconds(0.25f);
            }
        
            yield return new WaitForSeconds(0.5f);
            // moneyPos = null;
        }
    }
}