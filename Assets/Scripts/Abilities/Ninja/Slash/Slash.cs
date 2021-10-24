using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : SpellBase
{
    public float attackRange;
    public LayerMask ennemiesLayer;
    public int spellDamage;
    private GameObject _player;

    private bool hasCollided = false;

    public void Start()
    {
        Debug.Log("2");
    }

    public void Update() {
    }

    void DestroyObject() {
        Destroy(gameObject);
    }

    public override void SetPlayerAnimation(AnimatorControllerOui playerAnimator, Direction currentDirection)
    {
        // Debug.Log("current directyion :" + currentDirection);
        if (currentDirection == Direction.UP)
            playerAnimator.playerAnimator.SetTrigger("SlashBack");
        else if (currentDirection == Direction.DOWN)
            playerAnimator.playerAnimator.SetTrigger("SlashFront");
        else
            playerAnimator.playerAnimator.SetTrigger("SlashSide");
    }

    public override void ExecuteSpell(SpellBase skill, Target target, GameObject player)
    {
        //GameObject spell = InstantiateSkill(skill, target.shotPoint.position, target.transform.rotation);
        _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
        string playerId = player.GetComponentInParent<PlayerIdentity>().ID;
        _rm.InstantiateSpellForClients(playerId, skill.gameObject, target.shotPoint.position, target.shotPoint.rotation);
        _player = player;
        //spell.transform.parent = player.transform;
        Debug.Log("1");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (hasCollided == false) {
                _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
                if (_rm)
                {
                    string id = other.GetComponentInParent<PlayerIdentity>().ID;
                    string user = gameObject.GetComponent<SpellIdentity>().From;
                    print("DEBUG :::: oui : " + id);
                    _rm.SetDamageToClients(id, spellDamage, user);
                }
                hasCollided = true;
            }
        }
    }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.DrawWireSphere(new Vector3(0, 0, 0), attackRange);
    // }
}
