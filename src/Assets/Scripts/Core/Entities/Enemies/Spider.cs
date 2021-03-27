using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core { 
	
	/**
		Basic representation of the Spider enemy.
	*/
    public class Spider : Enemy {

        protected override void UserDefDie()
        {
            Events.Instance.EnemyDied("Spider");
        }

    }

}