using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{

    public GameObject[] cars;
    public GameObject[] bonuses;
    public GameObject[] outerRoadsideObjects;
    public GameObject fuel;
    public GameObject fuelStation;
    public GameObject repair;
    public GameObject repairStation;
    public GameObject lamp;
    public GameObject innerRoadsideObjects;
    public static readonly Dictionary<string, float> CarsSpeeds = new Dictionary<string, float>
    {
        ["4x4_blue"] = 0.5f,
        ["cabrio_blue"] = 0.5f,
        ["cabrio_yellow"] = 0.5f,
        ["minicar_black"] = 0.5f,
        ["pickup_gray"] = 0.5f,
        ["pickup_red"] = 0.5f,
        ["touringcar_white"] = 0.5f
    };
    private readonly Random randomGen = new Random();
    private Vector3 stageSizes;
    private float[] roadPositions;
    private float[] innerRoadside;

    void Start()
    {
        stageSizes = StageSizes.GetStageSizes();
        Debug.Log("x: " + stageSizes.x);
        Debug.Log("y: " + stageSizes.y);
        roadPositions = new[]{-0.1522f * stageSizes.x, -0.0527f * stageSizes.x,
                                    0.1522f * stageSizes.x, 0.0527f * stageSizes.x};
        innerRoadside = new[] { -0.25f * stageSizes.x, 0.25f * stageSizes.x };
        StartCoroutine(SpawnFuel());
        StartCoroutine(SpawnRepair());
        StartCoroutine(SpawnInnerRoadside());
        StartCoroutine(SpawnRoadside(outerRoadsideObjects, new[] { -0.5f * stageSizes.x, 0.5f * stageSizes.x }));
        StartCoroutine(SpawnCars(cars));
        StartCoroutine(SpawnMoney(bonuses[0], roadPositions, 5));
    }

    private IEnumerator SpawnFuel()
    {
        while (!GameStatistics.IsGameOver)
        {
            if (!maySpawnFuel)
            {
                if(GameStatistics.Fuel>0.5f)
                    yield return Distance.WaitForDistance((float)(randomGen.NextDouble() * 4 + 1) * stageSizes.y);
                else
                    yield return Distance.WaitForDistance((float)randomGen.NextDouble()*2 * stageSizes.y);
                maySpawnFuel = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SpawnRepair()
    {
        while (!GameStatistics.IsGameOver)
        {
            if (!maySpawnRepair)
            {
                if(GameStatistics.Endurance>0.5f)
                    yield return Distance.WaitForDistance((float)(randomGen.NextDouble() * 4 + 1) * stageSizes.y);
                else
                    yield return Distance.WaitForDistance((float)randomGen.NextDouble() * 2 * stageSizes.y);
                maySpawnRepair = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SpawnCars(GameObject[] objects)
    {
        // var carSpeedCoef = 0.15f * stageSizes.y * Time.deltaTime * 3f;
        var previousObject = objects[0];
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[randomGen.Next(0, objects.Length)];
            if (nextObject == previousObject)
            {
                continue;
            }
            previousObject = nextObject;
            var newObject = Instantiate(
                nextObject,
                new Vector3(0, stageSizes.y, -2),
                Quaternion.identity);

            if (randomGen.Next(0, 2) == 0)
            {
                newObject.transform.Rotate(0, 180, 0);
            }

            newObject.transform.DetachChildren();
            Destroy(newObject);

            while (MainCar.speed == 0)
            {
                yield return null;
            }

            yield return Distance.WaitForDistance(1f * stageSizes.y);

            // var time2 = carSpeedCoef / (0.5f * MainCar.speed * Time.deltaTime);
            // yield return new WaitForSeconds(time2);
        }
    }

    private bool maySpawnFuel;
    private bool maySpawnRepair;
    private IEnumerator SpawnRoadside(GameObject[] objects, float[] positions)
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[randomGen.Next(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[0], stageSizes.y, -1),
                Quaternion.identity).name = nextObject.name;
            nextObject = objects[randomGen.Next(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[1], stageSizes.y, -1),
                Quaternion.identity).name = nextObject.name;
            while (MainCar.speed == 0)
            {
                yield return null;
            }
            var time = 0.667f * stageSizes.y / (MainCar.speed);
            var position = -1;
            var roadsidePositions = new[] { -0.39f * stageSizes.x, 0.39f * stageSizes.x };
            if (maySpawnFuel || maySpawnRepair)
                time += 0.557f * stageSizes.y / (MainCar.speed);
            if (maySpawnFuel)
            {
                maySpawnFuel = false;
                position = randomGen.Next(0, 2);
                Instantiate(
                fuelStation,
                new Vector3(roadsidePositions[position], 1.557f * stageSizes.y, -1),
                Quaternion.identity).name = fuelStation.name;
                Instantiate(
                fuel,
                new Vector3(innerRoadside[position], 1.557f * stageSizes.y, -1),
                Quaternion.identity).name = fuel.name;
            }
            if (maySpawnRepair)
            {
                maySpawnRepair = false;
                if (position != -1)
                    position = Math.Abs(position - 1);
                else
                    position = randomGen.Next(0, 2);
                Instantiate(
                repairStation,
                new Vector3(roadsidePositions[position], 1.557f * stageSizes.y, -1),
                Quaternion.identity).name = nextObject.name;
                Instantiate(
                repair,
                new Vector3(innerRoadside[position], 1.557f * stageSizes.y, -1),
                Quaternion.identity).name = repair.name;
            }
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator SpawnInnerRoadside()
    {
        while (!GameStatistics.IsGameOver)
        {
            while (MainCar.speed == 0)
            {
                yield return null;
            }
            Instantiate(
                    lamp,
                    new Vector3(innerRoadside[0], stageSizes.y, -3),
                    Quaternion.identity).name = lamp.name;
            var newLamp = Instantiate(
                    lamp,
                    new Vector3(innerRoadside[1], stageSizes.y, -3),
                    Quaternion.identity);
            newLamp.name = lamp.name;
            newLamp.transform.Rotate(0, 180, 0);
            yield return Distance.WaitForDistance(0.5f * stageSizes.y);
        }
    }
    /*private IEnumerator Timer(float time)
    {
        while (true)
        {
            if (!maySpawn)
            {
                yield return new WaitForSeconds(time);
            }
            maySpawn = true;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
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

    private IEnumerator SpawnMoney(GameObject money, float[] positions, int count)
    {
        while (!GameStatistics.IsGameOver)
        {
            var position = positions[randomGen.Next(0, positions.Length)];
            var shift = 0f;
            for (var i = 0; i < count; i++)
            {
                Instantiate(money,
                    new Vector3(position, stageSizes.y + shift, -2),
                    Quaternion.identity).name = money.name;
                shift += 0.1f * stageSizes.y;
            }
            yield return Distance.WaitForDistance(1f * stageSizes.y);
        }
    }
}