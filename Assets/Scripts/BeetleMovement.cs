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
    bool isPoisoned = false;
    GameSession gameSession;
    Animator myAnimator;
    int poisonDmgCount;
    bool poisonEffect = true;
    private Camera mainCamera;
    bool isInCameraRange = false;
    void Start()
    {   
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        myboxCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
        if(isFrozen && beetleHealth>0)
        {
            return;
        }
        if(beetleHealth<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("beetleDeath");
            }
            FindObjectOfType<LevelComplete>().creatureKilled(50);
            Destroy(gameObject);
        }
        if(isPoisoned)
        {
            myRigidbody.velocity = new Vector2(0,moveSpeed*0.5f);
            StartCoroutine("poisonDamage");
            if(poisonDmgCount==3)
            {
                
                isPoisoned = false;
                poisonDmgCount = 0;
            }
        }
        
        else
        {
            myRigidbody.velocity = new Vector2(0,moveSpeed);
            FlipEnemy();
        }
        
       
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            beetleHealth -=1;
            
        }
        if(other.tag =="StrongBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            beetleHealth -=1;
            
        }
        if(other.tag=="IceBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("iceImpact");
            }
            myAnimator.SetBool("Freeze",true);
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            isFrozen = true;
            Invoke("unFreezeBeetle",1f);
            
        }
        if(other.tag == "PoisonBullet")
        {
          if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("poisonImpact");
            }
           isPoisoned = true; 
        }       
        
    }
    IEnumerator poisonDamage()
    {
        if(poisonEffect)
        {
            poisonDmgCount++;
            beetleHealth-=0.5f;
            poisonEffect = false;
            myAnimator.SetBool("Poison",true);
            Invoke("stopPoisonEffect",0.2f);
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }
  
    }
    void stopPoisonEffect()
    {
        myAnimator.SetBool("Poison",false);  
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
