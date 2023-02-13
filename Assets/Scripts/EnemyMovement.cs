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
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed,0);
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet")
        {
            mySpriteRenderer.color = Color.red;
            enemyHealth -=1;
            if(enemyHealth==1)
            {
                
                Invoke("turntoNormalColor",0.3f);
                SoundManagerScript.PlaySound("enemyHit");
            }
            
        }    
        if(enemyHealth<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(100);
            SoundManagerScript.PlaySound("enemyDeath");
            Destroy(gameObject);
        }
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
