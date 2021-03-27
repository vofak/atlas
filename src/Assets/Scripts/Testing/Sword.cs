using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		For testing purposes.
		Initial (obsolete) representation of player's weapon.
	*/
    public class Sword : MonoBehaviour, IWeapon
    {
        public Collider[] DamageColliders { get; private set; }

        public int BaseWeaponDamage { get; private set; }
        public int TotalWeaponDamage { get; private set; }

        void Start()
        {
            DamageColliders = GetComponents<Collider>();
            foreach (Collider col in DamageColliders)
            {
                col.isTrigger = true;
                col.enabled = false;
            }

            BaseWeaponDamage = 20;
        }

        public void OpenDamageColliders()
        {
            foreach (Collider col in DamageColliders)
            {
                col.enabled = true;
            }
        }

        public void CloseDamageColliders()
        {
            foreach (Collider col in DamageColliders)
            {
                col.enabled = false;
            }
        }


        public void PerformAttack(Vector3 direction, int characterDamage)
        {
            //int attack = Random.Range(0, AttacksAnimsHashes.Length);
            TotalWeaponDamage = BaseWeaponDamage + characterDamage;
        }

        void OnTriggerEnter(Collider other)
        {
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            if (enemy == null)
            {
                return;
            }

            enemy.TakeDamage(TotalWeaponDamage);
        }

    }
}