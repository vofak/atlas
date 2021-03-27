using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Atlas.Core
{
	/**
		Controller for generic enemy's actions.
		This class handles basic AI actions. AI FSM is defined here.
	*/
    public abstract class EnemyController : MonoBehaviour
    {
        public Enemy thisEnemy;
        public NavMeshAgent navAgent;
        public Collider coll;

        public Combat.EnemyWeapon weapon;

        public Patrol patrol;
        public float waitingTime;

        public bool registeredForCombat = false;

        public float attackCooldown;
        public float currentCooldown = 0f;

		/**
			AI finite state machine.
		*/
        public enum EnemyState
        {
            IDLE,
            PATROLLING,
            SPOTTED_PLAYER,
            CHASING,
            ATTACKING,

            OCCUPIED
        }

        public EnemyState currentState;
        public GameObject player;
        public Player playerEntity;

        void Start()
        {
            Initialize();
            UserDefStart();
        }

		/**
			This method allows derived classes to easily add functionality
			to the end of @Start method.
		*/
        protected virtual void UserDefStart() { }

        public virtual void FixedUpdate()
        {
            if (thisEnemy.isDead)
            {
                Die();
                return;
            }

            if (playerEntity.isDead)
            {
                currentState = EnemyState.IDLE;
                Idle();
                return;
            }

            UserDefHandleStates();

            switch (currentState)
            {
                case EnemyState.IDLE:
                    if (thisEnemy.wasAttacked)
                        currentState = EnemyState.SPOTTED_PLAYER;
                    else
                        Idle();
                    
                    break;
                case EnemyState.PATROLLING:
                    if (thisEnemy.wasAttacked)
                        currentState = EnemyState.SPOTTED_PLAYER;
                    else
                        Patrol();
                    break;
                case EnemyState.SPOTTED_PLAYER:
                    SpottedPlayer();
                    break;
                case EnemyState.CHASING:
                    Chase();
                    break;
                case EnemyState.ATTACKING:
                    Attack();
                    break;
                default:
                    break;
            }
        }

		/**
			This method allows derived classes to easily add enemy-specific AI.
		*/
        protected virtual void UserDefHandleStates() { }

        public void Initialize()
        {
            thisEnemy = GetComponent<Enemy>();
            coll = GetComponent<Collider>();
            weapon = GetComponentInChildren<Combat.EnemyWeapon>();

            navAgent = GetComponent<NavMeshAgent>();
            SetUpNavMeshAgent();

            player = GameObject.FindGameObjectWithTag("Player");
            playerEntity = player.GetComponent<Player>();

            patrol.Init();
            currentState = EnemyState.IDLE;
        }

        protected virtual void SetUpNavMeshAgent() { }

		/**
			Enemy behavior in idle state.
		*/
        public abstract void Idle();
        
		/**
			Enemy behavior in patrol state.
		*/
		public abstract void Patrol();
		
		/**
			Enemy behavior in SpottedPlayer state.
		*/
        public abstract void SpottedPlayer();
		
		/**
			Enemy behavior in chase state.
		*/
        public abstract void Chase();
		
		/**
			Enemy behavior in attack state.
		*/
        public abstract void Attack();
		
		/**
			Enemy behavior in die state.
		*/
        public abstract void Die();

		/**
			This method checks if player is in range of given value.
		*/
        protected virtual bool IsPlayerInRange(float range)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= range;
        }

		/**
			This overload checks if player is in range of the enemy.
		*/
        protected virtual bool IsPlayerInRange()
        {
            return IsPlayerInRange(thisEnemy.lookRadius);
        }

		/**
			This method checks, if enemy with this controller has the player in
			specified view of sight.
		*/
        protected virtual bool IsPlayerVisible(float angle)
        {
            Vector3 dirToPlayer = player.transform.position - transform.position;
            dirToPlayer.Normalize();

            return Mathf.Abs(Vector3.Dot(transform.forward.normalized, dirToPlayer)) <= angle;
        }

		/**
			This method checks, if enemy with this controller has the player in its
			view of sight.
		*/
        protected virtual bool IsPlayerVisible()
        {
            return IsPlayerVisible(15f);
        }

		/**
			Rotates enemy with this controller towards specified coordinates.
		*/
        protected void RotateTowards(float x, float y, float z)
        {
            RotateTowards(new Vector3(x, y, z));
        }

		/**
			Rotates enemy with this controller to specified direction.
		*/
        protected void RotateTowards(Vector3 targetDir)
        {
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
            {
                targetDir = transform.forward;
            }

            StartCoroutine("RotateGradually", Quaternion.LookRotation(targetDir));
        }

        private IEnumerator RotateGradually(Quaternion targetRotation)
        {
            while (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
                yield return new WaitForFixedUpdate();
            }
        }

        protected virtual void MoveTo(Vector3 position)
        {
            Debug.Log("Moving to given position: " + position.ToString());
        }

        protected virtual void Move(Vector3 direction)
        {
            Debug.Log("Unsupported in this class.");
        }

    }
}