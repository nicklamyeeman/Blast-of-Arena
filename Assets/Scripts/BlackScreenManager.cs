using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenManager : MonoBehaviour
{
    public GameObject manager;

    private RoundManager rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = manager.GetComponent<RoundManager>();
    }

    public void EndOfAnimationEvent()
    {
        rd.ChangeRound();
    }
}
