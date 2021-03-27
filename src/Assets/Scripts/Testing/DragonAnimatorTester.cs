using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Atlas.Core;

namespace Atlas.Combat
{
	/**
		For testing purposes.
		For testing of the Dragon animator.
	*/
    public class DragonAnimatorTester : MonoBehaviour
    {

        [SerializeField] [Range(0, 1)] private float speed;

        [SerializeField] private bool attack1;
        [SerializeField] private bool attack2;
        [SerializeField] private bool fly;

        [SerializeField] private bool die;


        bool update = true;

        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            anim.applyRootMotion = false;
            StartCoroutine("MyUpdate");
        }

        IEnumerator MyUpdate()
        {
            while (!die)
            {
                anim.applyRootMotion = !anim.GetBool(AnimatorHashes.Dragon.Param.canMove);

                if (!anim.applyRootMotion)
                {

                    if (attack1)
                    {
                        anim.applyRootMotion = true;
                        anim.CrossFade(AnimatorHashes.Dragon.Animation.attack01, 0.2f);
                        attack1 = false;
                        continue;
                    }
                    else if (attack2)
                    {
                        anim.applyRootMotion = true;
                        anim.CrossFade(AnimatorHashes.Dragon.Animation.attack02, 0.2f);
                        attack2 = false;
                        continue;
                    }


                }

                anim.SetBool(AnimatorHashes.Dragon.Param.isFlying, fly);
                anim.SetFloat(AnimatorHashes.Dragon.Param.speed, speed);
                yield return new WaitForFixedUpdate();
            }

            anim.CrossFade(AnimatorHashes.Dragon.Animation.die, 0.2f);
        }
    }
}