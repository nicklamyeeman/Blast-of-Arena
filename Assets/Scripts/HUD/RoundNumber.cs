using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundNumber : MonoBehaviour
{
    private int number;
    public Text roundText;

    // Start is called before the first frame update
    void Start()
    {
       number = SceneManager.GetActiveScene().buildIndex;
       number -= 1;
       roundText.text = "Round " + number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
