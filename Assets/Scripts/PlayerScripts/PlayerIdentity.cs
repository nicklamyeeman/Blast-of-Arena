using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerIdentity : MonoBehaviourPun
{
    public Text playerName;
    private string _id = "";
    public string ID { get { return _id; } }

    public void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            _id = photonView.Owner.NickName;
            playerName.text = photonView.Owner.NickName;
        }
    }
}
