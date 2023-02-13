using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThree : MonoBehaviour
{
    [SerializeField] public float bulletSpeed = 10f;
    
    [SerializeField] AudioClip beetleDeathSFX;
    Rigidbody2D myRigidbody;
    playerMovement player;
     float xSpeed,ySpeed;
    EnemyMovement enemyMovement;
    LevelComplete levelComplete;
    
    public GameObject enemyBlood;
    
    public GameObject beetleBlood;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        levelComplete = FindObjectOfType<LevelComplete>();
        enemyMovement = FindObjectOfType<EnemyMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        ySpeed = player.transform.localScale.y * bulletSpeed;
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x,(Mathf.Sign(ySpeed)) * transform.localScale.y);
        transform.Rotate(new Vector3(0,0,(Mathf.Sign(xSpeed))*-30f));
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed,-ySpeed/3);
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
      if(other.tag=="Platform" ||other.tag=="GiantBeetle")
      {
        Destroy(gameObject);
      }
      
    }
}
