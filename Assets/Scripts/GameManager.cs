using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;

public class GameManager : MonoBehaviourPunCallbacks, IInput
{
    public static GameManager GM; // Test Update
    private BaseController nowController;
   
    public enum Scenes
    {
        開始封面 = 0,
        檢查連動 = 1,
        選擇腳色 = 2,
        設定數值 = 3,
        操作教學 = 4,
        世界地圖 = 5,
        遊戲場景 = 6,
        手機場景 = 10
    }
    public enum InputStaut
    {
        確認 = 0,
        取消 = 1,
        左 = 2,
        右 = 3,
        空 = 10,
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
        {
            nowController.ChangeSceneEvent += ChangeSceneReceiver;
            nowController.BGMEvent += PlayBGMReceiver;
            nowController.SFXEvent += PlaySFXReceiver;
        }

        #region SceneManagement Inti
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;

        nowScene = (Scenes)SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().name == "10_手機場景")
        {
            Debug.LogWarning("手機啦!!");
        }
        else
        {
            if (nowScene != Scenes.開始封面)
            {
                ChangeSceneReceiver(0);
            }
        }
        #endregion

        AudioInti();
        PlayBGMReceiver("BGM");
        PlayBGMReceiver("Seagull");

        ConfirmEvent += StupidFunc;
        BackEvent += StupidFunc;
        LeftEvent += StupidFunc;
        RightEvent += StupidFunc;

        PhotonNetwork.ConnectUsingSettings(); // Test Update 
    }
    void Update()
    {
        #region PC DEBUGGING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE");
            otherPlayer = Instantiate(new GameObject()).AddComponent<DataSyncingExm>();
            otherPlayer.transform.SetParent(transform);
        }
        if (Input.GetKeyDown(KeyCode.W))
            ConfirmEvent();
        if (Input.GetKeyDown(KeyCode.A))
            LeftEvent();
        if (Input.GetKeyDown(KeyCode.S))
            BackEvent();
        if (Input.GetKeyDown(KeyCode.D))
            RightEvent();

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayBGMReceiver("BGM");
            PlayBGMReceiver("Seagull");
        }

        #endregion


        if (localPlayer != null)
        {
            localPlayer.SyncData(localPlayer.accelerometer);
            if (Time.time > nowInputTime)
            {
                nowStaut = InputStauts(otherPlayer.accelerometer);
                if (nowStaut != InputStaut.空)
                {
                    nowInputTime = Time.time + inputTimeDelay;
                    Debug.Log(nowStaut);
                }
            }
        }


    }

    #region SceneManagement

    private Scenes nowScene;

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.LogWarning("Load" + scene.name);
        nowController = FindObjectOfType<BaseController>();
        if (nowController != null)
        {
            nowController.ChangeSceneEvent += ChangeSceneReceiver;
            nowController.BGMEvent += PlayBGMReceiver;
            nowController.SFXEvent += PlaySFXReceiver;
        }
    }
    void OnSceneUnload(Scene scene)
    {
        Debug.LogWarning("Unload" + scene.name);
        if (nowController != null)
        {
            nowController.ChangeSceneEvent -= ChangeSceneReceiver;
            nowController.BGMEvent -= PlayBGMReceiver;
            nowController.SFXEvent -= PlaySFXReceiver;
        }
    }
    void ChangeSceneReceiver(Scenes toScene)
    {
        nowScene = toScene;
        SceneManager.LoadScene((int)toScene);
    }

    #endregion

    #region InputManagement
    
    public delegate void InputStautsHandler();
    public event InputStautsHandler ConfirmEvent;
    public event InputStautsHandler BackEvent;
    public event InputStautsHandler LeftEvent;
    public event InputStautsHandler RightEvent;

    [Header("InputManagement")]
    [Space]
    public float inputTimeDelay = 1f;
    private float nowInputTime;
    private InputStaut nowStaut;
    private void StupidFunc() { }
    public InputStaut InputStauts(Vector3 value)
    {
        if (value.y > 30)
        {
            ConfirmEvent();
            return InputStaut.確認;
        }
        else if (value.y < -30)
        {
            BackEvent();
            return InputStaut.取消;
        }
        else if (value.x > 30)
        {
            RightEvent();
            return InputStaut.右;
        }
        else if (value.x < -30)
        {
            LeftEvent();
            return InputStaut.左;
        }
        else
        {
            return InputStaut.空;
        }
    }

    #endregion

    #region AudioManagement

    [Header("AudioManagement")]
    [Space]
    public AudioClip[] BGM;
    private List<AudioSource> BGMList = new List<AudioSource>();

    public AudioClip[] SFX;
    private List<AudioSource> SFXList = new List<AudioSource>();

    public GameObject BGMPlayer;
    public GameObject SFXPlayer;

    public void PlayBGMReceiver(string clipName)
    {
        for(int i =0; i < BGM.Length; i++)
        {
            if(BGM[i].name == clipName)
            {
                BGMList[i].Play();
            }
        }
    }
    public void PlaySFXReceiver(string clipName)
    {
        for (int i = 0; i < SFX.Length; i++)
        {
            if (SFX[i].name == clipName)
            {
                SFXList[i].Play();
            }
        }
    }
    private void AudioInti()
    {
        int index = 0;
        foreach (AudioClip i in BGM)
        {
            BGMList.Add(BGMPlayer.AddComponent<AudioSource>());
            BGMList[index].clip = i;
            BGMList[index].playOnAwake = false;
            BGMList[index].loop = true;
            index++;
        }
        index = 0;
        foreach (AudioClip i in SFX)
        {
            SFXList.Add(SFXPlayer.AddComponent<AudioSource>());
            SFXList[index].clip = i;
            SFXList[index].playOnAwake = false;
            index++;
        }
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
    [Header("PhotonManagement")]
    [Space]
    public DataSyncingExm localPlayer;
    public DataSyncingExm otherPlayer;

    public void OnPlayerSpanw(DataSyncingExm p)
    {
        p.transform.SetParent(transform);
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
    public GameObject SpawnPlayer() { return PhotonNetwork.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, 0); }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SpawnPlayer();
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