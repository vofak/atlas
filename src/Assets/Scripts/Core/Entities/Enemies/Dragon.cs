using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atlas.Core { 
	
	/**
		Basic representation of the Dragon enemy.
	*/
    public class Dragon : Enemy {

        protected override void UserDefDie()
        {
            Events.Instance.EnemyDied("Dragon");

            StartCoroutine("EndGame");
        }

		/**
			Ends the game after successful killing of the main boss. 
		*/
        IEnumerator EndGame()
        {
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene(2);
        }

    }
}