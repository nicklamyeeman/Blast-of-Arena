using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] _roomListingCanvas;
    [SerializeField]
    GameObject _playerListingCanvas;

    public void SwitchScreen()
    {
        foreach (GameObject canvas in _roomListingCanvas)
            canvas.SetActive(false);
        _playerListingCanvas.SetActive(true);
    }

    public void LeaveRoom()
    {
        _roomListingCanvas[0].SetActive(true);
        _playerListingCanvas.SetActive(false);
    }

}
