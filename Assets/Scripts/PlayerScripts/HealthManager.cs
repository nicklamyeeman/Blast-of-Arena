using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    private bool isDead = false;
    private int maxHealth;
    private int currentHealth;
    public bool isInvicible = false;

    public HealthBar healthBar;
    private RoundManager roundManager;
    private AnimatorControllerOui playerAnimator;
    private VanishAbility vanishAbility;
    public GameObject combatText;

    private string _lastDealer;
    public string Killer { get { return _lastDealer; } }

    void Start()
    {
        maxHealth = GetComponent<ClassBase>().maxHealth;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerAnimator = GetComponent<AnimatorControllerOui>();
        vanishAbility = GetComponent<VanishAbility>();
        roundManager = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();

    }

    public void TakeDamage(int damage, string from)
    {
        //if (isDead == true)
        //    return;
        // Debug.Log("Current Health: " + currentHealth + " and damage taken: " + damage);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (vanishAbility.isVanished == true)
        {
            vanishAbility.CancelVanish();
        }
        playerAnimator.SetHitAnimation();
        GameObject ct = Instantiate(combatText, transform.position, Quaternion.identity);
        ct.transform.parent = gameObject.transform;
        ct.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        ct.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        //if (GetComponentInParent<PlayerIdentity>().photonView.IsMine)
        //    CinemachineManager.Instance.ShakeCamera(10f, .2f);
        GetComponent<AnimatorControllerOui>().SetHitAnimation();

        if (currentHealth <= 0 && isDead == false)
        {
            _lastDealer = from;
            Death();
        }
    }

    private void Death()
    {
        string name = GetComponentInParent<PlayerIdentity>().ID;
        isDead = true;
        roundManager.removePlayer(name, Killer);
        playerAnimator.SetDeathAnimation();
        // Destroy(gameObject);
    }

    public bool IsPlayerDead()
    {
        return isDead;
    }

}