using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            Vector3 newPos = new Vector3(0, 0, 0);
            GameObject newPlayer = MasterManager.NetworkInstantiate(_prefab, newPos, Quaternion.identity);
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");
            int i = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            print("DEBUG :::: Length : " + spawns.Length + " nb = " + i);
            newPos = spawns[i].GetComponent<Transform>().position;
            print("DEBUG :::: Spawn at " + newPos);
            newPlayer.GetComponent<CameraCheck>().PlayerPosition = newPos;
        }
    }
}