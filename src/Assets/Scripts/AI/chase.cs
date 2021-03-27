using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	For testing purposes.
	Chases the player with no regard to environment.
*/
public class chase : MonoBehaviour {
    public Transform player;
    static Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 45)
        {
            anim.SetBool("isIdle", false);
            
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if(direction.magnitude > 2)
            {
                this.transform.Translate(0, 0, 0.05f);
                
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }

        
	}
}
