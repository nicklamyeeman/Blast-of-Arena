using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public SpellBarController spellBarController;

    private SpellBase[] skills;
    private Dictionary<int, SpellBase> mapSpells;

    private ClassBase classBase;

    void Start()
    {
        classBase = GetComponent<ClassBase>();
        skills = classBase.abilities;
        mapSpells = new Dictionary<int, SpellBase>();

        // Debug.Log("skill tag: " + skills[0].tag);
        // Debug.Log("skill tag: " + skills[1].tag);
        // Debug.Log("skill tag: " + skills[2].tag);
        // Debug.Log("skill tag: " + skills[3].tag);
        // Debug.Log("skill tag: " + skills[4].tag);

        mapSpells.Add(0, skills[0]);
        mapSpells.Add(1, skills[1]);
        mapSpells.Add(2, skills[2]);
        mapSpells.Add(3, skills[3]);
        mapSpells.Add(4, skills[4]);
        LinkSpellToSpellBar();
    }

    void LinkSpellToSpellBar()
    {
        int size = mapSpells.Count;
        for (int i = 0; i < size; i++) {
            GameObject spellUI = spellBarController.spellUIList[i];
            UISkillController skillUIController = spellUI.GetComponent<UISkillController>();
            skillUIController.SetSkillCooldown(mapSpells[i]);
        }
        // foreach (GameObject spellUI in spellBarController.spellUIList) {
        //     Debug.Log("Tag: " + spellUI.tag);
        //     UISkillController skillUIController = spellUI.GetComponent<UISkillController>();
        //     skillUIController.SetSkillCooldown(mapSpells[spellUI.tag]);
        // }
    }

    public Dictionary<int, SpellBase> getMapSpells()
    {
        return mapSpells;
    }

}
