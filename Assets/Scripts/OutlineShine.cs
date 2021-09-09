using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class OutlineShine : MonoBehaviour
{
    private Outline outline;
    private float delayTime = 0.2f;
    private float nowTime;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nowTime)
        {
            outline.enabled = false;
        }
    }
    public void OnShine()
    {
        outline.enabled = true;
        nowTime = Time.time + delayTime;
    }
}
