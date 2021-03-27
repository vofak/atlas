using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Controller for enemy animations. This class encapsulates communication with
		enemy's animator and the FSM that it is represented by.
	*/
    public abstract class EnemyAnimationController : MonoBehaviour {

        public bool isInAction;
        public float speed;
        public Vector3 moveDirection;

        public Entity thisEntity;
        public Animator anim;

        public virtual void Start()
        {
            Init();
        }

       /* protected virtual void Update()
        {
            if ((isInAction = CheckForAction()))
            {
                anim.applyRootMotion = true;
            }
            else
            {
                anim.applyRootMotion = false;
            }
        }*/

		/**
			Updates parameters of the animator.
		*/
        protected abstract void UpdateParamaters();

		/**
			Plays attack01 animation of the animator and performs any additional
			operations that are necessary.
		*/
        public abstract void PerformAttack01();
        /*{
            Debug.Log("Unsupported in this class.");
        }*/

		/**
			Plays attack02 animation of the animator and performs any additional
			operations that are necessary.
		*/
        public abstract void PerformAttack02();
        /*{
            Debug.Log("Unsupported in this class.");
        }*/

		/**
			Checks if animator is currently playing any action animation.
		*/
        public abstract bool CheckForAction();
        /*{
            Debug.Log("Unsupported in this class.");
            return false;
        }*/

        protected virtual void Init()
        {
            thisEntity = GetComponent<Entity>();
            InitAnimator();
            UserDefInit();
        }

        protected void InitAnimator()
        {
            anim = GetComponentInChildren<Animator>();
            anim.applyRootMotion = false;
            SetUpAnimator();
        }

        protected abstract void SetUpAnimator();
        /*{
            // for player
            /*
            anim.SetBool(AnimatorHashes.Humanoid.Param.isArmed, true);
            anim.SetBool(AnimatorHashes.Humanoid.Param.isGrounded, true);
            anim.SetBool(AnimatorHashes.Humanoid.Param.hasShield, true);
            
        }*/

		/**
			This method allows derived classes to easily
			add functionality to @Init method.
		*/
        protected abstract void UserDefInit();
        /*{
            Debug.Log("Unsupported in this class.");
        }*/

        
    }
}