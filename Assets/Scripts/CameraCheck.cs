using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraCheck : MonoBehaviourPun
{
    [SerializeField]
    private GameObject[] _toDesac;
    [SerializeField]
    private GameObject _player;

    public Vector3 PlayerPosition {
        get { return _player.transform.position; }
        set { _player.GetComponent<Transform>().position = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (!base.photonView.IsMine)
        {
            foreach (GameObject obj in _toDesac)
                obj.SetActive(false);
        }
        else
        {
            _player.layer = LayerMask.NameToLayer("Default");
            _player.applyRecursiveOnDescendants(child => child.layer = LayerMask.NameToLayer("Default"));
            foreach (Transform childTrans in _player.GetComponentsInChildren<Transform>())
            {
                if (childTrans.gameObject.name == "SpritePivot")
                    childTrans.gameObject.applyRecursiveOnDescendants(child => child.layer = LayerMask.NameToLayer("BehindMask"));
            }

        }
        print("Layer player =" + _player.layer);
    }
}
