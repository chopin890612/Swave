using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    public Text text;
    public float delayTime = 0.1f;
    private float nextTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            float x = Input.acceleration.x * 100;
            float y = Input.acceleration.y * 100;
            float z = Input.acceleration.z * 100;
            text.text = (int)x + "\n" + (int)y + "\n" + (int)z + "\n";
            nextTime = Time.time + delayTime;
        }
    }
}
