using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Atlas.Core
{
	/**
		Troll animation controller that overrides base class methods.
	*/
    public class TrollAnimationController : EnemyAnimationController
    {

        public override bool CheckForAction()
        {
            isInAction = !anim.GetBool(AnimatorHashes.Troll.Param.canMove);
            return isInAction;
        }

        protected override void SetUpAnimator()
        {
            anim.SetFloat(AnimatorHashes.Troll.Param.speed, 0f);
            anim.SetBool(AnimatorHashes.Troll.Param.isMoving, false);
            anim.SetBool(AnimatorHashes.Troll.Param.isDead, false);
        }

        protected override void UserDefInit()
        {
           
        }

        public void Idle()
        {
            anim.SetBool(AnimatorHashes.Troll.Param.isMoving, false);
            anim.SetFloat(AnimatorHashes.Troll.Param.speed, 0);
        }

        public void Moving()
        {
            anim.SetBool(AnimatorHashes.Troll.Param.isMoving, true);
            anim.SetFloat(AnimatorHashes.Troll.Param.speed, speed);
        }

        public override void PerformAttack01()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Troll.Param.canMove, false);
            anim.Play(AnimatorHashes.Troll.Animation.attack01);
        }

        public override void PerformAttack02()
        {
            isInAction = true;
            anim.SetBool(AnimatorHashes.Troll.Param.canMove, false);
            anim.Play(AnimatorHashes.Troll.Animation.attack02);
        }

        protected override void UpdateParamaters()
        {
        }
    }
}