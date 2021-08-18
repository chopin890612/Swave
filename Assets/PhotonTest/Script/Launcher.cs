using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Tooltip("�C���Ǫ��a�H�ƤW��. ��C���Ǫ��a�H�Ƥw���B, �s���a�u��s�}�C���ǨӶi��C��.")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [SerializeField]
    private Text log;

    string gameVersion = "1";
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Connect()
    {
        // �ˬd�O�_�P Photon Cloud �s�u
        if (PhotonNetwork.IsConnected)
        {
            // �w�s�u, �|���H���[�J�@�ӹC����
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // ���s�u, �|�ջP Photon Cloud �s�u
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN �I�s OnConnectedToMaster(), �w�s�W Photon Cloud.");
        log.text = "PUN �I�s OnConnectedToMaster(), �w�s�W Photon Cloud.";

        // �T�{�w�s�W Photon Cloud
        // �H���[�J�@�ӹC����
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN �I�s OnDisconnected() {0}.", cause);
        log.text = "PUN �I�s OnDisconnected()" + cause;
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN �I�s OnJoinRandomFailed(), �H���[�J�C���ǥ���.");
        log.text = "PUN �I�s OnJoinRandomFailed(), �H���[�J�C���ǥ���.";

        // �H���[�J�C���ǥ���. �i���]�O 1. �S���C���� �� 2. ���������F.    
        // �n�a, �ڭ̦ۤv�}�@�ӹC����.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom }) ;
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("PUN �I�s OnJoinedRoom(), �w���\�i�J�C���Ǥ�.");
        log.text = "PUN �I�s OnJoinedRoom(), �w���\�i�J�C���Ǥ�.";
        PhotonNetwork.LoadLevel("GameRoom");

    }
}
