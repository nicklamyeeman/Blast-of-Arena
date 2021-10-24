using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
public class RoundManager : MonoBehaviourPun
{
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject roundAnimationUI;
    [SerializeField] private GameObject endOfRoundAnimationUI;

    private int nbOfPlayer = 0;
    private GameObject[] playerEntities;

    void Start()
    {
        GetNbOfPlayers();
        DeactivatePlayers();
    }

    private void Update()
    {
    }

    private void AddScore(int score, string player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Player[] allPlayers = PhotonNetwork.PlayerList;
            Player photonPlayer = Array.Find(allPlayers, elem => elem.NickName == player);
            if (player != string.Empty && player != "")
            {
                ExitGames.Client.Photon.Hashtable cp = photonPlayer.CustomProperties;
                int newScore = (int)cp["score"];
                newScore += score;
                cp["score"] = newScore;
                photonPlayer.SetCustomProperties(cp);
            }
        }
    }

    private void GetNbOfPlayers()
    {
        playerEntities = GameObject.FindGameObjectsWithTag("PlayerEntity");
        nbOfPlayer = playerEntities.Length;
    }

    public void DeactivateRoundAnimation()
    {
        roundAnimationUI.SetActive(false);
        foreach (GameObject playerEntity in playerEntities)
        {
            playerEntity.SetActive(true);
        }
        GetNbOfPlayers();
    }
    
    private void DeactivatePlayers()
    {
        foreach (GameObject playerEntity in playerEntities)
        {
            playerEntity.SetActive(false);
        }
    }

    private void LaunchEndOfRoundAnimation(string killer)
    {
        Player[] allPlayers = PhotonNetwork.PlayerList;
        RoundWinnerData rwd = endOfRoundAnimationUI.GetComponent<RoundWinnerData>();

        endOfRoundAnimationUI.SetActive(true);
        rwd.SetWinnerText(killer);

        foreach (Player player in allPlayers)
        {
            int playerScore = (int)player.CustomProperties["score"];
            rwd.SetScorePanelText(player.NickName + ": " + playerScore.ToString());
        }


    }

    public void ChangeRound()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        if (!PhotonNetwork.IsConnected)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void removePlayer(string dead, string killer)
    {
        print("Player : " + dead + " is dead by " + killer);
        nbOfPlayer -= 1;
        if (nbOfPlayer <= 1)
        {
            AddScore(1, killer);
            LaunchEndOfRoundAnimation(killer);
        }
    }

    public void SetDamageToClients(string playerId, int dmg, string source)
    {
        if (PhotonNetwork.IsConnected)
            photonView.RPC("RPC_ReceiveDamage", RpcTarget.Others, playerId, dmg, source);
    }

    public void SetInvisibilityToClients(string playerId)
    {
        if (PhotonNetwork.IsConnected)
            photonView.RPC("RPC_SetInvisibilityToPlayer", RpcTarget.AllViaServer, playerId);
    }

    public void InstantiateSpellForClients(string playerId, GameObject ability, Vector3 pos, Quaternion rota)
    {
        if (PhotonNetwork.IsConnected)
            photonView.RPC("RPC_InstatiateSpellFromPlayer", RpcTarget.AllViaServer, playerId, ability.gameObject.name, pos, rota);
    }

    [PunRPC]
    private void RPC_ReceiveDamage(string target, int dmg, string source)
    {
        if (PhotonNetwork.IsConnected)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerEntity");
            GameObject obj = Array.Find(players, elem => elem.name == "PlayerEntity(Clone)" && elem.GetComponent<PlayerIdentity>().ID == target);
            if (obj)
                obj.GetComponentInChildren<HealthManager>().TakeDamage(dmg, source);
        }
    }

    [PunRPC]
    private void RPC_SetInvisibilityToPlayer(string target)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerEntity");
        GameObject tobj = Array.Find(players, elem => elem.name == "PlayerEntity(Clone)" && elem.GetComponent<PlayerIdentity>().ID == target);
        if (tobj)
        {
            float alpha = (tobj.GetComponent<PhotonView>().IsMine) ? 0.4f : 0f;
            tobj.GetComponentInChildren<VanishAbility>().SetVanish(alpha);
        }
    }

    [PunRPC]
    private void RPC_InstatiateSpellFromPlayer(string target, string ability, Vector3 pos, Quaternion rota)
    {
        print("Player Attacking : " + target);
        print("Ability :" + ability);
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerEntity");
        GameObject player = Array.Find(players, elem => elem.name == "PlayerEntity(Clone)" && elem.GetComponent<PlayerIdentity>().ID == target);
        player = player.GetComponentInChildren<PlayerController>().gameObject;
        if (player)
        {
            GameObject spell = MasterManager.GetPrefab(ability);
            print("Try user 2 " + spell.GetComponent<SpellIdentity>().From);
            spell = Instantiate(spell, pos, rota);
            spell.GetComponent<SpellIdentity>().From = target;
            if (spell.name != "NinjaBomb(Clone)")
                spell.transform.parent = player.transform;
        }
    }

}
