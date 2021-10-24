using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : SpellBase
{
    // public float attackRange;
    // public LayerMask ennemiesLayer;
    public int spellDamage;
    private bool hasCollided = false;

    public void Start()
    {
        Debug.Log("DEBUG :::: Auto pos :" + transform.position);
        _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
    }

    public void Update() {
    }

    void DestroyObject() {
        Destroy(gameObject);
    }

    public override void SetPlayerAnimation(AnimatorControllerOui playerAnimator, Direction currentDirection)
    {
        Debug.Log("current direction :" + currentDirection);
        if (currentDirection == Direction.UP)
            playerAnimator.SetAutoAttack(false, false, true);
        else if (currentDirection == Direction.DOWN)
            playerAnimator.SetAutoAttack(false, true, false);
        else
            playerAnimator.SetAutoAttack(true, false, false);
    }

    public override void ExecuteSpell(SpellBase skill, Target target, GameObject player)
    {
        //GameObject spell = InstantiateSkill(skill, target.shotPoint.position, target.transform.rotation);
        _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
        string playerId = player.GetComponentInParent<PlayerIdentity>().ID;
        _rm.InstantiateSpellForClients(playerId, skill.gameObject, target.shotPoint.position, target.shotPoint.rotation);
        Debug.Log("DEBUG :::: ID Player " + player.GetComponentInParent<PlayerIdentity>().ID);
        //spell.transform.parent = player.transform;
        //Debug.Log("DEBUG :::: pos spell after " + spell.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (hasCollided == false) {
                Debug.Log("DEBUG :::: Collision");
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
