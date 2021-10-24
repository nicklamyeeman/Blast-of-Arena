using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

    public void OnClick_JoinRoom()
    {
        print("Trying to join room: " + RoomInfo.Name);
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
