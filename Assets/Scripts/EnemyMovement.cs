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
    bool isPoisoned = false;
    public GameObject enemyBlood;
    int poisonDmgCount;
    bool poisonEffect = true;
    private Camera mainCamera;
    bool isInCameraRange = false;

    
    void Start()
    {
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    
    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            isInCameraRange = true;
        }
        else
        {
            isInCameraRange = false;
        }
        if(isFrozen && enemyHealth>0)
        {
            return;
        }
        if(enemyHealth<=0)
        {
            if(isInCameraRange)
            {
                
                SoundManagerScript.PlaySound("enemyDeath"); 
            }
            FindObjectOfType<LevelComplete>().creatureKilled(100);
            Destroy(gameObject);
        }
        if(isPoisoned)
        {
            myRigidbody.velocity = new Vector2(moveSpeed*0.5f,0);
            StartCoroutine("poisonDamage");
            if(poisonDmgCount==3)
            {
                
                isPoisoned = false;
                poisonDmgCount = 0;
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(moveSpeed,0);
        }
        
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet")
        {
            Instantiate(enemyBlood, transform.position,Quaternion.identity);
            mySpriteRenderer.color = Color.red;
            enemyHealth -=1;
            if(enemyHealth==1)
            {
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("bossHit");
                }
                Invoke("turntoNormalColor",0.3f);
                
            }    
        }
        if(other.tag == "StrongBullet")
        {
            enemyHealth-=2;
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            Instantiate(enemyBlood, transform.position,Quaternion.identity);
        }
        if(other.tag == "IceBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("enemyHit");
            }
            myAnimator.SetBool("Freeze",true);
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            isFrozen = true;
            Invoke("unFreezeEnemy",1f);
        }
        if(other.tag == "PoisonBullet")
        {
           if(isInCameraRange)
           {
               SoundManagerScript.PlaySound("enemyHit");
           }
           isPoisoned = true; 
        }
            
        
    }
    IEnumerator poisonDamage()
    {
        if(poisonEffect)
        {
            poisonDmgCount++;
            enemyHealth-=0.5f;
            poisonEffect = false;
            myAnimator.SetBool("Poison",true);
            if(isInCameraRange)
            {
               SoundManagerScript.PlaySound("enemyHit");
            }
            Invoke("stopPoisonEffect",0.2f);
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }   
    }
    
    void stopPoisonEffect()
    {
        myAnimator.SetBool("Poison",false);  
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
