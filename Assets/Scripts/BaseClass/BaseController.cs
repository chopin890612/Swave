using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseController : MonoBehaviour
{
    public delegate void ChangeSceneHandler(GameManager.Scenes toScene);
    public event ChangeSceneHandler ChangeSceneEvent;
    public void ChangeScene(int i)
    {
        ChangeSceneEvent((GameManager.Scenes)i);
    }
}
