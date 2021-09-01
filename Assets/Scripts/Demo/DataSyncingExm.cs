using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DataSyncingExm : MonoBehaviourPun
{
    public int id = 0;
    public Vector3 accelerometer = Vector3.zero;

    public void AssignMyID(int id) => this.id = id;
    public void AssignAcc(Vector3 acc) => accelerometer = acc;

    #region Raise_Event in Selecting Role/Map
    public const byte SelectChangeEventCodeID = 0;
    public const byte UpdateAccelerometerEventCodeID = 1;
    void NetworkingClient_EventRecevied_Test(EventData obj)
    {
        if (obj.Code == SelectChangeEventCodeID)
        {
            object[] data = (object[])obj.CustomData;
            int check = (int)data[0];
            int id = (int)data[1];

            if (base.photonView.ViewID == check)
            {
                AssignMyID(id);
                Debug.Log(id);
            }
        }
        else if(obj.Code == UpdateAccelerometerEventCodeID)
        {
            object[] data = (object[])obj.CustomData;
            int check = (int)data[0];
            Vector3 acc = (Vector3)data[1];

            if (base.photonView.ViewID == check)
            {
                AssignAcc(acc);
                Debug.Log("ID: " + id + acc + check);
            }
        }
    }
    public void SyncID(int id)
    {
        object[] data = new object[] { base.photonView.ViewID, id };
        AssignMyID(id);
        PhotonNetwork.RaiseEvent(SelectChangeEventCodeID, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);        
    }
    public void SyncAcc(Vector3 acc)
    {
        object[] data = new object[] { base.photonView.ViewID, acc };
        AssignAcc(acc);
        PhotonNetwork.RaiseEvent(UpdateAccelerometerEventCodeID, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
    #endregion

    void Update()
    {
        accelerometer.x = Input.acceleration.x * 100;
        accelerometer.y = Input.acceleration.y * 100;
        accelerometer.z = Input.acceleration.z * 100;
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
