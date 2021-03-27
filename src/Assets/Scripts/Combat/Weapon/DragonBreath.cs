using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Representation of the dragon breath that is a unique weapon used by the dragon.
	*/
    public class DragonBreath : MonoBehaviour, IWeapon
    {

        public Collider[] DamageColliders { get; private set; }
        public int[] AttacksAnimsHashes { get; private set; }

        public int BaseWeaponDamage { get; private set; }
        public int TotalWeaponDamage { get; private set; }

        GameObject dragonBreathSpray;
        Core.Stats stats;

        void Start()
        {
            DamageColliders = GetComponents<Collider>();
            foreach (Collider col in DamageColliders)
            {
                col.isTrigger = true;
                col.enabled = false;
            }

            AttacksAnimsHashes = new int[] { Animator.StringToHash("Dragon|Attack02") };
            BaseWeaponDamage = 20;

            dragonBreathSpray = transform.parent.GetChild(0).gameObject;
            dragonBreathSpray.SetActive(false);

            stats = GameObject.FindObjectOfType<Core.Dragon>().GetComponent<Core.Stats>();
        }

		/**
			Activates weapon's colliders so that it can deal damage.
		*/
        public void OpenDamageColliders()
        {
            foreach (Collider col in DamageColliders)
            {
                col.enabled = true;
            }

            dragonBreathSpray.SetActive(true);
        }

		/**
			Deactivates weapon's colliders so that it can't deal damage.
		*/
        public void CloseDamageColliders()
        {
            foreach (Collider col in DamageColliders)
            {
                col.enabled = false;
            }

            dragonBreathSpray.SetActive(false);
        }

        public void PerformAttack(Vector3 direction, int characterDamage)
        {
            //int attack = Random.Range(0, AttacksAnimsHashes.Length);
            TotalWeaponDamage = BaseWeaponDamage + characterDamage;
        }

		/**
			Deals damage to the player.
		*/
        void OnTriggerEnter(Collider other)
        {
            Core.Player playerCol = other.gameObject.GetComponent<Core.Player>();
            if (playerCol == null)
            {
                return;
            }

            playerCol.TakeDamage(stats.magicDmg);
        }
    }

   /* public class DragonBreath : Weapon
    {
        protected override void OnTriggerEnter(Collider other)
        {
            Core.Player playerCol = other.gameObject.GetComponent<Core.Player>();
            if (playerCol == null)
            {
                return;
            }

            playerCol.TakeDamage(stats.magicDmg);
        }

    }*/
}