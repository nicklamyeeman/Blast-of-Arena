using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : SpellBase
{
    private void Start()
    {
    }

    public override void SetPlayerAnimation(AnimatorControllerOui playerAnimator, Direction currentDirection)
    {
        playerAnimator.SetDashAnimation(currentDirection);
    }

    public override void ExecuteSpell(SpellBase skill, Target target, GameObject player)
    {
        player.GetComponent<DashMove>().Dash();
    }

}
