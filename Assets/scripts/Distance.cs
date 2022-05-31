using System.Collections;
using UnityEngine;

public class Distance
{
    public static IEnumerator WaitForDistance(float distance)
    {
        
        var currentDistance = 0f;
        while (currentDistance < distance)
        {
            currentDistance += MainCar.Speed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
