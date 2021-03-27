using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Animator controller state behavior.
	This behavior is used by human animator to properly override any other animation
	by the animation of falling. 
*/
public class ChangeLayerWeights : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetLayerWeight(layerIndex - 1, 1f);
        animator.SetLayerWeight(layerIndex, 0.75f);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetLayerWeight(layerIndex - 1, 0.8f);
        animator.SetLayerWeight(layerIndex, 1f);
    }

}
