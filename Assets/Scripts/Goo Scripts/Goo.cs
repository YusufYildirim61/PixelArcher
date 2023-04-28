using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goo : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float speed;
    public bool isFlipped = false;
    [SerializeField] public float health;
    
    [Header("Components")]
    Animator myAnimator;
    public Transform player;
    public PolygonCollider2D myCollider;
    public Rigidbody2D myRigidbody;
    private Vector3 respawnPoint;

    [Header("Attack")]
    public Vector3 attackOffset;
    [SerializeField] private float attackRange;
    public LayerMask attackMask;
    GameSession gameSession;
    
    public bool isFrozen = false;
    bool isPoisoned = false;
    int poisonDmgCount;
    bool poisonEffect = true;
    
    private Camera mainCamera;
    bool isInCameraRange = false;

    FightAreaTrigger fightAreaTrigger;
    void Start()
    {
        fightAreaTrigger = GetComponentInParent<FightAreaTrigger>();
        mainCamera = Camera.main;
        gameSession = FindObjectOfType<GameSession>();
        respawnPoint = transform.position;
        myCollider = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
            Vector2 target = new Vector2(player.position.x, myRigidbody.position.y);
            Vector2 newPosition =Vector2.MoveTowards(myRigidbody.position, target, speed*0.5f*Time.fixedDeltaTime);
            if(/*Mathf.Abs(player.position.x-myRigidbody.position.x)<=walkRange  &&*/ fightAreaTrigger.isInFightArea)
            {
            
                myAnimator.SetBool("Walk",true);
                myRigidbody.MovePosition(newPosition);
            }
            else
            {
                myAnimator.SetBool("Walk",false);
            }
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
        else
        {
            Vector2 target = new Vector2(player.position.x, myRigidbody.position.y);
            Vector2 newPosition =Vector2.MoveTowards(myRigidbody.position, target, speed*Time.fixedDeltaTime);
            if(/*Mathf.Abs(player.position.x-myRigidbody.position.x)<=walkRange  &&*/ fightAreaTrigger.isInFightArea)
            {
            
                myAnimator.SetBool("Walk",true);
                myRigidbody.MovePosition(newPosition);
            }
            else
            {
                myAnimator.SetBool("Walk",false);
            }
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
                SoundManagerScript.PlaySound("bossHit");
            }
            myAnimator.SetBool("Freeze",true);
            Invoke("unFreezeGoo",1f);
        }
        if(other.tag == "PoisonBullet")
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossHit");
            }
            isPoisoned = true; 
        }
        if(health<=0)
        {
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("bossDeath");
            }
            FindObjectOfType<LevelComplete>().creatureKilled(200);
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
                if(isInCameraRange)
                {
                    SoundManagerScript.PlaySound("bossDeath");
                }
                FindObjectOfType<LevelComplete>().creatureKilled(200);
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
    void unFreezeGoo()
    {
        myAnimator.SetBool("Freeze",false);
        isFrozen = false;
    }

    public void Attack()
    {
      Vector3 pos = transform.position;
      pos+= transform.right*attackOffset.x;
      pos+=transform.up* attackOffset.y;

      Collider2D colInfo = Physics2D.OverlapCircle(pos,attackRange,attackMask);
      if(colInfo != null)
      {

        colInfo.GetComponent<playerMovement>().isTouchedHazards = true;
        Invoke("backtoSpawnPoint",0.8f);
      }
    }
    void OnDrawGizmosSelected()
    {
    	Vector3 pos = transform.position;
    	pos += transform.right * attackOffset.x;
    	pos += transform.up * attackOffset.y;

    	Gizmos.DrawWireSphere(pos, attackRange);
    }
    void backtoSpawnPoint()
    {
        transform.position = respawnPoint;
    }
}
