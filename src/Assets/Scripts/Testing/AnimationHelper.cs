using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Atlas.Core;

namespace Atlas.Combat
{

	/**
		For testing purposes.
		For testing of the Human animator.
	*/
    [RequireComponent(typeof(Animator))]
    public class AnimationHelper : MonoBehaviour
    {

        [SerializeField] [Range(-1, 1)] private float vertical;
        [SerializeField] [Range(-1, 1)] private float horizontal;

        [SerializeField] private bool isArmed;
        [SerializeField] private bool attack;
        [SerializeField] private bool focused;

        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            anim.applyRootMotion = false;
        }

        // Update is called once per frame
        void Update()
        {
            anim.applyRootMotion = !anim.GetBool(AnimatorHashes.Human.Param.canMove);

            if (attack && !anim.applyRootMotion)
            {
                anim.applyRootMotion = true;
                anim.CrossFade(AnimatorHashes.Human.Animation.attack01, 0.2f);
                attack = false;
                return;
            }

            anim.SetBool(AnimatorHashes.Human.Param.isArmed, isArmed);
            anim.SetBool(AnimatorHashes.Human.Param.isFocused, focused);
            anim.SetFloat(AnimatorHashes.Human.Param.vertical, vertical);
            anim.SetFloat(AnimatorHashes.Human.Param.horizontal, horizontal);
        }
    }
}