using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LastMenuScore : MonoBehaviour
{
    private Text playerScoreText;

    void Start()
    {
        playerScoreText = GetComponent<Text>();
        Player[] allPlayers = PhotonNetwork.PlayerList;

        foreach (Player player in allPlayers)
        {
            int playerScore = (int)player.CustomProperties["score"];
            playerScoreText.text += player.NickName + ":  " + playerScore.ToString() + "\n\n";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveLobby();
            SceneManager.LoadScene(1);
        }
    }
}
