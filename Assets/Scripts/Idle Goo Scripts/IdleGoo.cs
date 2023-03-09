using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGoo : MonoBehaviour
{
    [SerializeField] public int health;

    [Header("Components")]
    Animator myAnimator;
    public Transform player;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;
    
    [Header("Attack")]
    public Vector3 attackOffset;
    [SerializeField] private float attackRange;
    public LayerMask attackMask;

    public float walkRange = 5f;
    GameSession gameSession;
    bool isFrozen = false;
    
    
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myCollider = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(isFrozen)
        {
            return;
        }
        else
        {
            Invoke("attack",0.4f);
        }
            
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet" && gameSession.isOnDefaultArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            health--;
            myAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="Bullet" && gameSession.isOnStrongArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            health-=2;
            myAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="Bullet" && gameSession.isOnIceArrow)
        {
            isFrozen = true;
            SoundManagerScript.PlaySound("bossHit");
            myAnimator.SetBool("Freeze",true);
            Invoke("unFreezeIdleGoo",1f);
            
        }
        if(health<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(150);
            myAnimator.SetTrigger("Death");
            SoundManagerScript.PlaySound("bossDeath");
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
    }
    void returnToNormalState()
    {
        myAnimator.SetBool("Hit",false);
    }
    void unFreezeIdleGoo()
    {
        myAnimator.SetBool("Freeze",false);
        isFrozen = false;
    }

    public void Attack()
    {
      Vector3 pos = transform.position;
      pos+= transform.right*attackOffset.x;
      pos+=transform.up* attackOffset.y;

      Collider2D colInfo = Physics2D.OverlapCircle(pos,attackRange,attackMask);
      if(colInfo != null)
      {

        colInfo.GetComponent<playerMovement>().isTouchedHazards = true;
        
      }
    }
    void OnDrawGizmosSelected()
    {
    	Vector3 pos = transform.position;
    	pos += transform.right * attackOffset.x;
    	pos += transform.up * attackOffset.y;

    	Gizmos.DrawWireSphere(pos, attackRange);
    }
    void attack()
    {
        myAnimator.SetTrigger("Attack");
    }
}
