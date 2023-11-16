using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo_Walk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public  float attackRange = 3f;
    
    Transform player;
    
    Rigidbody2D rb;

    Goo goo;
    FightAreaTrigger fightAreaTrigger;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player =  GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        goo = animator.GetComponent<Goo>();
        fightAreaTrigger = goo.GetComponentInParent<FightAreaTrigger>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(goo.isFrozen)
        {
            return;
        }
        else
        {
            if(goo.health<=0)
            {
                animator.SetTrigger("Death");
                return;
            }
            goo.LookAtPlayer();
            if(Mathf.Abs(player.position.x-rb.position.x)<=attackRange && fightAreaTrigger.isInFightArea)
            {
                animator.SetTrigger("Attack");
            }
            
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
