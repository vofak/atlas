using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class that holds database of existing skills
 */ 
[CreateAssetMenu(fileName = "New SkillList", menuName = "SkillTree/Skill List")]
public class SkillsList : ScriptableObject {
    

    public List<Skill> skills;
}
