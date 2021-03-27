using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Enemy's weapon that damages the player.
	*/
    public class EnemyWeapon : Weapon
    {
        protected override void OnTriggerEnter(Collider other)
        {
            Core.Player playerCol = other.gameObject.GetComponent<Core.Player>();
            if (playerCol == null)
            {
                return;
            }

            playerCol.TakeDamage(stats.physicalDmg);
        }

    }

}
