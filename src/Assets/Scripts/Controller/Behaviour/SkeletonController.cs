using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core {

	/**
		Skeleton controller overriding methods of the base class.
	*/
    public class SkeletonController : EnemyController
    {
        public SkeletonAnimationController animController;
        public Combat.SkeletonAnimEventHandler animEventHandler;

        protected override void UserDefHandleStates() {
           /* if (thisEnemy.wasParried)
            {
                animController.isInAction = true;
                animController.anim.Play(AnimatorHashes.Skeleton.Animation.damaged);
                thisEnemy.wasParried = false;
            }*/
        }

        public override void Idle()
        {
            if (IsPlayerInRange())
            {
                currentState = EnemyState.SPOTTED_PLAYER;
                return;
            }

            animController.Idle();

            if (patrol.size > 0)
            {
                currentState = EnemyState.PATROLLING;
            }
        }

        public override void Patrol()
        {
            if (IsPlayerInRange())
            {
                currentState = EnemyState.SPOTTED_PLAYER;
                return;
            }

            animController.speed = navAgent.velocity.magnitude;
            animController.Moving();

            if (Vector3.Distance(navAgent.destination, transform.position) > navAgent.stoppingDistance)
            {
                return;
            }

            if (waitingTime > 0f)
            {
                waitingTime -= Time.fixedDeltaTime;
                return;
            }

            Transform target = patrol.getNextWaypoint();
            navAgent.SetDestination(target.position);
            waitingTime = Random.Range(2.5f, 10f);
        }

        public override void SpottedPlayer()
        {
            navAgent.isStopped = true;

            animController.speed = 0;
            animController.Moving();

            RotateTowards(player.transform.position - transform.position);

            Combat.Combat.Register(thisEnemy);
            registeredForCombat = true;

            currentState = EnemyState.CHASING;
        }

        public override void Attack()
        {
            Vector3 moveDir = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(moveDir);

            animController.PerformAttack01();

            currentCooldown = attackCooldown;
            currentState = EnemyState.CHASING;
        }

        public override void Chase()
        {
            if (animController.CheckForAction())
            {
                return;
            }

            Vector3 moveDir = player.transform.position - transform.position;
            RotateTowards(moveDir);

            if (IsPlayerInRange(navAgent.stoppingDistance) && IsPlayerVisible())
            {
                navAgent.isStopped = true;


                if (currentCooldown > 0f)
                {
                    currentCooldown -= Time.fixedDeltaTime;
                    animController.speed = 0;
                    animController.Moving();
                }
                else
                {
                    currentState = EnemyState.ATTACKING;
                }

            }
            else
            {
                navAgent.isStopped = false;
                navAgent.SetDestination(player.transform.position);

                animController.speed = navAgent.velocity.magnitude;
                animController.Moving();
            }

        }

        protected override void UserDefStart()
        {
            animController = GetComponent<SkeletonAnimationController>();
            attackCooldown = 1f;

            animEventHandler = GetComponentInChildren<Combat.SkeletonAnimEventHandler>();
            animEventHandler.Init(this);
        }

        protected override void SetUpNavMeshAgent()
        {
            navAgent.speed = 3f;
            navAgent.stoppingDistance = 1.5f;
            navAgent.angularSpeed = 200f;
        }

        public override void Die()
        {
            if (animController.anim.enabled)
            {
                if (registeredForCombat)
                {
                    Combat.Combat.Deregister(thisEnemy);
                }

                animController.anim.enabled = false;

                coll.isTrigger = true;
                navAgent.enabled = false;
            }
        }
    }
}