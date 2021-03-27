using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Atlas.Core
{
	/**
		Specialized class for managing stats of the player.
		This class handles of player's capabilities such as physical damage,
		magical damage and armor.
		It also manages player's level ans experience.
	*/
    [System.Serializable]
    public class PlayerStats : Stats
    {
        private SkillTree skillTree;
        private PlayerInventory inv;

        public int currLvl;
        public int currExp;
        public int nextLvlExp;


        public override void Start()
        {
            base.Start();
            skillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillTree>();
            inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            skillTree.onSkillUpdateCallback += RecomputeStats;
            inv.onInventoryChangeCallback += RecomputeStats;

            Events.Instance.onEnemyDiedCallback += IncreaseExperience;
            currLvl = 1;
            currExp = 0;
            nextLvlExp = 50;

            Events.Instance.onQuestCompletedCallback += QuestExp;
        }

		/**
			Recomputes all player's stats based on currently equipped items and
			learned skills.
		*/
        public void RecomputeStats()
        {
            ResetStats();

            foreach (Equipment eq in inv.equipment)
            {
                if (eq != null)
                {
                    AddStats(eq);
                }
                
            }

            foreach (AcquiredSkill skill in skillTree.skills)
            {
                AddStats(skill);
            }

            foreach (AcquiredSkill skill in skillTree.skills)
            {
                AddStatsPercent(skill);
            }

        }

        private void AddStats(Equipment eq)
        {
            AddStats(eq.armorStat, eq.magicDamageStat, eq.damageStat, eq.healthRegen, eq.manaRegen, eq.critChanceStat);
        }

        private void AddStats(AcquiredSkill skill)
        {
            AddStats(skill.skill.armorFlat*skill.currLevel, skill.skill.magicDmgFlat*skill.currLevel, skill.skill.physicalDmgFlat*skill.currLevel, 0, 0, skill.skill.critPercent*skill.currLevel);

        }

        private void AddStatsPercent(AcquiredSkill skill)
        {
            int newArmor = this.armor + (skill.skill.armorPercent * skill.currLevel * this.armor / 100);
            int newMagic = this.magicDmg + (skill.skill.magicDmgPercent * skill.currLevel * this.magicDmg / 100);
            int newPhysical = this.physicalDmg + (skill.skill.physicalDmgPercent * skill.currLevel * this.physicalDmg / 100);
            int newCrit = this.critChance + (skill.skill.critDmg * skill.currLevel);
            SetStats(newArmor, newMagic, newPhysical, this.healthRegen, this.manaRegen, newCrit);
        }


        private void AddStats(int armor, int magicDmg, int physicalDmg, int healthRegen, int manaRegen, int critChance)
        {
            this.armor += armor;
            this.magicDmg += magicDmg;
            this.physicalDmg += physicalDmg;
            this.healthRegen += healthRegen;
            this.manaRegen += manaRegen;
            this.critChance += critChance;
        }

        private void SetStats(int armor, int magicDmg, int physicalDmg, int healthRegen, int manaRegen, int critChance)
        {
            this.armor = armor;
            this.magicDmg = magicDmg;
            this.physicalDmg = physicalDmg;
            this.healthRegen = healthRegen;
            this.manaRegen = manaRegen;
            this.critChance = critChance;
            this.secondaryDmg = 20;
        }

        private void ResetStats()
        {
            SetStats(0, 80, 50, 0, 0, 1);
        }

        private void IncreaseExperience(string name)
        {
            switch (name)
            {
                case "Skeleton":
                    GainExperience(100);
                    break;
                case "Spider":
                    GainExperience(150);
                    break;
                case "Troll":
                    GainExperience(300);
                    break;
                case "Dragon":
                    GainExperience(1000000);
                    break;
            }
        }

        public void GainExperience(int amount)
        {
            currExp += amount;
            
            while (currExp >= nextLvlExp)
            {
                currLvl += 1;
                Events.Instance.LevelUp();

                health.ChangeMaxValue(25);
                mana.ChangeMaxValue(5);

                nextLvlExp += 500;
            }
        }


        private void QuestExp(Quest q)
        {
            GainExperience(q.expReward);
        }
    }
}

