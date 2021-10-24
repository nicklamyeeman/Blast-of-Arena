using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerInput : MonoBehaviourPun
{
    public SpellBarController spellBarController;
    public Target target;
    private HealthManager healthmanager;
    private AnimatorControllerOui playerAnimator;
    private DetectDirection detectDirection;
    private PlayerController playerController;
    private VanishAbility vanishAbility;

    private KeyCode keyCode;
    private Abilities abilities;
    private SpellBase spell;

    private bool isKeyPressed = false;

    private KeyCode currentKeyPressed = KeyCode.None;
    private float attackRate = 2f;
    float nextAttackTime = 0f;

    public void Start()
    {
        abilities = GetComponent<Abilities>();
        healthmanager = GetComponent<HealthManager>();
        playerAnimator = GetComponent<AnimatorControllerOui>();
        detectDirection = GetComponent<DetectDirection>();
        playerController = GetComponent<PlayerController>();
        vanishAbility = GetComponent<VanishAbility>();
    }

    public void Update()
    {
        for (int i = 0; i < spellBarController.spellUIList.Length; i++)
        {
            GameObject spellUI = spellBarController.spellUIList[i];
            keyCode = spellUI.GetComponent<KeyBind>().keyCode;
            if (Input.GetKey(keyCode) && healthmanager.IsPlayerDead() == false)
            {
                TimerController.instance.BeginTimer();
                // Debug.Log("Key Code : " + keyCode);
                // Debug.Log("Tag : " + spellUI.tag);
                spell = abilities.getMapSpells()[i];
                currentKeyPressed = keyCode;
                CastSpell(spell, keyCode);
            }
        }
        if (currentKeyPressed == KeyCode.Mouse0 &&
            Input.GetKeyUp(currentKeyPressed))
        {
            playerAnimator.SetAutoAttack(false, false, false);
            isKeyPressed = false;
            currentKeyPressed = KeyCode.None;
        }
        SetPlayerState();
    }

    void SetPlayerState()
    {
        if (isKeyPressed == true && base.photonView.IsMine) {
            playerController.SetCurrentDirection(GetMousePosition());
            playerController.SetIsAttacking(true);
            playerController.SetPlayerSpeed(4);
        }
        else
        {
            playerController.SetIsAttacking(false);
            playerController.SetPlayerSpeed(10);
        }
    }

    Direction GetMousePosition()
    {
        string direction = detectDirection.GetMouseDirection();
        Direction currentDirection = Direction.LEFT;

        if (direction == "up")
            currentDirection = Direction.UP;
        else if (direction == "down")
            currentDirection = Direction.DOWN;
        else if (direction == "left")
            currentDirection = Direction.LEFT;
        else if (direction == "right")
            currentDirection = Direction.RIGHT;
        // Debug.Log("Current Direction: " + currentDirection);
        return currentDirection;
    }

    public void yo()
    {
        isKeyPressed = false;
        Debug.Log("yo");
    }

    void CastSpell(SpellBase spell, KeyCode keyCode)
    {
        if (spell.isCooldown == false && base.photonView.IsMine) {
            SpellBase script = spell.GetComponent<SpellBase>();
            if (vanishAbility.isVanished == true)
            {
                vanishAbility.CancelVanish();
            }
            if (isKeyPressed == true && script.isAffectPlayer == false)
            {
                playerAnimator.SetAutoAttack(false, false, false);
                isKeyPressed = false;
            }
            // Debug.Log("Affecting player ?: " + script.isAffectPlayer);
            if (script.isAffectPlayer == true)
                isKeyPressed = true;
            if (currentKeyPressed == KeyCode.Space)
            {
                script.SetPlayerAnimation(playerAnimator, playerController.currentDirection);
            }
            else
                script.SetPlayerAnimation(playerAnimator, GetMousePosition());
            script.ExecuteSpell(spell, target, gameObject);
            if ((!script.CompareTag("AutoAttack")) && script.isAffectPlayer == true)
            {
                Invoke("yo", 0.4f);
            }
            spell.isCooldown = true;
        }
    }
}
