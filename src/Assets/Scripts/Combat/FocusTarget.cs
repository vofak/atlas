using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Atlas.Combat
{
	/**
		This class encapsulates access to the transform that camera track during
		focus mode in combat.
		It also, for its suitable access, displays focused enemy's health.
	*/
    public class FocusTarget : MonoBehaviour
    {

        /*
         * Should be assigned in the inspector.
         * In case it is not, it is defaulted to object's own transform.
         */
        public Transform target;
        public Atlas.Core.Stats stats;

        private void Start()
        {
            {
                if (target == null)
                {
                    target = transform;
                }
            }

            stats = GetComponent<Atlas.Core.Stats>();
        }

		/**
			Updates displayed enemy's health.
		*/
        public void UpdateEnemyHealth()
        {
            int curr = stats.health.GetCurrentValue();
            int max = stats.health.GetMaxValue();
            Atlas.UI.StatsUpdater.instance.DisplayEnemyHealth(curr, max);
        }

		/**
			Hides enemy's health on deactivation of the focus.
		*/
        public void HideHealth()
        {
            Atlas.UI.StatsUpdater.instance.enemyHealthUI.SetActive(false);
        }

    }

}

