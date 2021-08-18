using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    private BaseController nowController;
    private int nowScene;
    public enum Scenes
    {
        開始封面 = 0,
        檢查連動 = 1,
        選擇腳色 = 2,
        設定數值 = 3,
        操作教學 = 4,
        世界地圖 = 5,
        遊戲場景 = 6
    }
    void Awake()
    {
        #region Singleton Init
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("More than 1 GM.");
            Destroy(gameObject);
        }
        #endregion
    }
    void Start()
    {
        //Get the Controller in Scene
        nowController = FindObjectOfType<BaseController>();

        //Add receiver to handle Controller's event 
        if(nowController != null)
            nowController.ChangeSceneEvent += ChangeSceneReceiver;

        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;

        nowScene = SceneManager.GetActiveScene().buildIndex;
        if (nowScene != (int)Scenes.開始封面)
        {
            ChangeSceneReceiver(0);
        }
    }

    #region SceneManagement

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.LogWarning("Load" + scene.name);
        nowController = FindObjectOfType<BaseController>();
        if (nowController != null)
            nowController.ChangeSceneEvent += ChangeSceneReceiver;
    }
    void OnSceneUnload(Scene scene)
    {
        Debug.LogWarning("Unload" + scene.name);
        if (nowController != null)
            nowController.ChangeSceneEvent -= ChangeSceneReceiver;
    }
    void ChangeSceneReceiver(int toScene)
    {
        nowScene = toScene;
        SceneManager.LoadScene(toScene);
    }

    #endregion

    #region InputManagement

    #endregion
}
