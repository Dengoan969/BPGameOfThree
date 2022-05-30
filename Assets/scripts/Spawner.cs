using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] roadPatterns;
    public GameObject[] bonuses;
    public GameObject[] roadsidePatterns;
    public GameObject fuel;
    public GameObject fuelStation;
    public GameObject repair;
    public GameObject repairStation;
    public GameObject streetLight;
    public GameObject[] sidewalkObjects;
    private readonly Random _randomGen = new Random();
    private Vector3 _stageSizes;
    private float[] _roadPositions;
    private float[] _sidewalkPositions;

    void Start()
    {
        _stageSizes = StageSizes.GetStageSizes();
        Debug.Log("x: " + _stageSizes.x);
        Debug.Log("y: " + _stageSizes.y);
        _roadPositions = new[]{-0.1522f * _stageSizes.x, -0.0527f * _stageSizes.x,
                                    0.1522f * _stageSizes.x, 0.0527f * _stageSizes.x};
        _sidewalkPositions = new[] { -0.25f * _stageSizes.x, 0.25f * _stageSizes.x };
        StartCoroutine(SpawnFuel());
        StartCoroutine(SpawnRepair());
        StartCoroutine(SpawnSidewalk());
        StartCoroutine(SpawnRoadside(roadsidePatterns, new[] { -0.5f * _stageSizes.x, 0.5f * _stageSizes.x }));
        StartCoroutine(SpawnCars(roadPatterns));
        StartCoroutine(SpawnMoney(bonuses[0], _roadPositions, 5));
    }

    private IEnumerator SpawnFuel()
    {
        while (!GameStatistics.IsGameOver)
        {
            if (!_maySpawnFuel)
            {
                if(GameStatistics.Fuel>0.5f)
                    yield return Distance.WaitForDistance((float)(_randomGen.NextDouble() * 4 + 1) * _stageSizes.y);
                else
                    yield return Distance.WaitForDistance((float)_randomGen.NextDouble()*2 * _stageSizes.y);
                _maySpawnFuel = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SpawnRepair()
    {
        while (!GameStatistics.IsGameOver)
        {
            if (!_maySpawnRepair)
            {
                if(GameStatistics.Endurance>0.5f)
                    yield return Distance.WaitForDistance((float)(_randomGen.NextDouble() * 4 + 1) * _stageSizes.y);
                else
                    yield return Distance.WaitForDistance((float)_randomGen.NextDouble() * 2 * _stageSizes.y);
                _maySpawnRepair = true;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SpawnCars(GameObject[] patterns)
    {
        var previousPattern = String.Empty;
        while (!GameStatistics.IsGameOver)
        {
            var nextPattern = patterns[_randomGen.Next(0, patterns.Length)];
            if (nextPattern.name == previousPattern)
            {
                continue;
            }
            previousPattern = nextPattern.name;
            if(nextPattern.CompareTag("Obstacle"))
            {
                yield return Distance.WaitForDistance(0.5f*_stageSizes.y);
            }
            var spawnedPattern = Instantiate(
                nextPattern,
                new Vector3(0, _stageSizes.y, 0),
                Quaternion.identity);

            if (_randomGen.Next(0, 2) == 0)
            {
                spawnedPattern.transform.Rotate(0, 180, 0);
                foreach (Transform child in spawnedPattern.transform)
                {
                    child.Rotate(0, 180, 0);
                }
            }
            spawnedPattern.transform.DetachChildren();
            Destroy(spawnedPattern);
            yield return Distance.WaitForDistance(_stageSizes.y);
        }
    }

    private bool _maySpawnFuel;
    private bool _maySpawnRepair;
    private IEnumerator SpawnRoadside(GameObject[] objects, float[] positions)
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextObject = objects[_randomGen.Next(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[0], _stageSizes.y, 0),
                Quaternion.identity).name = nextObject.name;
            nextObject = objects[_randomGen.Next(0, objects.Length)];
            Instantiate(
                nextObject,
                new Vector3(positions[1], _stageSizes.y, 0),
                Quaternion.identity).name = nextObject.name;
            while (MainCar.speed == 0)
            {
                yield return null;
            }
            var time = 0.667f * _stageSizes.y / (MainCar.speed);
            var position = -1;
            var roadsidePositions = new[] { -0.39f * _stageSizes.x, 0.39f * _stageSizes.x };
            if (_maySpawnFuel || _maySpawnRepair)
                time += 0.557f * _stageSizes.y / (MainCar.speed);
            if (_maySpawnFuel)
            {
                _maySpawnFuel = false;
                position = _randomGen.Next(0, 2);
                Instantiate(
                fuelStation,
                new Vector3(roadsidePositions[position], 1.557f * _stageSizes.y, 0),
                Quaternion.identity).name = fuelStation.name;
                Instantiate(
                fuel,
                new Vector3(_sidewalkPositions[position], 1.557f * _stageSizes.y, 0),
                Quaternion.identity).name = fuel.name;
            }
            if (_maySpawnRepair)
            {
                _maySpawnRepair = false;
                if (position != -1)
                    position = Math.Abs(position - 1);
                else
                    position = _randomGen.Next(0, 2);
                Instantiate(
                repairStation,
                new Vector3(roadsidePositions[position], 1.557f * _stageSizes.y, 0),
                Quaternion.identity).name = nextObject.name;
                Instantiate(
                repair,
                new Vector3(_sidewalkPositions[position], 1.557f * _stageSizes.y, 0),
                Quaternion.identity).name = repair.name;
            }
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator SpawnSidewalk()
    {
        while (!GameStatistics.IsGameOver)
        {
            while (MainCar.speed == 0)
            {
                yield return null;
            }
            Instantiate(
                    streetLight,
                    new Vector3(_sidewalkPositions[0], _stageSizes.y, 0),
                    Quaternion.identity).name = streetLight.name;
            var newLamp = Instantiate(
                    streetLight,
                    new Vector3(_sidewalkPositions[1], _stageSizes.y, 0),
                    Quaternion.identity);
            newLamp.name = streetLight.name;
            newLamp.transform.Rotate(0, 180, 0);
            yield return Distance.WaitForDistance(0.9f*_stageSizes.y);
        }
    }

    private IEnumerator SpawnMoney(GameObject money, float[] positions, int count)
    {
        while (!GameStatistics.IsGameOver)
        {
         var position = positions[_randomGen.Next(0, positions.Length)];
            var shift = 0f;
            for (var i = 0; i < count; i++)
            {
                Instantiate(money,
                    new Vector3(position, _stageSizes.y + shift, 0),
                    Quaternion.identity).name = money.name;
                shift += 0.1f * _stageSizes.y;
            }
            yield return Distance.WaitForDistance(1f * _stageSizes.y);
        }
    }
}