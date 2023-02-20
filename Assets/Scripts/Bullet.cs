using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float bulletSpeed = 10f;
    
    [SerializeField] AudioClip beetleDeathSFX;
    Rigidbody2D myRigidbody;
    BoxCollider2D myboxCollider;
    playerMovement player;
     float xSpeed;
    EnemyMovement enemyMovement;
    LevelComplete levelComplete;
    
    public GameObject enemyBlood;
    
    public GameObject beetleBlood;
    
    
   
    void Start()
    {
        
        myboxCollider = GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        levelComplete = FindObjectOfType<LevelComplete>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,0f);
        destroyArrow();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
      if(other.tag=="Enemy" && other.isTrigger.Equals(false))
      {
        Instantiate(enemyBlood, transform.position,Quaternion.identity);
        Destroy(gameObject);
      }  
      if(other.tag=="Beetle" && other.isTrigger.Equals(false))
      {
        Instantiate(beetleBlood, transform.position,Quaternion.identity);
        Destroy(gameObject); 
      }
      if(other.tag=="Platform" ||other.tag=="GiantBeetle" || other.tag=="Boss")
      {
        Destroy(gameObject);
      }
      
    }
    void destroyArrow()
    {
      if(myboxCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Hazards,Bouncing")))
      {
        Destroy(gameObject);
      }
    }
    
    
}
