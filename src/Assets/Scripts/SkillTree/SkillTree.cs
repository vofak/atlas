using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * script enabling player to acquire skills
 */ 
public class SkillTree : MonoBehaviour {


    public SkillsList skillList;

    public List<AcquiredSkill> skills = new List<AcquiredSkill>();

    public int skillPoints = 1;

    public delegate void OnSkillUpdate();
    public OnSkillUpdate onSkillUpdateCallback;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < skillList.skills.Count; i++)
        {
            skills.Add(new AcquiredSkill(skillList.skills[i], 0));
        }
        Events.Instance.onLevelUpCallback += AddSkillPoint;
	}

    public void AddSkillPoint()
    {
        skillPoints++;
    }
	
	public void UpgradeSkill(int skill)
    {
        if(skills[skill].currLevel < skills[skill].skill.maxLevel && skillPoints > 0 )
        {
            if(skills[skill].skill.requiredSkill < 0 || skills[skills[skill].skill.requiredSkill].currLevel == skills[skills[skill].skill.requiredSkill].skill.maxLevel)
            {
                skillPoints--;
                skills[skill].currLevel++;
                onSkillUpdateCallback.Invoke();

            }
        }
    }
}
