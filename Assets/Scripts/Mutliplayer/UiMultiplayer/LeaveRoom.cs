using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaveRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameUIHandler _canvasHandler;

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

    public override void OnLeftRoom()
    {
        _canvasHandler.LeaveRoom();
    }
}
