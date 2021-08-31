using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class BaseController : MonoBehaviourPunCallbacks
{
    public delegate void ChangeSceneHandler(GameManager.Scenes toScene);
    public event ChangeSceneHandler ChangeSceneEvent;
    public void ChangeScene(int i)
    {
        ChangeSceneEvent((GameManager.Scenes)i);
    }
}
