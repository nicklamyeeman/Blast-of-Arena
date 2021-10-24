using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomListing _rootListing;
    [SerializeField]
    private GameUIHandler _canvaHandler;

    private List<RoomListing> _listings = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Fetch Rooms");
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = Instantiate(_rootListing, _content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        ExitGames.Client.Photon.Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        cp["score"] = 0;
        PhotonNetwork.SetPlayerCustomProperties(cp);
        print(PhotonNetwork.LocalPlayer.CustomProperties["score"]);
        _content.DestroyChildren();
        _listings.Clear();
        _canvaHandler.SwitchScreen();

    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby");
    }

    public override void OnLeftLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClick_RefreshGames()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        else
            PhotonNetwork.LeaveLobby();
    }
}
