using UnityEngine;

public class StageSizes
{
    private static Vector3 stageSizes;
    private static bool isSetted;

    public static Vector3 GetStageSizes()
    {
        if(!isSetted)
        {
            stageSizes = 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            isSetted = true;
        }
        return stageSizes;
    }
}
