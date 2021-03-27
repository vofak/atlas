using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Spawner for magical projectile.
	*/
    public class ProjectileSpawn : MonoBehaviour
    {

        public int spawnCost = 10;

        public int spellIndex = 0;

        private GameObject[] spells;
        public Core.Stats playerStats;

        [Header("Projectile settings")]
        [SerializeField]
        private float projectileSpeed = 10f;
        [SerializeField] private int projectileTTL = 3;

        private void Start()
        {
            spells = new GameObject[3];

            spells[0] = (GameObject)Resources.Load("Weapons/Projectiles/power_01");
            spells[1] = (GameObject)Resources.Load("Weapons/Projectiles/power_02");
            spells[2] = (GameObject)Resources.Load("Weapons/Projectiles/power_03");
        }

        public void Init(Core.PlayerController controller)
        {
            playerStats = controller.thisPlayer.gameObject.GetComponent<Core.Stats>();
        }

		/**
			Cast new projectile in given direction.
		*/
        public void CastProjectile(Vector3 direction, int dmg)
        {
            if (playerStats.mana.Decrease(spawnCost))
            {
                GameObject instance = GameObject.Instantiate(spells[spellIndex], transform.position, Quaternion.identity);
                instance.transform.rotation = Quaternion.LookRotation(direction.normalized);
                instance.transform.Rotate(-90, -90, 0);

                Projectile newProjectile = Projectile.NewInstance(projectileSpeed, projectileTTL, direction, dmg, instance);
                GameObject.Instantiate(newProjectile, transform.position, Quaternion.identity);
            }
            
        }
    }
}