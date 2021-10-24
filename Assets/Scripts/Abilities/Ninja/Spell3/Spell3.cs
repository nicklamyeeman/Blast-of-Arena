using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell3 : SpellBase
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    public void Start() {
        Invoke("DestroyProjectile", lifeTime);
    }

    public void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            // if (hitInfo.collider.CompareTag("Enemy")) {
            //     // do something when enemy is touched
            // }
            // else if (hitInfo.collider.CompareTag("Environment")) {
            //     DestroyProjectile();
            // }
            DestroyProjectile();
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }

    public override void ExecuteSpell(SpellBase spell, Target target, GameObject player)
    {
        InstantiateSkill(spell, target.shotPoint.position, target.transform.rotation);
        Debug.Log("Spell 3 pressed");
    }

    public override void SetPlayerAnimation(AnimatorControllerOui playerAnimator, Direction currentDirection) {}

}
