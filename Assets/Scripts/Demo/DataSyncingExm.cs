using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class DataSyncingExm : MonoBehaviourPun
{
    public int id = 0;

    public void AssignMyID(int id) => this.id = id;

    #region Raise_Event in Selecting Role/Map
    public const byte SelectChangeEventCodeID = 0;
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
    }
    public void SyncID(int id)
    {
        object[] data = new object[] { base.photonView.ViewID, id };
        AssignMyID(id);
        PhotonNetwork.RaiseEvent(SelectChangeEventCodeID, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
    #endregion

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
