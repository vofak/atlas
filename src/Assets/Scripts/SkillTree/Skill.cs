using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing a skill that adds some stats to the player
 * 
 */ 
[CreateAssetMenu(fileName = "New Skill", menuName = "SkillTree/Skill")]
public class Skill : ScriptableObject
{
    public int maxLevel;
    public int requiredSkill;

    public int magicDmgFlat;
    public int magicDmgPercent;

    public int physicalDmgFlat;
    public int physicalDmgPercent;

    public int critDmg;
    public int critPercent;

    public int armorFlat;
    public int armorPercent;
}
