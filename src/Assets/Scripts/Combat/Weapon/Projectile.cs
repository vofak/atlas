using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Combat
{
	/**
		Projectile behavior.
		This class manages all from projectile gradual movement,
		through collisions with enemies and environment to projectile destruction after
		its lifetime has passed.
	*/
    public class Projectile : MonoBehaviour
    {

        [Header("Instance specifics")]
        [SerializeField]
        private float speed;
        [SerializeField] private int TTL;

        [SerializeField] private Vector3 direction;
        [SerializeField] private int damage;
        [SerializeField] private GameObject particleEffect;

        private static GameObject projectilePrefab;
        private static GameObject hitPrefab;

		/**
			Method used for instantiation of a new projectile.
			This method is used because standard instantiation of a MonoBehaviour
			does not allow for passing additional parameters to the constructor.
		*/
        public static Projectile NewInstance(float speed, int TTL, Vector3 direction, int dmg, GameObject effect)
        {
            if (projectilePrefab == null)
            {
                projectilePrefab = (GameObject)Resources.Load("Weapons/Projectiles/Projectile");
                hitPrefab = (GameObject) Resources.Load("Weapons/Projectiles/Hit");
            }

            Projectile ret = projectilePrefab.GetComponent<Projectile>();

            ret.speed = speed;
            ret.TTL = TTL;
            ret.direction = direction;
            ret.damage = dmg;
            ret.particleEffect = effect;

            return ret;
        }

        // Use this for initialization
        void Start()
        {
            StartCoroutine("ProjectileLife");
        }

		/**
			Handles the entire projectile's lifetime.
		*/
        IEnumerator ProjectileLife()
        {
            while (TTL > 0)
            {
                yield return new WaitForSeconds(1);
                TTL -= 1;
            }

            Destroy(particleEffect);
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            transform.position = transform.position + direction * speed * Time.fixedDeltaTime;
        }

		/**
			Behaviour when the projectile collides with anything important.
		*/
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                Destroy(particleEffect);
                Destroy(gameObject);
            }        

            Core.Enemy enemyCol = other.gameObject.GetComponent<Core.Enemy>();
            if (enemyCol == null)
            {
                return;
            }

            enemyCol.TakeDamage(damage);
            Destroy(particleEffect);
            StartCoroutine("DestroyAfter");
            
        }

		/**
			Handles the destruction of the object along with all particle effects.
		*/
        IEnumerator DestroyAfter() {
            GameObject currHit = GameObject.Instantiate(hitPrefab, transform.position, Quaternion.identity);

            transform.position = new Vector3(0, 10000, 0);
            yield return new WaitForSeconds(1);

            Destroy(currHit);
            Destroy(gameObject);
        }
    }
}