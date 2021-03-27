using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Atlas.Core
{
	/**
		Generic representation of every enemy that appears in the game.
	*/
    public class Enemy : NPC
    {
		// Transform that is used as target for the camera during focus in combat
        public Combat.FocusTarget focusTarget;
		
		// Range of the enemy
        [Range(0.1f, 50f)] public float lookRadius = 6f;
		
        public bool wasParried;
        public NavMeshAgent navAgent;

		/**
			This method offers the possibility for derived classes to add
			some behaviour at the end of @Start method without having to explicitly
			override it and call its implementation in the base class.
		*/
        protected virtual void UserDefStartII() { }


        protected override void UserDefStart() {
            focusTarget = GetComponent<Combat.FocusTarget>();
            wasParried = false;
            navAgent = GetComponent<NavMeshAgent>();

            UserDefStartII();
        }

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

        protected override void Move(Vector3 position)
        {
            Debug.Log("Enemy is moving to " + position.ToString());
        }

		/**
			This method implements a way which this enemy follows a player
			or any other specified game object.
		*/
        protected virtual void Follow(GameObject target)
        {
            Move(target.transform.position);
        }

        protected override void Die()
        {
			if (isDead)
            {
                return;
            }
            isDead = true;
            UserDefDie();
            focusTarget.HideHealth();

            Lootable l = GetComponent<Lootable>();
            if(l != null)
            {
                l.Active = true;
            }
 
            StartCoroutine("DestroyAfterDeath");
        }
		
		/**
			This method offers the possibility for derived classes to add
			some behaviour at the end of @Die method without having to explicitly
			override it and call its implementation in the base class.
		*/
        protected virtual void UserDefDie() { }

		/**
			This method destroys dead enemy after 30 seconds from its death.
		*/
        protected IEnumerator DestroyAfterDeath()
        {
            focusTarget.enabled = false;
            yield return new WaitForSeconds(30);
            Destroy(gameObject);
        }

		/**
			Gizmos drawing callback.
		*/
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, lookRadius);
        }

		/**
			This piece of code is executed when this enemy is spawned during gameplay.
			Basically, what is called on Start needs to be called here.
		*/
        public void OnSpawn()
        {
            Start();
            GetComponent<EnemyAnimationController>().Start();
			
			// To fix occasional problems with initializing new Nav Mesh Agent.
            navAgent.enabled = false;
            navAgent.enabled = true;
        }
    }

}