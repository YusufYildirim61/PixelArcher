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
    void Start() 
    {
        gameSession = FindObjectOfType<GameSession>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        key.SetActive(false);
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<PolygonCollider2D>();
        bossAnimator = GetComponent<Animator>();
    }
      
    void Update() 
    {
        if(!playerMovement.hasKey && !playerMovement.isStopped)
        {
           key.transform.Rotate(new Vector3(0,2,0));
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
        if(other.tag=="Bullet" && gameSession.isOnDefaultArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            bossHealth--;
            bossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="Bullet" && gameSession.isOnStrongArrow)
        {
            SoundManagerScript.PlaySound("bossHit");
            bossHealth-=2;
            bossAnimator.SetBool("Hit",true);
            Invoke("returnToNormalState",0.2f);
        }
        if(other.tag=="Bullet" && gameSession.isOnIceArrow)
        {
            isFrozen = true;
            SoundManagerScript.PlaySound("bossHit");
            bossAnimator.SetBool("Freeze",true);
            Invoke("unFreezeBoss",1f);
        }
        if(bossHealth<=0)
        {
            SoundManagerScript.PlaySound("bossDeath");
            myCollider.enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            bossAnimator.SetTrigger("Death");
            key.SetActive(true);
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().Stop();
        }
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
