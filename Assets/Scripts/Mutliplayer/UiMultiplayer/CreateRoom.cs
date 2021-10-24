using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;
    [SerializeField]
    private GameUIHandler _canvaHandler;

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        options.PublishUserId = true;
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        print("Created room sucessfully.");
        ExitGames.Client.Photon.Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["score"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(cp);
        print(PhotonNetwork.LocalPlayer.CustomProperties["score"]);

        _canvaHandler.SwitchScreen();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room creation failed.");
    }
}
