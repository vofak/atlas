using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Basic controller that handles all player attacks, movement and animations.
	*/
    public class PlayerController : MonoBehaviour, IThirdPersonController
    {
        /* 
         * Interface properties
         */
        public bool IsFocused { get; set; }
        public Transform FocusedTarget { get; set; }
        public float Vertical { get; set; }
        public float Horizontal { get; set; }
        public float DeltaTime { get; set; }

        public Combat.FocusTarget focused;

        /*
         * Additional properties
         */
        public Vector3 MoveDirection { get; set; }
        public float MoveAmount { get; set; }
        public bool IsSprinting { get; set; }
        public bool IsRolling { get; set; }


        public bool LeftMouseClicked { get; set; }
        public bool RightMouseClicked { get; set; }
        public bool magicAttack;

        public bool IsGrounded { get; private set; }
        public bool IsInAction { get; private set; }

        /*
         * Additional fields 
         */
        [HideInInspector] public Animator anim;
        [HideInInspector] public Rigidbody rigid;
        [HideInInspector] public Combat.ProjectileSpawn projectileSpawn;

        private Combat.AnimatorHook animHook;


        private float moveSpeed = 3f;
        private float sprintSpeed = 5f;
        private float rotationSpeed = 7f;

        private float distanceToGround = 0.5f;

        public Player thisPlayer;
        public Combat.PlayerWeapon currWeapon;
        public Combat.Shield currShield;
        public MeshRenderer swordMesh;
        public MeshRenderer shieldMesh;

        public bool isArmed;
        public bool isShielded;

        // Inspector assignments
        [SerializeField] private GameObject activeModel;
        public GameObject weaponObject;
        public GameObject shieldObject;
        

        public void Init()
        {
            thisPlayer = GetComponent<Core.Player>();

            SetUpAnimator();
            SetUpRigid();

            projectileSpawn = GetComponentInChildren<Combat.ProjectileSpawn>();
            projectileSpawn.Init(this);

            animHook = activeModel.AddComponent<Combat.AnimatorHook>();
            animHook.Init(this);

            currWeapon = weaponObject.GetComponent<Combat.PlayerWeapon>();
            currShield = shieldObject.GetComponent<Combat.Shield>();

            swordMesh = weaponObject.GetComponent<MeshRenderer>();
            shieldMesh = shieldObject.GetComponent<MeshRenderer>();

            swordMesh.enabled = false;
            shieldMesh.enabled = false;
        }

		/**
			User defined Update().
		*/
        public void Recompute()
        {
            if (focused != null)
            {
                focused.UpdateEnemyHealth();
            }


            if (thisPlayer.isDead)
            {
                Die();
                return;
            }

            SpawnEquipment();

            if ((IsInAction = !anim.GetBool(AnimatorHashes.Human.Param.canMove)))
            {
                anim.applyRootMotion = true;
                return;
            }

            anim.applyRootMotion = false;
            DetectAction();
            if (IsInAction)
            {
                return;
            }

            if (IsFocused)
            {
                IsSprinting = false;
            }

            HandleRolls();
            if (IsInAction)
            {
                return;
            }

            Rotate();
            Move();
            Animate();
        }

		/**
			User defined FixedUpdate().
		*/
        public void FixedRecompute()
        {
            anim.SetBool(AnimatorHashes.Human.Param.isGrounded, thisPlayer.isGrounded);
        }

		/**
			User defined LateUpdate().
		*/
        public void LateRecompute()
        {
            throw new System.NotImplementedException();
        }

        private void SetUpAnimator()
        {
            anim = GetComponentInChildren<Animator>();
            anim.applyRootMotion = false;

            anim.SetBool(AnimatorHashes.Human.Param.isArmed, isArmed);
            anim.SetBool(AnimatorHashes.Human.Param.hasShield, isShielded);
            anim.SetBool(AnimatorHashes.Human.Param.isGrounded, true);
        }

        private void SetUpRigid()
        {
            rigid = GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeRotation;
        }

		/**
			Detects whether player wants to perform a special action such as a roll
			or an attack.
		*/
        private void DetectAction()
        {
            if (magicAttack)
            {
                anim.CrossFade(AnimatorHashes.Human.Animation.spellcast, 0.2f);
                return;
            }

            if (!LeftMouseClicked && !RightMouseClicked)
            {
                anim.SetBool(AnimatorHashes.Human.Param.isParrying, false);
                IsInAction = false;
                return;
            }

            if (LeftMouseClicked && isArmed)
            {
                anim.CrossFade(AnimatorHashes.Human.Animation.attack01, 0.1f);
            }
            else if (RightMouseClicked && isShielded)
            {
                anim.CrossFade(AnimatorHashes.Human.Animation.attack02, 0.2f);
            }

            IsInAction = true;
        }

		/**
			Rotates player according to the input.
		*/
        private void Rotate()
        {
            Vector3 targetHeading = IsFocused ? focused.target.transform.position - transform.position : MoveDirection;
            targetHeading.Normalize();
            targetHeading.y = 0;

            if (targetHeading == Vector3.zero)
            {
                targetHeading = transform.forward;
            }

            Quaternion targetRot = Quaternion.LookRotation(targetHeading);
            Quaternion currentRot = Quaternion.Slerp(transform.rotation, targetRot, DeltaTime * MoveAmount * rotationSpeed);
            transform.rotation = currentRot;
        }

		/**
			Moves player according to the input.
		*/
        private void Move()
        {
            float targetSpeed = IsSprinting ? sprintSpeed : moveSpeed;

            if (/*IsGrounded*/ thisPlayer.isGrounded)
            {
                Vector3 moveDir = MoveDirection;
                moveDir.y = 0;
                rigid.velocity = (targetSpeed * MoveAmount) * moveDir;
                //transform.Translate((targetSpeed * MoveAmount) * moveDir);
            }

        }

		/**
			Animates player according to his current actions.
		*/
        private void Animate()
        {
            anim.SetBool(AnimatorHashes.Human.Param.isRunning, IsSprinting);
            anim.SetBool(AnimatorHashes.Human.Param.isFocused, IsFocused);

            if (IsFocused)
            {
                Vector3 relativDir = transform.InverseTransformDirection(MoveDirection);
                if (relativDir.y != 0)
                {
                    relativDir = new Vector3(relativDir.x, 0, relativDir.z);
                    relativDir.Normalize();
                }
                anim.SetFloat(AnimatorHashes.Human.Param.vertical, relativDir.z);
                anim.SetFloat(AnimatorHashes.Human.Param.horizontal, relativDir.x);
            }
            else
            {
                anim.SetFloat(AnimatorHashes.Human.Param.vertical, MoveAmount);
            }

        }

		/**
			Handles special action of rolling.
		*/
        private void HandleRolls()
        {
            if (!IsRolling)
            {
                return;
            }

            float v = Vertical;
            if (MoveAmount > 0.3f)
            {
                v = IsFocused ? (v > 0f ? 1 : ((Horizontal != 0) ? 1 : -1)) : 1;
            }
            else
            {
                v = 0;
            }

            if (v != 0)
            {
                Vector3 moveDir = MoveDirection;
                if (IsFocused)
                {
                    moveDir = v > 0f ? MoveDirection : -MoveDirection;
                }

                moveDir = moveDir == Vector3.zero ? transform.forward : moveDir;
                Quaternion rot = Quaternion.LookRotation(moveDir);
                transform.rotation = rot;
            }

            anim.SetFloat(AnimatorHashes.Human.Param.vertical, v);
            IsInAction = true;
            anim.CrossFade(AnimatorHashes.Human.Animation.rollsBlend, DeltaTime);
        }

		/**
			Checks if the player is on ground or midair.
		*/
        private bool IsOnGround()
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

            return ret;
        }

		/**
			Animates player at their death.
		*/
        public void Die()
        {
            if (!anim.GetBool(AnimatorHashes.Human.Param.isDead))
            {
                anim.SetBool(AnimatorHashes.Troll.Param.isDead, true);
                anim.Play(AnimatorHashes.Human.Animation.die);
            }
        }

		/**
			Spawns equipped weapon and shield.
		*/
        private void SpawnEquipment()
        {
            anim.SetBool(AnimatorHashes.Human.Param.isArmed, isArmed);
            anim.SetBool(AnimatorHashes.Human.Param.hasShield, isShielded);

            if (isArmed)
            {
                swordMesh.enabled = true;
            }
            else
            {
                swordMesh.enabled = false;
            }

            if (isShielded)
            {
                shieldMesh.enabled = true;
            }
            else
            {
                shieldMesh.enabled = false;
            }
        }

    }
}