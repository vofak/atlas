using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{

	/**
		Dragon animation controller that overrides base class methods.
	*/
    public class DragonAnimationController : EnemyAnimationController
    {

        public override bool CheckForAction()
        {
            isInAction = !anim.GetBool(AnimatorHashes.Dragon.Param.canMove);
            return isInAction;
        }

        protected override void SetUpAnimator()
        {
            anim.SetFloat(AnimatorHashes.Dragon.Param.speed, 0f);
            anim.SetBool(AnimatorHashes.Dragon.Param.isFlying, false);
            anim.SetBool(AnimatorHashes.Skeleton.Param.isDead, false);
        }

        protected override void UpdateParamaters()
        {

        }

        protected override void UserDefInit()
        {

        }

        public void Idle()
        {
            anim.SetFloat(AnimatorHashes.Dragon.Param.speed, speed);
        }

        public void Moving()
        {
            anim.SetFloat(AnimatorHashes.Dragon.Param.speed, speed);
        }

        public override void PerformAttack01()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Skeleton.Param.canMove, false);
            anim.Play(AnimatorHashes.Dragon.Animation.attack01);
        }

        public override void PerformAttack02()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Dragon.Param.canMove, false);
            anim.Play(AnimatorHashes.Dragon.Animation.attack02);
        }
    }
}