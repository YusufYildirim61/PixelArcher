using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    Animator bossAnimator;
    public float bossHealth = 30f;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;
    public GameObject key;
    playerMovement playerMovement;
    GameSession gameSession;
    public bool isFrozen = false;
    public bool isPoisoned = false;
    int poisonDmgCount;
    bool poisonEffect = true;
    private Camera mainCamera;
    bool isInCameraRange = false;
    
    
    void Start() 
    {
        
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        key.SetActive(false);
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<PolygonCollider2D>();
        bossAnimator = GetComponent<Animator>();
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
        if(!playerMovement.hasKey && !playerMovement.isStopped)
        {
           key.transform.Rotate(new Vector3(0,2,0));
        }
        if(isPoisoned)
        {
            
            StartCoroutine("poisonDamage");
            if(bossHealth<=0)
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
            bossHealth--;
            bossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="StrongBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            bossHealth-=2;
            bossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="IceBullet")
        {
            isFrozen = true;
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            bossAnimator.SetBool("Freeze",true);
            Invoke("unFreezeBoss",1f);
        }
        if(other.tag == "PoisonBullet")
        {
           if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
           isPoisoned = true; 
        }
        if(bossHealth<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossDeath");
            }
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            bossAnimator.SetTrigger("Death");
            key.SetActive(true);
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
        }
    }
    IEnumerator poisonDamage()
    {

        if(poisonEffect)
        {
            poisonDmgCount++;
            bossHealth-=0.5f;
            poisonEffect = false;
            bossAnimator.SetBool("Poison",true);
            Invoke("stopPoisonEffect",0.2f);
            if(bossHealth<=0)
            {
                stopPoisonEffect();
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("bossDeath");
                }
                myCollider.enabled = false;
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                bossAnimator.SetTrigger("Death");
                key.SetActive(true);
                FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
            }
            yield return new WaitForSeconds(0.8f);
            poisonEffect = true;
             
        }
  
    }
    void stopPoisonEffect()
    {
        bossAnimator.SetBool("Poison",false);  
    }
    void returnToNormalState()
    {
        bossAnimator.SetBool("Hit",false);
    }
    void unFreezeBoss()
    {
        bossAnimator.SetBool("Freeze",false);
        isFrozen = false;
    }
    
}
