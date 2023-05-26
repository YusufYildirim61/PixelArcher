using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGoo : MonoBehaviour
{
    [SerializeField] public float health;

    [Header("Components")]
    Animator myAnimator;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;
    
    [Header("Attack")]
    public Vector3 attackOffset;
    [SerializeField] private float attackRange;
    public LayerMask attackMask;

    
    GameSession gameSession;
    bool isFrozen = false;
    bool isPoisoned = false;
    int poisonDmgCount;
    bool poisonEffect = true;
    private Camera mainCamera;
    bool isInCameraRange = false;
    

   
    void Start()
    {
        
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        myCollider = GetComponent<PolygonCollider2D>();
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
        if(isFrozen)
        {
            return;
        }
        if(isPoisoned)
        {
            StartCoroutine("poisonDamage");
            if(health<=0)
            {
                StopCoroutine("poisonDamage");
                
            }
            if(poisonDmgCount==3)
            {
                isPoisoned = false;
                poisonDmgCount = 0;
            }
        }
        
        if(health>0 && !isPoisoned && !isFrozen)
        {
            Invoke("attack",0.7f);
            
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
            health--;
            myAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="StrongBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            health-=2;
            myAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="IceBullet")
        {
            isFrozen = true;
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("iceImpact");
            }
            myAnimator.SetBool("Freeze",true);
            Invoke("unFreezeIdleGoo",1f);
            
        }
        if(other.tag == "PoisonBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("poisonImpact");
            }
           isPoisoned = true; 
        }
        if(health<=0 ||(health<=0 && isFrozen))
        {
            FindObjectOfType<LevelComplete>().creatureKilled(150);
            myAnimator.SetTrigger("Death");
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("gooDeath");
            }
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
    }
    IEnumerator poisonDamage()
    {

        if(poisonEffect)
        {
            poisonDmgCount++;
            health-=0.5f;
            poisonEffect = false;
            myAnimator.SetBool("Poison",true);
            Invoke("stopPoisonEffect",0.2f);
            if(health<=0)
            {
                stopPoisonEffect();
                myAnimator.SetTrigger("Death");
                FindObjectOfType<LevelComplete>().creatureKilled(150);
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("gooDeath");
                }
                myCollider.enabled = false;
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }
  
    }
    void stopPoisonEffect()
    {
        myAnimator.SetBool("Poison",false);  
    }
    void returnToNormalState()
    {
        myAnimator.SetBool("Hit",false);
    }
    void unFreezeIdleGoo()
    {
        myAnimator.SetBool("Freeze",false);
        isFrozen = false;
        if(health<=0)
        {
            myAnimator.SetTrigger("Death");
        }
    }

    public void Attack()
    {
      Vector3 pos = transform.position;
      pos+= transform.right*attackOffset.x;
      pos+=transform.up* attackOffset.y;
      if(isInCameraRange)
      {
        SoundManagerScript.PlaySound("idleGooAttack");
      }
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
        if(health>0)
        {
            myAnimator.SetTrigger("Attack");
        }
        else if(health<=0)
        {
            myAnimator.SetTrigger("Death");
        }
        
        
        
    }
}
