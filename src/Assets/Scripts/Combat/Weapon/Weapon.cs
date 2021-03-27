using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Generic weapon representation.
	*/
    public abstract class Weapon : MonoBehaviour {

        public Collider[] damageColliders;
        public Core.Stats stats;

        void Start () {
		    damageColliders = GetComponents<Collider>();

            foreach (Collider col in damageColliders)
            {
                col.isTrigger = true;
                col.enabled = false;
            }

            stats = GetComponentInParent<Core.Stats>();

            UserDefInit();
        }

		/**
			This method allows for the derived classes to easily add functionality to
			@Start method withput having to override the method and
			call the parent implemetation again.
		*/
        protected virtual void UserDefInit() { }

		/**
			Activates weapon's colliders so that it can deal damage.
		*/
        public void OpenDamageColliders()
        {
            foreach (Collider col in damageColliders)
            {
                col.enabled = true;
            }
        }

		/**
			Deactivates weapon's colliders so that it can't deal damage.
		*/
        public void CloseDamageColliders()
        {
            foreach (Collider col in damageColliders)
            {
                col.enabled = false;
            }
        }

		/**
			Basic behavior of the given weapon on collision.
		*/
        protected abstract void OnTriggerEnter(Collider other);
    }
}