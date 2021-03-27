using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Basic stats of every entity in the game.
	*/
    public class Stats : MonoBehaviour
    {

        [Header("Basics")]
        public Health health;
        public NotHealth mana;

        [Header("Regen per 5 seconds")]
        public int healthRegen;
        public int manaRegen;

        [Header("Combat related")]
        public int physicalDmg;
        public int secondaryDmg;
        public int magicDmg;
        public int armor;
        public int critChance;

        public virtual void Start()
        {
            if (healthRegen > 0)
            {
                StartCoroutine("RegenerateStat", new Param(health, healthRegen));
            }

            if (manaRegen > 0)
            {
                StartCoroutine("RegenerateStat", new Param(mana, manaRegen));
            }
        }

        IEnumerator RegenerateStat(Param param)
        {
            for (;;)
            {
                yield return new WaitForSeconds(5);
                param.stat.Increase(param.regen);
            }
        }

		/**
			Auxiliary structure for easier passing of parameters
			to @RegenerateStat enumerator.
		*/
        private struct Param
        {
            public Stat stat;
            public int regen;

            public Param(Stat stat, int regen)
            {
                this.stat = stat;
                this.regen = regen;
            }
        }
    }
}