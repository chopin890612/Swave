using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks, IInput
{
    public static GameManager GM; // Test Update
    private BaseController nowController;
    private Scenes nowScene;

    public delegate void InputStautsHandler();
    public event InputStautsHandler ConfirmEvent;
    public event InputStautsHandler BackEvent;
    public event InputStautsHandler LeftEvent;
    public event InputStautsHandler RightEvent;

    private bool isPhone;

    public enum Scenes
    {
        開始封面 = 0,
        手機場景 = 1,
        檢查連動 = 2,
        選擇腳色 = 3,
        設定數值 = 4,
        操作教學 = 5,
        世界地圖 = 6,
        遊戲場景 = 7
        
    }
    public enum InputStaut
    {
        確認 = 0,
        取消 = 1,
        左 = 2,
        右 = 3,
        錯誤 = 100
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
        if (nowController != null)
            nowController.ChangeSceneEvent += ChangeSceneReceiver;

        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;

        nowScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
        if (nowScene == Scenes.手機場景)
        {
            isPhone = true;
        }
        else
        {
            if (nowScene != Scenes.開始封面)
            {
                ChangeSceneReceiver(0);
            }
        }

        PhotonNetwork.ConnectUsingSettings(); // Test Update 
    }
    void Update()
    {
        if (localPlayer != null)
        {
            localPlayer.SyncData(localPlayer.accelerometer);
            Debug.Log(InputStauts(otherPlayer.accelerometer));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlayer();
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
    void ChangeSceneReceiver(Scenes toScene)
    {
        nowScene = toScene;
        SceneManager.LoadScene((int)toScene);
    }

    #endregion

    #region InputManagement

    public InputStaut InputStauts(Vector3 value)
    {
        if (value.y > 30)
            return InputStaut.確認;
        else if (value.y < -30)
            return InputStaut.取消;
        else if (value.x > 30)
            return InputStaut.右;
        else if (value.x < -30)
            return InputStaut.左;
        else
            return InputStaut.錯誤;
    }

    #endregion

    #region PhotonManagment
    /// <summary>
    /// Just a player prefab aim for testing.
    /// </summary>
    string playerPrefab = "Testplayer";
    /// <summary>
    /// Replacing the class after creating a real player.
    /// </summary>
    public DataSyncingExm localPlayer;
    public DataSyncingExm otherPlayer;

    public void OnPlayerSpanw(DataSyncingExm p)
    {
        if (p.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("Updating");
            p.SyncData(p.accelerometer);
            localPlayer = p;
        }
        else
        {
            otherPlayer = p;
        }
    }
    public void OnPlayerDestroy(DataSyncingExm p)
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        p.SyncData(p.accelerometer);
        //}
    }

    /// <summary>
    /// Loading player prefab from resource folder by string path.
    /// </summary>
    public void SpawnPlayer() => PhotonNetwork.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0);
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SpawnPlayer();
        //if(isPhone)
        //    SpawnPlayer();
        //player = FindObjectOfType<DataSyncingExm>();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Pun connected");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Pun Disconnected");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }
    #endregion
}
