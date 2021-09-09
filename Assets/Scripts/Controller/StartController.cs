using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartController : BaseController
{
    public Button start;
    public Button keepingOn;
    private void Start()
    {
        start.onClick.AddListener(new UnityEngine.Events.UnityAction(() => ChangeScene(GameManager.Scenes.檢查連動)));
        keepingOn.onClick.AddListener(new UnityEngine.Events.UnityAction(() => ChangeScene(GameManager.Scenes.世界地圖)));
    }
}
