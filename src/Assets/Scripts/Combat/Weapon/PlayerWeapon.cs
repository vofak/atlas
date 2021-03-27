using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Player's weapon that damages the enemy.
	*/
    public class PlayerWeapon : Weapon
    {
        protected override void OnTriggerEnter(Collider other)
        {
            Core.Enemy enemyCol = other.gameObject.GetComponent<Core.Enemy>();
            if (enemyCol == null)
            {
                return;
            }

            int toss = Random.Range(0, 100);
            int targetDmg = stats.physicalDmg;

            if (toss < stats.critChance)
            {
                targetDmg *= 2;
            }

            enemyCol.TakeDamage(targetDmg);
        }

    }

}

