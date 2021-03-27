using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{

/**
		Handler for Spider animation events.
	*/
    public class SpiderAnimEventHandler : MonoBehaviour
    {

        Core.SpiderController controller;

        public void Init(Core.SpiderController controller)
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
    }
}