using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : SpellBase
{
    private RoundManager _rm;

    private void Start()
    {
    }

    void DestroyObject() {
        Destroy(gameObject);
    }

    public override void ExecuteSpell(SpellBase skill, Target target, GameObject player)
    {
        Debug.Log("Vanished");
        SpellBase spell = Instantiate(skill, player.transform.position, player.transform.rotation);
        player.GetComponent<VanishAbility>().SetVanish(0.4f);
        _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
        if (_rm)
        {
            string id = player.GetComponentInParent<PlayerIdentity>().ID;
            _rm.SetInvisibilityToClients(id);
        }

    }

}
