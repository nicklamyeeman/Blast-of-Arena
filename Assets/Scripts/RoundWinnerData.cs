using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RoundWinnerData : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    [SerializeField] private Text scorePanel;

    public void SetWinnerText(string winner)
    {
        Debug.Log("Hello in set text");
        winnerText.text = winner;
    }

    public void SetScorePanelText(string playerScore)
    {
        scorePanel.text += playerScore + "\n \n";
    }
}
