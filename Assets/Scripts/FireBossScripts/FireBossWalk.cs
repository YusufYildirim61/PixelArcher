using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public  float attackRange = 3f;
    Transform player;
    Rigidbody2D rb;
    FireBoss fireBoss;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        player =  GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        fireBoss = animator.GetComponent<FireBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(fireBoss.isFrozen)
        {
            return;
        }
        if(fireBoss.isPoisoned)
        {
            fireBoss.LookAtPlayer();
            
                Debug.Log("asg");
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPosition =Vector2.MoveTowards(rb.position, target, speed*0.5f*Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
                

                if(Vector2.Distance(player.position, rb.position)<=attackRange)
                {
                    animator.SetTrigger("Attack");
                }
            
            
            
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPosition =Vector2.MoveTowards(rb.position, target, speed*Time.fixedDeltaTime);
            fireBoss.LookAtPlayer();
            rb.MovePosition(newPosition);

                if(Vector2.Distance(player.position, rb.position)<=attackRange)
                {
                    animator.SetTrigger("Attack");
                }
            
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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