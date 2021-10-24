using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MasterClientObjects : MonoBehaviour
{
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            gameObject.SetActive(false);
    }
}
