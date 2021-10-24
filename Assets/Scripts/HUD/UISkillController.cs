using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    public Image imageCooldown;
    SpellBase skill;
    private bool toSet = false;

    void Start()
    {
        // spell = skill.GetComponent<SkillCooldown>();
        // spell.isCooldown = false;
    }

    void Update()
    {
        if (skill.isCooldown) {
            if (toSet == false) {
                imageCooldown.fillAmount = 1;
                toSet = true;
            }
            imageCooldown.fillAmount -= 1 / skill.skillCooldown * Time.deltaTime;

            if (imageCooldown.fillAmount <= 0) {
                imageCooldown.fillAmount = 0;
                skill.isCooldown = false;
                toSet = false;
            }
        }
    }

    public void SetSkillCooldown(SpellBase _skill)
    {
        skill = _skill;
        skill.isCooldown = false;
    }

}
