using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    Animator fireBossAnimator;
    public float fireBossHealth = 30f;
    public BoxCollider2D fireBossCollider;
    public Rigidbody2D fireBossRigidbody;
    playerMovement playerMovement;
    GameSession gameSession;
    public bool isFrozen = false;
    public bool isPoisoned = false;
    int poisonDmgCount;
    bool poisonEffect = true;
    private Camera mainCamera;
    bool isInCameraRange = false;
    
    [Header("Attack Settings")]
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    FightAreaTrigger fightAreaTrigger;
    
    
    void Start()
    {
        fightAreaTrigger = GetComponentInParent<FightAreaTrigger>();
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        fireBossRigidbody = GetComponent<Rigidbody2D>();
        fireBossCollider = GetComponent<BoxCollider2D>();
        fireBossAnimator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(fireBossHealth<=0)
        {
            return;
        }
        if(fightAreaTrigger.isInFightArea)
        {
            fireBossAnimator.SetBool("Walk",true);
        }
        else
        {
            fireBossAnimator.SetBool("Walk",false);
        }
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            isInCameraRange = true;
        }
        else
        {
            isInCameraRange = false;
        }
        if(isPoisoned)
        {
            
            StartCoroutine("poisonDamage");
            if(fireBossHealth<=0)
            {
                StopCoroutine("poisonDamage");
            }
            if(poisonDmgCount==3)
            {
                isPoisoned = false;
                poisonDmgCount = 0;
            }
        }
        
        else
        {
            return;
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
        if(other.tag=="Bullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            fireBossHealth--;
            fireBossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="StrongBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            fireBossHealth-=2;
            fireBossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="IceBullet")
        {
            isFrozen = true;
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            fireBossHealth-=2;
            fireBossAnimator.SetBool("Freeze",true);
            Invoke("unFreezeBoss",2f);
        }
        if(other.tag == "PoisonBullet")
        {
           if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
           isPoisoned = true; 
        }
        if(fireBossHealth<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossDeath");
            }
            fireBossCollider.enabled = false;
            fireBossRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            fireBossAnimator.SetTrigger("Death");
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
        }
    }
    IEnumerator poisonDamage()
    {

        if(poisonEffect)
        {
            poisonDmgCount++;
            fireBossHealth-=0.5f;
            poisonEffect = false;
            fireBossAnimator.SetBool("Poison",true);
            Invoke("stopPoisonEffect",0.2f);
            if(fireBossHealth<=0)
            {
                stopPoisonEffect();
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("bossDeath");
                }
                fireBossCollider.enabled = false;
                fireBossRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                fireBossAnimator.SetTrigger("Death");
                FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
            }
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }
  
    }
    void stopPoisonEffect()
    {
        fireBossAnimator.SetBool("Poison",false);  
    }
    void returnToNormalState()
    {
        fireBossAnimator.SetBool("Hit",false);
    }
    void unFreezeBoss()
    {
        fireBossAnimator.SetBool("Freeze",false);
        isFrozen = false;
        if(fireBossHealth<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossDeath");
            }
            fireBossCollider.enabled = false;
            fireBossRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            fireBossAnimator.SetTrigger("Death");
            
        }
        
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos+= transform.right*attackOffset.x;
        pos+=transform.up* attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos,attackRange,attackMask);
        if(colInfo != null)
        {
            if(!playerMovement.isDamaged)
            {
                colInfo.GetComponent<playerMovement>().DamagedbySecondBoss();
            }
            
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
