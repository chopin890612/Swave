using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class BaseController : MonoBehaviour
{
    public delegate void ChangeSceneHandler(GameManager.Scenes toScene);
    public event ChangeSceneHandler ChangeSceneEvent;

    public delegate void AudioHandler(string clipName);
    public event AudioHandler BGMEvent;
    public event AudioHandler SFXEvent;

    public void ChangeScene(GameManager.Scenes scenes)
    {
        ChangeSceneEvent(scenes);
    }
    public void PlayBGM(string clipName)
    {
        BGMEvent(clipName);
    }
    public void PlaySFX(string clipName)
    {
        SFXEvent(clipName);
    }
}
