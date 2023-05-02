using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    Animator iceBossAnimator;
    public float iceBossHealth = 30f;
    public BoxCollider2D iceBossCollider;
    public Rigidbody2D iceBossRigidbody;
    playerMovement playerMovement;
    GameSession gameSession;
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
        iceBossRigidbody = GetComponent<Rigidbody2D>();
        iceBossCollider = GetComponent<BoxCollider2D>();
        iceBossAnimator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if(iceBossHealth<=0)
        {
            return;
        }
        if(fightAreaTrigger.isInFightArea)
        {
            iceBossAnimator.SetBool("Walk",true);
        }
        else
        {
            iceBossAnimator.SetBool("Walk",false);
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
            if(iceBossHealth<=0)
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
            iceBossHealth--;
            iceBossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="StrongBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            iceBossHealth-=2;
            iceBossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="IceBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            
        }
        if(other.tag == "PoisonBullet")
        {
           if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
           isPoisoned = true; 
        }
        if(iceBossHealth<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossDeath");
            }
            iceBossCollider.enabled = false;
            iceBossRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            iceBossAnimator.SetTrigger("Death");
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
        }
    }
    IEnumerator poisonDamage()
    {

        if(poisonEffect)
        {
            poisonDmgCount++;
            iceBossHealth-=0.5f;
            poisonEffect = false;
            iceBossAnimator.SetBool("Poison",true);
            Invoke("stopPoisonEffect",0.2f);
            if(iceBossHealth<=0)
            {
                stopPoisonEffect();
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("bossDeath");
                }
                iceBossCollider.enabled = false;
                iceBossRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                iceBossAnimator.SetTrigger("Death");
                FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
            }
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }
  
    }
    void stopPoisonEffect()
    {
        iceBossAnimator.SetBool("Poison",false);  
    }
    void returnToNormalState()
    {
        iceBossAnimator.SetBool("Hit",false);
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
