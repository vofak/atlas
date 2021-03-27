using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core { 

	/**
		Basic representation of the Skeleton enemy.
	*/
    public class Skeleton : Enemy {

        public Rigidbody rigid;
        public Collider coll;

        List<Rigidbody> ragdollRigids;
        List<Collider> ragdollColliders;

        protected override void UserDefStartII()
        {
            
            rigid = GetComponent<Rigidbody>();
            coll = GetComponent<Collider>();

            rigid.constraints = RigidbodyConstraints.FreezeRotation;
            rigid.mass = 20;
            InitRagdoll();
        }

        protected override void UserDefDie()
        {
            EnableRagdoll();
            rigid.isKinematic = true;

            Events.Instance.EnemyDied("Skeleton");
        }

		/**
			Initializes all colliders and rigidbodies that
			skeleton's ragdoll comprises of.
			All these components are deactivated at the skeleton's birth.
		*/
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

                rig.mass = 20;
                rig.isKinematic = true;
                ragdollRigids.Add(rig);

                Collider rigColl = rig.gameObject.GetComponent<Collider>();
                rigColl.isTrigger = true;
                ragdollColliders.Add(rigColl);
            }

        }

		/**
			Enables all components that make up the ragdoll after at skeleton's death.
		*/
        private void EnableRagdoll()
        {
            
            for (int i = 0; i < ragdollRigids.Count; ++i)
            {
                ragdollRigids[i].isKinematic = false;
                ragdollColliders[i].isTrigger = false;

                ragdollRigids[i].velocity = Vector3.zero;
            }

        }

        protected override void Update()
        {
            base.Update();
            rigid.velocity = Vector3.zero;
            
        }

        public void GetParried()
        {
            wasParried = true;
        }

    }
}