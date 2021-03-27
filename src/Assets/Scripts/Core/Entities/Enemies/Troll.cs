using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Basic representation of the Troll enemy.
	*/
    public class Troll : Enemy {

        protected override void UserDefDie()
        {
            Events.Instance.EnemyDied("Troll");
        }

    }
}