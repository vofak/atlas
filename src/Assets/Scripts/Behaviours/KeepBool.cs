using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Animator controller state behavior.
	This behavior is used to keep a certain animator parameter set to specific value for 
	as long as this state is active.
*/
public class KeepBool : StateMachineBehaviour {

    public string boolName;
    public bool valueToKeep;
    public bool resetOnExit = true;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, valueToKeep);
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetOnExit)
        {
            animator.SetBool(boolName, ! valueToKeep);
        }

    }
}
