using UnityEngine;
using Photon.Pun;

public class OtherClientsObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
            gameObject.SetActive(false);
    }
}
