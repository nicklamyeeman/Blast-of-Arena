using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();   
    }

    public void SetSpawnState(bool spawnState)
    {
        playerAnimator.SetBool("HasSpawn", spawnState);
    }
}
