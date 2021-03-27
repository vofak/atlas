using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Basic class representing generic entity such as player or NPC.
		This class is to be derived from by any entity
		that makes an appearance in the game.
	*/
    public class Entity : MonoBehaviour
    {

        public bool wasAttacked;
        public bool isGrounded;
        public bool isDead;

		// Current properties of this entity.
        public Stats stats;

        [Header("Settings")]

        [SerializeField]
        [Range(0f, 2f)]
        private float distanceToGround = 0.5f;


        protected void Start()
        {
            stats = GetComponent<Stats>();
            stats.health.OnDepletionCallback += Die;

            isDead = false;
            UserDefStart();
        }

		/**
			This method offers the possibility for derived classes to add
			some behaviour at the end of @Start method without having to explicitly
			override it and call its implementation in the base class.
		*/
        protected virtual void UserDefStart() { }


        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            CheckIsGrounded();
        }

        protected virtual void LateUpdate()
        {
            
        }
		
		/**
			Basic behavior of the entity at the time of its death.
			It is to be overridden by the derived class.
		*/
        protected virtual void Die()
        {
            Debug.Log("Died");
        }
		
		/**
			Basic behavior of all entities when they are damaged by an opponent.
			
			Value of entity's armor is subtracted from value of dealt damage.
			However, every attack deals at least 1 damage.
		*/
        public virtual void TakeDamage(int value)
        {
            wasAttacked = true;

            int targetDamage = value - stats.armor;
            if (targetDamage <= 0)
            {
                targetDamage = 1;
            }

            stats.health.Decrease(targetDamage);
        }

		/**
			Checks if this entity is on ground or whether it is midair.
		*/
        protected void CheckIsGrounded()
        {
            bool ret = false;

            Vector3 rayOrigin = transform.position + (distanceToGround * Vector3.up);
            Vector3 rayDirection = -Vector3.up;
            float distance = distanceToGround + 0.3f;
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, distance))
            {
                ret = true;
                transform.position = hit.point;
            }

            isGrounded = ret;
        }
    }
}