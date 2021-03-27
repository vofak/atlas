using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Generic representation of every entity in the game that is not controlled by the player.
	*/
    public abstract class NPC : Entity
    {
        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }

		/**
			Moves this entity to given position.
			How the entity navigates to the target position
			is left to decide by the derived entities.
		*/
        protected abstract void Move(Vector3 position);
 
    }

}