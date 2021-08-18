using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public delegate void ChangeSceneHandler(int toScene);
    public event ChangeSceneHandler ChangeSceneEvent;
    public void ChangeScene(int i)
    {
        ChangeSceneEvent(i);
    }
}
