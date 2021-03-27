using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Shield behavior encapsulation.
		Shield can be used to smash enemies and cause secondary damage.
	*/
    public class Shield : MonoBehaviour
    {

        public Collider[] ParryColliders { get; private set; }
        public Core.Stats stats;

        // Use this for initialization
        void Start()
        {
            GameObject parryCollider = GameObject.FindGameObjectWithTag("ParryColliders");
            ParryColliders = parryCollider.GetComponents<Collider>();
            foreach (Collider col in ParryColliders)
            {
                col.isTrigger = true;
                col.enabled = false;
            }

        }

        public void Init(Core.PlayerController controller)
        {
            stats = controller.thisPlayer.gameObject.GetComponent<Core.Stats>();
        }

        public void OpenParryColliders()
        {
            foreach (Collider col in ParryColliders)
            {
                col.enabled = true;
            }

        }

        public void CloseParryColliders()
        {
            foreach (Collider col in ParryColliders)
            {
                col.enabled = false;
            }

        }

        void OnTriggerEnter(Collider other)
        {
            Core.Enemy monster = other.gameObject.GetComponent<Core.Enemy>();
            if (monster == null)
            {
                return;
            }

            monster.TakeDamage(stats.secondaryDmg);
        }
    }
}