using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : SpellBase
{
    public int spellDamage = 25;
    private List<Collider2D> list = new List<Collider2D>();
    private bool _canDoDmg = false;
    // Start is called before the first frame update
    void Start()
    {
        // canExplode = false;
        // testt = false;
    }

    void EnableDamages()
    {

    }

    void playSound()
    {
        Debug.Log("play sound");
        GetComponent<AudioSource>().Play();
    }


    void DestroyObject() {
        foreach (var item in list) {  
            item.GetComponent<HealthManager>().isInvicible = false; 
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("ouais ouais ouais " + canExplode);
        HealthManager hm = other.GetComponent<HealthManager>();
        if (other.CompareTag("Player") && hm.isInvicible == false) {
            list.Add(other);
            Debug.Log("-25");
            hm.isInvicible = true;
            // other.GetComponent<HealthManager>().SetInvicibility(true);
            _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
            if (_rm)
            {
                string id = other.GetComponentInParent<PlayerIdentity>().ID;
                string user = gameObject.GetComponent<SpellIdentity>().From;
                print("Try user " + user);
                print("DEBUG :::: oui : " + id);
                _rm.SetDamageToClients(id, spellDamage, user);
            }    //         GetComponent<Animator>().SetTrigger("bombExplosion");
                 //         // if (hasCollided == false) {
                 //             // _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
                 //             // if (_rm)
                 //             // {
                 //             //     string id = other.GetComponentInParent<PlayerIdentity>().ID;
                 //             //     print("DEBUG :::: oui : " + id);
                 //             //     _rm.SetDamageToClients(id, spellDamage);
                 //             // }
                 //         //     hasCollided = true;
                 //         // }
        }
    }

    void Update()
    {
    }

    public override void ExecuteSpell(SpellBase skill, Target target, GameObject player)
    {
        Debug.Log("Bomb");
        _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
        string playerId = player.GetComponentInParent<PlayerIdentity>().ID;
        Quaternion rotation = new Quaternion();
        _rm.InstantiateSpellForClients(playerId, skill.gameObject, player.transform.position, rotation);
        // Invoke("ActivateBomb", 2);
        // _rm = GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>();
        // if (_rm)
        // {
        //     string id = player.GetComponentInParent<PlayerIdentity>().ID;
        //     _rm.SetInvisibilityToClients(id);
        // }

    }

}
