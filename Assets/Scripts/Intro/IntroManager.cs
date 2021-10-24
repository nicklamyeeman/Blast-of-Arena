using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _video;
    [SerializeField]
    private GameObject _lauchScreen;
    [SerializeField]
    private float _time = 5f;

    void Awake()
    {
        Invoke("SwitchToPlayScreen", _time);
    }

    private void SwitchToPlayScreen()
    {
        //_video.SetActive(false);
        _lauchScreen.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKeyDown)
            SwitchToPlayScreen();
    }
}
