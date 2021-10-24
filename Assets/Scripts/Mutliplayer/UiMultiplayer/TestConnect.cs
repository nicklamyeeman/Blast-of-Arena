using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class TestConnect : MonoBehaviourPunCallbacks
{
    private Text _text;

    /* Connect to the master server
     * All players should load the scene together
     */
    void Start()
    {
        _text = GetComponent<Text>();
        print("Connecting to server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    /* Connected to Master Server Callback
     * Join Lobby if connected
     */
    public override void OnConnectedToMaster()
    {
        print("Connected to Master.");
        _text.text = "Connected to server";
        print(PhotonNetwork.LocalPlayer.NickName);
    }

    public void OnClick_JoinLobby()
    {
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnLeftLobby()
    {
        _text.text = "Left lobby";
    }

    public override void OnJoinedLobby()
    {
        _text.text = "Connected to Lobby";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected : " + cause.ToString());
        _text.text = "Disconnected from server";
    }
}
