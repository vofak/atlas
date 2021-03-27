using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		For testing purposes.
		For testing of the Skeleton animator.
	*/
    public class SkeletonTest : MonoBehaviour, IEnemy
    {

        public int health = 100;
        public Animator anim;
        public FocusTarget focusTarget;
        public Rigidbody rigid;
        public Collider coll;
        bool isUntouchable;
        bool isDead;

        bool canMove = true;

        List<Rigidbody> ragdollRigids;
        List<Collider> ragdollColliders;


        public void Die()
        {
            isDead = true;
            // enable interactable

            EnableRagdoll();
            StartCoroutine("DestroyAfterDeath");

        }

        public IEnumerator DestroyAfterDeath()
        {
            focusTarget.enabled = false;
            yield return new WaitForSeconds(15);
            Destroy(gameObject);
        }

        public void TakeDamage(int dmg)
        {

            if (isUntouchable)
            {
                return;
            }

            health -= dmg;
            isUntouchable = true;

            anim.CrossFade(Core.AnimatorHashes.Human.Animation.damaged, 0.4f);

        }

        // Use this for initialization
        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            focusTarget = GetComponent<FocusTarget>();
            rigid = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();

            rigid.constraints = RigidbodyConstraints.FreezeRotation;
            rigid.mass = 20;
            InitRagdoll();

            isDead = false;

        }

        private void InitRagdoll()
        {
            ragdollRigids = new List<Rigidbody>();
            ragdollColliders = new List<Collider>();

            Rigidbody[] rigs = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rig in rigs)
            {
                if (rig == rigid)
                {
                    continue;
                }

                rig.isKinematic = true;
                ragdollRigids.Add(rig);

                Collider rigColl = rig.gameObject.GetComponent<Collider>();
                rigColl.isTrigger = true;
                ragdollColliders.Add(rigColl);
            }
            
        }

        private void EnableRagdoll()
        {
            for (int i = 0; i < ragdollRigids.Count; ++i)
            {
                ragdollRigids[i].isKinematic = false;
                ragdollColliders[i].isTrigger = false;
            }

            rigid.isKinematic = true;
            StartCoroutine("DisableAnimator");
        }

        private IEnumerator DisableAnimator()
        {
            yield return new WaitForEndOfFrame();

            anim.enabled = false;
            coll.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (isUntouchable)
            {
                isUntouchable = !anim.GetBool(Core.AnimatorHashes.Human.Param.canMove);
            }
            else
            {
                anim.applyRootMotion = false;
            }


            if (!isDead && health <= 0)
            {
                Die();
            }
        }

        public void LooseBalance()
        {
            if (isUntouchable)
            {
                return;
            }

            isUntouchable = true;
            anim.CrossFade(Core.AnimatorHashes.Human.Animation.parried, 0.4f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ParryColliders"))
            {
                LooseBalance();
            }
        }
    }
}