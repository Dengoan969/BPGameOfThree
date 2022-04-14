using UnityEditor.Build.Reporting;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    
    public float speed = 5f;
    public GameObject road;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -15f)
        {
            Instantiate(road, new Vector3(0f, 21f, 0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
