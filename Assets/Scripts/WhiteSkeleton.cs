using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSkeleton : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] Vector3[] positions;
    Animator myAnimator;
    private int index;
    public bool isFlipped = false;
    public Transform player;
    [SerializeField] private int health;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;

    [Header("Attack")]
    public Vector3 attackOffset;
    [SerializeField] private float attackRange;
    public LayerMask attackMask;
    void Start()
    {
        myCollider = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        Attack();   
        if(Vector2.Distance(player.position, myRigidbody.position)<=attackRange)
        {
            
            transform.position = Vector2.MoveTowards(transform.position,positions[index],Time.deltaTime*0);
            myAnimator.SetTrigger("Attack");
            //myRigidbody.velocity = new Vector2(0,0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position,positions[index],Time.deltaTime*speed);
            myAnimator.ResetTrigger("Attack");
            //myRigidbody.velocity = new Vector2(speed,0);
        }
        

        if(transform.position == positions[index])
        {
            if(index == positions.Length -1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet")
        {
            SoundManagerScript.PlaySound("bossHit");
            health--;
            myAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(health<=0)
        {
            SoundManagerScript.PlaySound("bossDeath");
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            myAnimator.SetTrigger("Death");
            
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
        }
    }
    void returnToNormalState()
    {
        myAnimator.SetBool("Hit",false);
    }

    public void Attack()
    {
      Vector3 pos = transform.position;
      pos+= transform.right*attackOffset.x;
      pos+=transform.up* attackOffset.y;

      Collider2D colInfo = Physics2D.OverlapCircle(pos,attackRange,attackMask);
      if(colInfo != null)
      {
          colInfo.GetComponent<playerMovement>().Die();
      }
    }
    void OnDrawGizmosSelected()
    {
    	Vector3 pos = transform.position;
    	pos += transform.right * attackOffset.x;
    	pos += transform.up * attackOffset.y;

    	Gizmos.DrawWireSphere(pos, attackRange);
    }
}
