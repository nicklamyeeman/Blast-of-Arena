using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;
    public Player PlayerInfo { get; private set; }
    public bool Ready = false;
    public RawImage _rdyImg;

    public void SetPlayerInfo(Player player)
    {
        PlayerInfo = player;
        SetPlayerText(player);
        _rdyImg.color = Ready ? Color.green : Color.red;
        if (player.IsMasterClient)
            _rdyImg.gameObject.SetActive(false);
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        if (target != null && target == PlayerInfo)
            if (changedProps.ContainsKey("RandomNumber"))
                SetPlayerText(target);
    }

    private void SetPlayerText(Player player)
    {
        int nb = -1;
        if (player.CustomProperties.ContainsKey("RandomNumber"))
            nb = (int)player.CustomProperties["RandomNumber"];
        _text.text = player.NickName + ((player.IsMasterClient) ? " -> Host" : "");

    }
}
