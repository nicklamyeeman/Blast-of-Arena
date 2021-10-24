using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    private Text roundText;
    private Animator roundAnim;

    public RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        roundText = GetComponent<Text>();
        roundText.text = "Round " + (SceneManager.GetActiveScene().buildIndex - 1).ToString();
        roundAnim = GetComponent<Animator>();
    }

    public void changeRoundText(int roundTimer)
    {
        int hasRepeated = roundAnim.GetInteger("repeatedTime");

        if (hasRepeated > 0)
        {
            Debug.Log("RoundTimer: " + roundTimer + " HasRepeated: " + hasRepeated);
            roundTimer -= hasRepeated;
            roundText.text = roundTimer.ToString();
        }
        roundAnim.SetInteger("repeatedTime", hasRepeated + 1);
        if (hasRepeated > 3)
            roundManager.DeactivateRoundAnimation();
    }
}
