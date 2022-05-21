using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var track = PlayerPrefs.GetString("CurrentMusic", "LevelOneMusic");
        if (track == string.Empty || track is null)
        {
            GameObject.
                FindGameObjectWithTag("LevelOneMusic")
                .GetComponent<AudioSource>()
                .Play();
        }
        else
        {
            GameObject.
                FindGameObjectWithTag(track)
                .GetComponent<AudioSource>()
                .Play();
        }
    }
}
