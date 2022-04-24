using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    private float speed = 6f;
    public Transform player;
    public float delta = 0.05f;
    void Update()
    {
        // TODO make PAUSE at escape
        if (!GameStatistics.IsGameOver)
        {
            if (MainCar.speed % 10 == 0 && Math.Abs(MainCar.speed - 50f) > 10e-9)
                delta += 0.01f;
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 100f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, -delta, -10f);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                var rotation = Quaternion.Euler(0f, 0f, 80f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
                var position = player.position;
                player.position = MoveInsideBounds(position, delta, 10f);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                StopAllTracksByTag("LevelOneMusic");
                Destroy(GameObject.FindGameObjectWithTag("LevelOneMusic"));
                GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<UniMusic>().PlayMusic();
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                var defaultRot = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, speed * Time.deltaTime);
            }
        }
    }
    
    private Vector3 MoveInsideBounds(Vector3 pos, float inpDelta, float bound)
    {
        if (bound < 0)
            return (pos + new Vector3(inpDelta, 0, 0)).x >= bound
                ? player.position + new Vector3(inpDelta, 0, 0)
                : player.position + Vector3.zero;
        return (pos + new Vector3(inpDelta, 0, 0)).x <= bound
            ? player.position + new Vector3(inpDelta, 0, 0)
            : player.position + Vector3.zero;
    }
    
    private void StopAllTracksByTag(string inpTag)
    {
        var allTracksPlaying = GameObject.FindGameObjectsWithTag(inpTag);
        foreach (var track in allTracksPlaying)
        {
            track.GetComponent<UniMusic>().StopMusic();
        }
    }
}
