using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    CannonTrap cannonTrap;
    public float canonBulletSpeed = 10f;
    Rigidbody2D myRigidbody;
    float xSpeed;
    
    void Start()
    {
        cannonTrap = FindObjectOfType<CannonTrap>();
        xSpeed = cannonTrap.transform.localScale.x * canonBulletSpeed;
        transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(xSpeed,0f);
      //cannonTrap = FindObjectOfType<CannonTrap>();
      //xSpeed = cannonTrap.transform.localScale.x * canonBulletSpeed;
      //transform.localScale = new Vector2((Mathf.Sign(xSpeed)) * transform.localScale.x, transform.localScale.y);
    }
    
    // Update is called once per frame
    void Update()
    {
        //myRigidbody.velocity = new Vector2(xSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
      
      if(other.tag=="Platform" || other.tag=="GiantBeetle")
      {
        Destroy(gameObject);
      }
      
    }  
}
