using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TestSuite
{
    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("GameScene");
    }

    [UnityTest]
    public IEnumerator TestIncreaseSpeed()
    {
        var initialSpeed = MainCar.Speed;
        yield return new WaitForSeconds(1f);
        Assert.Greater(MainCar.Speed, initialSpeed);
    }
    
    [UnityTest]
    public IEnumerator TestCarsSpawn()
    {
        var i = 0;
        while (i < 60 && GameObject.FindWithTag("Car") == null)
        {
            i++;
            yield return new WaitForSeconds(1f);
        }
    }
    
    [UnityTest]
    public IEnumerator TestDeadObstacleSpawn()
    {
        var i = 0;
        while (i < 60 && GameObject.FindWithTag("DeadObstacle") == null)
        {
            i++;
            yield return new WaitForSeconds(1f);
        }
    }
    
    [UnityTest]
    public IEnumerator TestLampObstacleSpawn()
    {
        var i = 0;
        while (i < 60 && GameObject.FindWithTag("LampObstacle") == null)
        {
            i++;
            yield return new WaitForSeconds(1f);
        }
    }

    [UnityTest]
    public IEnumerator TestBalanceIncrease()
    {
        var spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        var money = spawner.money;
        var tempBalance = GameStatistics.Balance;
        var spawnedMoney = Object.Instantiate(money, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedMoney.name = money.name;
        yield return new WaitForSeconds(2f);
        Assert.Greater(GameStatistics.Balance, tempBalance);
    }
    
    [UnityTest]
    public IEnumerator TestFuelDecrease()
    {
        var initialFuel = GameStatistics.Fuel;
        yield return new WaitForSeconds(3f);
        Assert.Less(GameStatistics.Fuel, initialFuel);
    }
    
    [UnityTest]
    public IEnumerator TestEnduranceDecrease()
    {
        var spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        var initialEndurance = GameStatistics.Endurance;
        var gnome = spawner.roadPatterns
            .First(x => x.CompareTag("Obstacle"));
        var spawnedGnome = Object.Instantiate(gnome, new Vector3(50, 0, 0), Quaternion.identity);
        spawnedGnome.name = gnome.name;
        yield return new WaitForSeconds(2f);
        Assert.Less(GameStatistics.Endurance, initialEndurance);
    }
}