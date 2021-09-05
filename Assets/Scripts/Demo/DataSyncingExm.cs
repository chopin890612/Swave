using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DataSyncingExm : MonoBehaviourPun
{
    public Vector3 accelerometer = Vector3.zero;

    public void AssignData(Vector3 acc)
    {
        accelerometer = acc;
    }

    #region Raise_Event in Selecting Role/Map
    public const byte SelectChangeEventCodeID = 0;
    void NetworkingClient_EventRecevied_Test(EventData obj)
    {
        if (obj.Code == SelectChangeEventCodeID)
        {
            object[] data = (object[])obj.CustomData;
            int check = (int)data[0];
            Vector3 acc = (Vector3)data[1];

            if (base.photonView.ViewID == check)
            {
                AssignData(acc);
                //Debug.Log("ID: " + check + acc);
            }
        }
    }
    public void SyncData(Vector3 acc)
    {
        object[] data = new object[] { base.photonView.ViewID, acc};
        AssignData(acc);
        PhotonNetwork.RaiseEvent(SelectChangeEventCodeID, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);        
    }
    #endregion

    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            accelerometer = new Vector3(Input.acceleration.x * 100, Input.acceleration.y * 100, Input.acceleration.z * 100);
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventRecevied_Test;

        if (GameManager.GM is GameManager gm)
        {
            gm.OnPlayerSpanw(this);
        }
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventRecevied_Test;
        if (GameManager.GM is GameManager gm)
        {
            gm.OnPlayerDestroy(this);
        }
    }
}
