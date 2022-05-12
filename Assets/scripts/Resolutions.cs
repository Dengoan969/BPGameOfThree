using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolutions : MonoBehaviour
{
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;
    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        dropdown.ClearOptions();
        foreach (var res in rsl)
        {
            resolutions.Add(res.width + "x" + res.height);
        }
        dropdown.AddOptions(resolutions);
    }
    
    
    public void ChangeResolution(int newRes) 
        => Screen.SetResolution(rsl[newRes].width, rsl[newRes].height, Screen.fullScreen);
}
