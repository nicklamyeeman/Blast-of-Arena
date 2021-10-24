using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class SpellBase : MonoBehaviourPun
{
    public float skillCooldown;
    public bool isCooldown;
    // public Image icon;
    public string title;
    public bool isAffectPlayer;
    protected RoundManager _rm;

    public virtual void ExecuteSpell(SpellBase skill, Target target, GameObject player) {}
    public virtual void SetPlayerAnimation(AnimatorControllerOui playerAnimator, Direction currentDirection) {}

    // Network call
    public GameObject InstantiateSkill(SpellBase skill, Vector3 pos, Quaternion rota)
    {
        GameObject obj;
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("DEBUG :::: J'envoie a travers le network a la pos : " + pos);
            obj = MasterManager.NetworkInstantiate(skill.gameObject, pos, rota);
        }
        else
            obj = Instantiate(skill, pos, rota).gameObject;

        return obj;
    }

    public void DamageEnnemy(GameObject target, int dmg)
    {
    }
}
