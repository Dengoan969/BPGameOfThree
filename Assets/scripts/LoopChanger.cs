using UnityEngine;

public class LoopChanger : MonoBehaviour
{
    private bool isLoopPressed;
    public void LoopCurrentSong()
    {
        isLoopPressed = !isLoopPressed;
        Debug.Log($"Loop turned to {isLoopPressed}");
        GameObject
            .FindGameObjectWithTag(PlayerPrefs.GetString("CurrentMusic"))
            .GetComponent<AudioSource>()
            .loop = isLoopPressed;
    }
}
