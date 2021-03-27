using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Atlas.Core;

namespace Atlas.Combat
{
	/**
		For testing purposes.
		For testing of the Spider animator.
	*/
    [RequireComponent(typeof(Animator))]
    public class SpiderAnimatorTester : MonoBehaviour
    {

        [SerializeField] [Range(0, 1)] private float speed;

        [SerializeField] private bool attack1;
        [SerializeField] private bool attack2;
        [SerializeField] private bool damaged;

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
                anim.applyRootMotion = !anim.GetBool(AnimatorHashes.Spider.Param.canMove);

                if (!anim.applyRootMotion)
                {
                    if (damaged)
                    {
                        anim.applyRootMotion = true;
                        anim.CrossFade(AnimatorHashes.Spider.Animation.damaged, 0.2f);
                        damaged = false;
                        continue;
                    }
                    else if (attack1)
                    {
                        anim.applyRootMotion = true;
                        anim.CrossFade(AnimatorHashes.Spider.Animation.attack01, 0.2f);
                        attack1 = false;
                        continue;
                    }
                    else if (attack2)
                    {
                        anim.applyRootMotion = true;
                        anim.CrossFade(AnimatorHashes.Spider.Animation.attack02, 0.2f);
                        attack2 = false;
                        continue;
                    }


                }

                anim.SetFloat(AnimatorHashes.Spider.Param.speed, speed);
                yield return new WaitForFixedUpdate();
            }

            anim.Play(AnimatorHashes.Spider.Animation.die);
        }
    }
}