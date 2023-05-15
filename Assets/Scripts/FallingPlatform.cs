using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Animator myAnimator;
    BoxCollider2D myboxCollider;
    playerMovement playerMovement;
    Rigidbody2D rb;
    float timer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myboxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag =="Player")
        {
            Invoke("destroyPlatform",0.8f);   
        }
        
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            Invoke("rebuildPlatform",1.5f);
        }
    }
    
    void Update()
    {
        
    }
    void destroyPlatform()
    {
        myboxCollider.enabled = false;    
        myAnimator.SetBool("isTouched",true);
    }
    void rebuildPlatform()
    {
        
        myAnimator.SetBool("isTouched",false);
        myboxCollider.enabled = true;
    }
    void playFallingPlatformSFX()
    {
        SoundManagerScript.PlaySound("arrowShot");
    }
    
}
