using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing at what level the player has the skill
 */
public class AcquiredSkill {

	public Skill skill;
    public int currLevel;

    public AcquiredSkill(Skill skill, int currLevel)
    {
        this.skill = skill;
        this.currLevel = currLevel;
    }
}
