using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Main encapsulation of the entire combat.
		This class keeps track of all enemies currently subscribed to combat.
		
		When new combat happens, new instance of this class is instatiated to
		keep track of all enemies in the combat.
		
		The usage is two-fold. Firstly, this class allows to easily scroll through all
		enemies in current combat and change player's focus at whim. Secondly, this class
		allows simple communication between all enemies in combat, which was supposed
		to be used in Battle Circle implementation.
	*/
    public class Combat
    {
		
        private List<Core.Enemy> enemiesInCombat;
        private Dictionary<Core.Enemy, int> enemiesIndices;
        private int privateCount;
        private int scrollIndex;

        static Core.PlayerController player;
        static Combat currentCombat;

		/**
			Registers enemy to current combat.
		*/
        public static void Register(Core.Enemy enemy)
        {
            if (currentCombat == null)
            {
                CreateNewCombat(enemy);
            }
            else
            {
                AddNewEnemy(enemy);
            }
        }

		/**
			Deregisters enemy that has died.
		*/
        public static void Deregister(Core.Enemy enemy)
        {
            int idx = RemoveEnemy(enemy);

            if (currentCombat.privateCount < 1)
            {
                player.IsFocused = false;
                currentCombat = null;
            }
            else if (idx == currentCombat.scrollIndex)
            {
                FocusTarget newFocused = getNextFocusTarget();
                if (newFocused == null)
                {
                    player.focused.HideHealth();
                }
                player.focused = newFocused;
            }
        }

		/**
			Scrolls forward through all enemies that may be focused.
		*/
        public static FocusTarget getNextFocusTarget()
        {
            if (currentCombat == null || currentCombat.privateCount < 1)
            {
                return null;
            }

            Core.Enemy ret = null;
            while (ret == null)
            {
                currentCombat.scrollIndex += 1;
                if (currentCombat.scrollIndex > currentCombat.enemiesInCombat.Count - 1)
                {
                    currentCombat.scrollIndex = 0;
                }
                
                ret = currentCombat.enemiesInCombat[currentCombat.scrollIndex];
            }

            return ret.GetComponent<FocusTarget>();
        }

		
		/**
			Scrolls backward through all enemies that may be focused.
		*/
        public static FocusTarget getPreviousFocusTarget()
        {
            if (currentCombat == null || currentCombat.privateCount < 1)
            {
                return null;
            }


            Core.Enemy ret = null;

            while (ret == null)
            {
                currentCombat.scrollIndex -= 1;
                if (currentCombat.scrollIndex < 0)
                {
                    currentCombat.scrollIndex = currentCombat.enemiesInCombat.Count - 1;
                }

                ret = currentCombat.enemiesInCombat[currentCombat.scrollIndex];
            }

            return ret.GetComponent<FocusTarget>();
        }

		/**
			Creates new instance of this class in case new combat occurs in the game.
		*/
        private static void CreateNewCombat(Core.Enemy enemy)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Core.PlayerController>();
            }

            currentCombat = new Combat
            {
                enemiesInCombat = new List<Core.Enemy>(),
                enemiesIndices = new Dictionary<Core.Enemy, int>(),
                privateCount = 0,
                scrollIndex = 0
            };

            AddNewEnemy(enemy);
        }

		/**
			Auxiliary method for @Register method.
		*/
        private static void AddNewEnemy(Core.Enemy enemy)
        {
            currentCombat.enemiesIndices.Add(enemy, currentCombat.enemiesInCombat.Count);
            currentCombat.enemiesInCombat.Add(enemy);
            currentCombat.privateCount++;
        }

		/**
			Auxiliary method for @Deregister method.
		*/
        private static int RemoveEnemy(Core.Enemy enemy)
        {
            int idx;

            if (currentCombat.enemiesIndices.TryGetValue(enemy, out idx))
            {
                currentCombat.enemiesInCombat[idx] = null;
                currentCombat.enemiesIndices.Remove(enemy);
                currentCombat.privateCount--;
            }
            else
            {
                idx = -1;
            }

            return idx;
        }
        
        
    }
}