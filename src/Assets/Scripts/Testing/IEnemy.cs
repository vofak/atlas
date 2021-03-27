using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Initial (obsolete) representation of an enemy.
	*/
    public interface IEnemy
    {

        void TakeDamage(int dmg);
        void LooseBalance();
        void Die();
        IEnumerator DestroyAfterDeath();
    }
}