using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PGM : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private GameObject playerPrefab;

    public List<PlayerM> playersList = new List<PlayerM>();
    public Text chat;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Start");

            return;
        }
        if (PlayerM.localPlayer == null)
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    private void Update()
    {

    }
    public void MessageReceiver(GameObject sender, string message)
    {
        chat.text = chat.text + "\n" + sender.name + message;
        Debug.Log(sender.name + message);
    }
    // 玩家離開遊戲室時, 把他帶回到遊戲場入口
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Start");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(!stream.IsWriting)
            chat.text = (string)stream.ReceiveNext();

    }
}
