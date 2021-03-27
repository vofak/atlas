using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atlas.Core
{
	/**
		Basic class for handling all necessary information about player's current state.
	*/
    public class Player : Entity
    {
        public Rigidbody rigid;
        public Collider coll;
        public PlayerInventory inv;
        public AudioSource damagedAudio;

        List<Rigidbody> ragdollRigids;
        List<Collider> ragdollColliders;

        private int repeats;
        private float deltaHeight;

        protected override void UserDefStart()
        {
            isGrounded = true;

            rigid = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();

            rigid.constraints = RigidbodyConstraints.FreezeRotation;
            rigid.mass = 20;

            repeats = (int)Mathf.Ceil(2f / Time.fixedDeltaTime);
            deltaHeight = 0.8f / repeats;


            inv = GetComponent<PlayerInventory>();
            damagedAudio = GetComponent<AudioSource>();
        }

        public override void TakeDamage(int value)
        {
            damagedAudio.enabled = true;
            base.TakeDamage(value);
            StartCoroutine("StopAudio");
        }

        protected override void Die()
        {
            isDead = true;

            coll.enabled = false;
            rigid.isKinematic = true;

            StartCoroutine("EndGame");
        }

		/**
			Game behavior after player's death.
		*/
        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene(0);
        }

		/**
			Handles turning off the audio of player being damaged in combat.
		*/
        IEnumerator StopAudio()
        {
            yield return new WaitForSeconds(1);
            damagedAudio.enabled = false;
        }


    }

}