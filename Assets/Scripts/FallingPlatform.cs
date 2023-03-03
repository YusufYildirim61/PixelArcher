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
    [SerializeField] float delay =1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myboxCollider = GetComponent<BoxCollider2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider==playerMovement.myBodyCollider)
        {
            
            Invoke("destroyPlatform",1.5f);
            
        }
        
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>delay)
        {
            rebuildPlatform();
            timer -=delay;
        }
    }
    void destroyPlatform()
    {
        myboxCollider.enabled = false;    
        myAnimator.SetBool("isTouched",true);
    }
    void rebuildPlatform()
    {
        myboxCollider.enabled = true;
        myAnimator.SetBool("isTouched",false);
    }
    
}
