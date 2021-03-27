using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Handler for Human animation events.
	*/
    public class AnimatorHook : MonoBehaviour
    {

        Core.PlayerController animController;

        Animator anim;
        ProjectileSpawn projectileSpawn;


        public void Init(Core.PlayerController controller)
        {
            animController = controller;
            anim = animController.anim;
            projectileSpawn = animController.projectileSpawn;
        }

		/**
			Moves animator object along with the player during attack.
		*/
        void OnAnimatorMove()
        {
            if (!animController.IsInAction)
            {
                return;
            }

            Vector3 delta = anim.deltaPosition;
            delta.y = 0;
            if(animController.DeltaTime == 0)
            {
                animController.rigid.velocity = new Vector3(0f, 0f, 0f);

            } else
            {
                animController.rigid.velocity = delta / animController.DeltaTime;

            }
        }

		/**
			Opens weapon colliders to deal damage during attacks.
		*/		
        public void OpenDamageColliders()
        {
            animController.currWeapon.OpenDamageColliders();
        }

		/**
			Closes weapon colliders not to deal damage when not attacking.
		*/
        public void CloseDamageColliders()
        {
            animController.currWeapon.CloseDamageColliders();
        }

		/**
			Opens shield colliders to deal damage during attacks.
		*/
        public void OpenParryColliders()
        {
            animController.currShield.OpenParryColliders();
        }

		/**
			Closes shield colliders not to deal damage when not attacking.
		*/
        public void CloseParryColliders()
        {
            animController.currShield.CloseParryColliders();
        }

		/**
			Cast projectile during magical attack.
		*/
        public void CastProjectile()
        {
            projectileSpawn.CastProjectile(animController.transform.forward, animController.thisPlayer.stats.magicDmg);
        }
    }
}