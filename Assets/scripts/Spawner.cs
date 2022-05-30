using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] roadPatterns;
    public GameObject money;
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
    private float[] _roadsidePositions;
    private float[] _servicePositions;

    void Start()
    {
        _stageSizes = StageSizes.GetStageSizes();
        Debug.Log("x: " + _stageSizes.x);
        Debug.Log("y: " + _stageSizes.y);
        _roadPositions = new[]{-0.1522f * _stageSizes.x, -0.0527f * _stageSizes.x,
                                    0.1522f * _stageSizes.x, 0.0527f * _stageSizes.x};
        _sidewalkPositions = new[] { -0.25f * _stageSizes.x, 0.25f * _stageSizes.x };
        _roadsidePositions = new[] { -0.5f * _stageSizes.x, 0.5f * _stageSizes.x };
        _servicePositions = new[] { -0.39f * _stageSizes.x, 0.39f * _stageSizes.x };
        StartCoroutine(SpawnFuel());
        StartCoroutine(SpawnRepair());
        StartCoroutine(SpawnSidewalk());
        StartCoroutine(SpawnRoadside());
        StartCoroutine(SpawnRoadPatterns());
        StartCoroutine(SpawnMoney());
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

    private IEnumerator SpawnRoadPatterns()
    {
        var previousPattern = String.Empty;
        while (!GameStatistics.IsGameOver)
        {
            var nextPattern = roadPatterns[_randomGen.Next(0, roadPatterns.Length)];
            if (nextPattern.name == previousPattern)
            {
                continue;
            }
            previousPattern = nextPattern.name;
            if(nextPattern.CompareTag("Obstacle"))
            {
                yield return Distance.WaitForDistance(0.5f * _stageSizes.y);
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
    private IEnumerator SpawnRoadside()
    {
        while (!GameStatistics.IsGameOver)
        {
            var nextPattern = roadsidePatterns[_randomGen.Next(0, roadsidePatterns.Length)];
            Instantiate(
                nextPattern,
                new Vector3(_roadsidePositions[0], _stageSizes.y, 0),
                Quaternion.identity).name = nextPattern.name;
            nextPattern = roadsidePatterns[_randomGen.Next(0, roadsidePatterns.Length)];
            Instantiate(
                nextPattern,
                new Vector3(_roadsidePositions[1], _stageSizes.y, 0),
                Quaternion.identity).name = nextPattern.name;
            var waitDistance = 0.557f * _stageSizes.y;
            var position = -1;
            
            if (_maySpawnFuel || _maySpawnRepair)
            {
                waitDistance += 0.337f * _stageSizes.y;
            }
            
            if (_maySpawnFuel)
            {
                _maySpawnFuel = false;
                position = _randomGen.Next(0, 2);
                Instantiate(
                    fuelStation,
                    new Vector3(_servicePositions[position], 1.557f * _stageSizes.y, 0),
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
                    new Vector3(_servicePositions[position], 1.557f * _stageSizes.y, 0),
                    Quaternion.identity).name = repairStation.name;
                Instantiate(
                    repair,
                    new Vector3(_sidewalkPositions[position], 1.557f * _stageSizes.y, 0),
                    Quaternion.identity).name = repair.name;
            }

            yield return Distance.WaitForDistance(waitDistance);
        }
    }

    private IEnumerator SpawnSidewalk()
    {
        while (!GameStatistics.IsGameOver)
        {
            Instantiate(
                    streetLight,
                    new Vector3(_sidewalkPositions[0], _stageSizes.y, 0),
                    Quaternion.identity).name = streetLight.name;
            var spawnedStreetLight = Instantiate(
                    streetLight,
                    new Vector3(_sidewalkPositions[1], _stageSizes.y, 0),
                    Quaternion.identity);
            spawnedStreetLight.name = streetLight.name;
            spawnedStreetLight.transform.Rotate(0, 180, 0);
            yield return Distance.WaitForDistance(0.9f * _stageSizes.y);
        }
    }

    private IEnumerator SpawnMoney()
    {
        while (!GameStatistics.IsGameOver)
        {
         var position = _roadPositions[_randomGen.Next(0, _roadPositions.Length)];
            for (var i = 0; i < 5; i++)
            {
                Instantiate(money,
                    new Vector3(position, _stageSizes.y + i * 0.1f * _stageSizes.y, 0),
                    Quaternion.identity).name = money.name;
            }
            yield return Distance.WaitForDistance(_stageSizes.y);
        }
    }
}