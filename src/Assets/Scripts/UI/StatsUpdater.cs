using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Atlas.UI { 

	/**
		This class updates player's HUD.
		This class updates the HUD whenever it receives an event about the change
		in player stats.
	*/
    public class StatsUpdater : MonoBehaviour {

        public static StatsUpdater instance;

        private void Awake()
        {
            instance = this;
        }


        public Core.Stats playerStats;

        [Header("Player stats")]
        public Image healthFill;
        public Text healthValues;
        public Image manaFill;
        public Text manaValues;

        [Header("Enemy stats")]
        public GameObject enemyHealthUI;
        public Image enemyHealthFill;
        public Text enemyHealthValues;

        // Use this for initialization
        void Start () {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Core.Stats>();

            playerStats.health.OnChangedCallback += UpdateHealth;
            playerStats.mana.OnChangedCallback += UpdateMana;

            UpdateHealth(playerStats.health.GetCurrentValue(), playerStats.health.GetMaxValue());
            UpdateMana(playerStats.mana.GetCurrentValue(), playerStats.mana.GetMaxValue());
        }

        void UpdateHealth(int curr, int max)
        {
            float ratio = (float) curr / max;
            healthFill.fillAmount = ratio;

            healthValues.text = curr.ToString() + '/' + max.ToString();
        }

        void UpdateMana(int curr, int max)
        {
            float ratio = (float) curr / max;
            manaFill.fillAmount = ratio;

            manaValues.text = curr.ToString() + '/' + max.ToString();
        }

		/**
			Displays enemy health during focus mode in ocmbat.
		*/
        public void DisplayEnemyHealth(int curr, int max)
        {
            if (!enemyHealthUI.activeSelf)
            {
                enemyHealthUI.SetActive(true);
            }

            float ratio = (float)curr / max;
            enemyHealthFill.fillAmount = ratio;

            enemyHealthValues.text = curr.ToString() + '/' + max.ToString();
        }
    }
}