using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Spider animation controller that overrides base class methods.
	*/
    public class SpiderAnimationController : EnemyAnimationController
    {
        public override bool CheckForAction()
        {
            isInAction = !anim.GetBool(AnimatorHashes.Spider.Param.canMove);
            return isInAction;
        }

        public override void PerformAttack01()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Spider.Param.canMove, false);
            anim.Play(AnimatorHashes.Spider.Animation.attack01);
        }

        public override void PerformAttack02()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Spider.Param.canMove, false);
            anim.Play(AnimatorHashes.Spider.Animation.attack02);
        }

        protected override void SetUpAnimator()
        {
            anim.SetFloat(AnimatorHashes.Spider.Param.speed, 0f);
            anim.SetBool(AnimatorHashes.Spider.Param.isDead, false);
        }

        protected override void UpdateParamaters()
        {
        }

        protected override void UserDefInit()
        {
        }

        public void Moving()
        {
            anim.SetFloat(AnimatorHashes.Spider.Param.speed, speed);
        }
    }
}