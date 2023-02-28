using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSkeleton_Walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public  float attackRange = 3f;
    public float walkRange = 5f;
    Transform player;
    
    Rigidbody2D rb;

    WhiteSkeleton whiteSkeleton;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player =  GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        whiteSkeleton = animator.GetComponent<WhiteSkeleton>();
    }

     
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        whiteSkeleton.LookAtPlayer();
        if(Vector2.Distance(player.position, rb.position)<=attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");  
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}