using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
     
    Rigidbody2D myRigidbody;
    
    public float beetleHealth = 1f;
    BoxCollider2D myboxCollider;
    bool isFrozen = false;
    GameSession gameSession;
    Animator myAnimator;
    void Start()
    {   
        gameSession = FindObjectOfType<GameSession>();
        myboxCollider = GetComponent<BoxCollider2D>();
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
            myRigidbody.velocity = new Vector2(0,moveSpeed);
            FlipEnemy();
        }
        
       
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet" && (gameSession.isOnDefaultArrow || gameSession.isOnStrongArrow || gameSession.isOnPoisonArrow))
        {
            beetleHealth -=1;
            
        }
        if(other.tag=="Bullet" && gameSession.isOnIceArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            myAnimator.SetBool("Freeze",true);
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            isFrozen = true;
            Invoke("unFreezeBeetle",1f);
            
        }       
        if(beetleHealth<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(50);
            SoundManagerScript.PlaySound("beetleDeath");
            Destroy(gameObject);
        }
    }
    void unFreezeBeetle()
    {
        myAnimator.SetBool("Freeze",false);
        isFrozen = false;
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag=="Platform")
        {
            //moveSpeed *= -1f;
            //FlipEnemyFacing();
        }        
    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(1,-(Mathf.Sign(myRigidbody.velocity.y)));
    }
    
    void FlipEnemy()
    {
      if(myboxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
      {
            moveSpeed *= -1f;
            FlipEnemyFacing();
      }
    }
}
