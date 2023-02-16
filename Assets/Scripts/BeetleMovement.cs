using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
     
    Rigidbody2D myRigidbody;
    
    public float beetleHealth = 1f;
    void Start()
    {   
        
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(0,moveSpeed);
       
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Bullet")
        {
            beetleHealth -=1;
            
        }    
        if(beetleHealth<=0)
        {
            FindObjectOfType<LevelComplete>().creatureKilled(50);
            SoundManagerScript.PlaySound("beetleDeath");
            Destroy(gameObject);
        }
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
        transform.localScale = new Vector2(1,-(Mathf.Sign(myRigidbody.velocity.y)));
    }
    

}