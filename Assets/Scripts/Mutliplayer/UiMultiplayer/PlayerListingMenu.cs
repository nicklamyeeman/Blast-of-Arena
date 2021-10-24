using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _rootListing;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private bool _ready = false;

    private void Awake()
    {
        GetCurrentPlayerList();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        //_readyText.text = (state == true) ? "R" : "N";
    }

    private void GetCurrentPlayerList()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null ||
            PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach(KeyValuePair<int, Player> elem in PhotonNetwork.CurrentRoom.Players)
            AddPlayer(elem.Value);
    }

    private void AddPlayer(Player player)
    {
        PlayerListing listing = Instantiate(_rootListing, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayer(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.PlayerInfo == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public override void OnLeftRoom()
    {
        _content.DestroyChildren();
        _listings.Clear();
    }

    private bool AreOtherPlayerReady()
    {
        for (int i = 0; i < _listings.Count; i += 1)
        {
            if (_listings[i].PlayerInfo != PhotonNetwork.LocalPlayer)
                if (!_listings[i].Ready)
                    return false;
        }
        print("All players ready");
        return true;
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient && AreOtherPlayerReady() == true)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            Scene toLoad = SceneManager.GetActiveScene();
            PhotonNetwork.LoadLevel(toLoad.buildIndex + 1);
        }
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            print("PhotonView ID :" + photonView.name);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, _ready);
        }
    }

    //RPCs let the client deliver and receive messsages through the network
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        print("READY");
        int index = _listings.FindIndex(x => x.PlayerInfo == player);
        if (index != -1)
        {
            _listings[index].Ready = ready;
            _listings[index].SetPlayerInfo(_listings[index].PlayerInfo);
        }
    }

}
