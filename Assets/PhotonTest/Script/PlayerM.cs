using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerM : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameObject localPlayer;

    public PGM pgm;
    public delegate void MessageHandler(GameObject sender, string message);
    public event MessageHandler MessageEvent;

    [SerializeField]
    private int playerId;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Text chatLog;

    #region SendEvent
    protected void OnMessageSend(string messageLog)
    {
        MessageEvent(gameObject, messageLog);
    }
    public void MessageSend(InputField inputField)
    {
        OnMessageSend(inputField.text);
    }
    #endregion

    private void Awake()
    {
        //追蹤Localplayer避免在同步時實例化
        if (photonView.IsMine)
            localPlayer = gameObject;

        //DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        pgm = FindObjectOfType<PGM>().GetComponent<PGM>();
        playerId = pgm.playersList.Count;
        pgm.playersList.Add(this);

        MessageEvent += pgm.MessageReceiver;
    }

    void Update()
    {

    }
    public void Chat()
    {
        chatLog.text += inputField.text + "\n";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(chatLog.text);
        }
        else
        {
            chatLog.text = (string)stream.ReceiveNext();
        }
    }
}
