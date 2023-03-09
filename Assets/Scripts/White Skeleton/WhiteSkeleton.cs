using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSkeleton : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speed;
    private int index;
    public bool isFlipped = false;
    [SerializeField] public int health;
    
    [Header("Components")]
    Animator myAnimator;
    public Transform player;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;
    private Vector3 respawnPoint;

    [Header("Attack")]
    public Vector3 attackOffset;
    [SerializeField] private float attackRange;
    public LayerMask attackMask;
    
    GameSession gameSession;
    public bool isFrozen = false;
    public float walkRange = 5f;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        respawnPoint = transform.position;
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
            Vector2 target = new Vector2(player.position.x, myRigidbody.position.y);
            Vector2 newPosition =Vector2.MoveTowards(myRigidbody.position, target, speed*Time.fixedDeltaTime);
            //myRigidbody.MovePosition(newPosition);
            if(Vector2.Distance(player.position, myRigidbody.position)<=walkRange)
            {
            Debug.Log("asda");
            myAnimator.SetBool("Walk",true);
            myRigidbody.MovePosition(newPosition);
            }
            else
            {
                myAnimator.SetBool("Walk",false);
            }
        }
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z*=-1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
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
            SoundManagerScript.PlaySound("bossHit");
            isFrozen = true;
            myAnimator.SetBool("Freeze",true);
            Invoke("unFreezeWhiteSkeleton",1f);
        }
        if(health<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(200);
            SoundManagerScript.PlaySound("bossDeath");
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            myAnimator.SetTrigger("Death");
        }
    }
    void returnToNormalState()
    {
        myAnimator.SetBool("Hit",false);
    }
    void unFreezeWhiteSkeleton()
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
        Invoke("backtoSpawnPoint",0.8f);
      }
    }
    void OnDrawGizmosSelected()
    {
    	Vector3 pos = transform.position;
    	pos += transform.right * attackOffset.x;
    	pos += transform.up * attackOffset.y;

    	Gizmos.DrawWireSphere(pos, attackRange);
    }
    void backtoSpawnPoint()
    {
        transform.position = respawnPoint;
    }
}
