using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core {

	/**
		Skeleton animation controller that overrides base class methods.
	*/
    public class SkeletonAnimationController : EnemyAnimationController
    {
        public override bool CheckForAction()
        {
            isInAction = !anim.GetBool(AnimatorHashes.Skeleton.Param.canMove);
            return isInAction;
        }

        protected override void SetUpAnimator()
        {
            anim.SetFloat(AnimatorHashes.Skeleton.Param.speed, 0f);
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
            anim.SetFloat(AnimatorHashes.Skeleton.Param.speed, speed);
        }

        public void Moving()
        {
            anim.SetFloat(AnimatorHashes.Skeleton.Param.speed, speed);
        }

        public override void PerformAttack01()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Skeleton.Param.canMove, false);
            anim.Play(AnimatorHashes.Skeleton.Animation.attack01);
        }

        public override void PerformAttack02()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Skeleton.Param.canMove, false);
            anim.Play(AnimatorHashes.Skeleton.Animation.attack02);
        }

    }
}