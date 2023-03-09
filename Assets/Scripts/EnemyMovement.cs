using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
   
    Rigidbody2D myRigidbody;
    public float enemyHealth = 2f;
    bool colliderType;
    Animator myAnimator;
    
    SpriteRenderer mySpriteRenderer;
    GameSession gameSession;
    bool isFrozen;
    public GameObject enemyBlood;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    
    void Update()
    {
        if(isFrozen)
        {
            return;
        }
        else
        {
            myRigidbody.velocity = new Vector2(moveSpeed,0);
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet" && gameSession.isOnDefaultArrow)
        {
            Instantiate(enemyBlood, transform.position,Quaternion.identity);
            mySpriteRenderer.color = Color.red;
            enemyHealth -=1;
            if(enemyHealth==1)
            {
                Invoke("turntoNormalColor",0.3f);
                SoundManagerScript.PlaySound("enemyHit");
            }    
        }
        if(other.tag == "Bullet" && gameSession.isOnStrongArrow)
        {
            enemyHealth-=2;
            Instantiate(enemyBlood, transform.position,Quaternion.identity);
        }
        if(other.tag == "Bullet" && gameSession.isOnIceArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            myAnimator.SetBool("Freeze",true);
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            isFrozen = true;
            Invoke("unFreezeEnemy",1f);
        }    
        if(enemyHealth<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(100);
            SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject);
        }
    }
    void unFreezeEnemy()
    {
        myAnimator.SetBool("Freeze",false);
        isFrozen = false;
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag=="Platform")
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
        
                
    }
    

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)),1);
         
    }
    void turntoNormalColor()
    {
        mySpriteRenderer.color = Color.white;
    }

    
}
