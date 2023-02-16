using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    playerMovement playerMovement;
    Animator doorAnimator;
    Rigidbody2D doorRB;
    BoxCollider2D doorCollider;
    void Start()
    {
        doorRB = GetComponent<Rigidbody2D>();
        doorCollider = GetComponent<BoxCollider2D>();
        doorAnimator = GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider==playerMovement.myBodyCollider && playerMovement.hasKey)
        {
            SoundManagerScript.PlaySound("gateOpen");
            doorCollider.enabled = false;
            doorRB.constraints = RigidbodyConstraints2D.FreezeAll;
            doorAnimator.SetTrigger("OpenDoor");
            //Destroy(gameObject);
        }
    }
}
