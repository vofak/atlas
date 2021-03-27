using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Handler for Dragon animation events.
	*/
    public class DragonAnimEventHandler : MonoBehaviour
    {

        DragonBreath dragonBreath;
        Core.DragonController controller;

        // Use this for initialization
        void Start()
        {
            dragonBreath = GetComponentInChildren<DragonBreath>();
        }

        public void Init(Core.DragonController controller)
        {
            this.controller = controller;
        }

		/**
			Opens weapon colliders to deal damage during attacks.
		*/	
        public void OpenDamageColliders()
        {
            controller.weapon.OpenDamageColliders();
        }

		/**
			Closes weapon colliders not to deal damage when not attacking.
		*/
        public void CloseDamageColliders()
        {
            controller.weapon.CloseDamageColliders();
        }

		/**
			Opens breath colliders to deal damage when attacking with breath.
		*/
        public void OpenBreathDamageColliders()
        {
            dragonBreath.OpenDamageColliders();
        }

		/**
			Closes breath colliders not to deal damage when not attacking with breath.
		*/
        public void CloseBreathDamageColliders()
        {
            dragonBreath.CloseDamageColliders();
        }
    }
}