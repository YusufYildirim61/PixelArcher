using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    playerMovement playerMovement;
    ControllablePlatform controllablePlatform;
     Animator doorAnimator;
     Rigidbody2D doorRB;
     BoxCollider2D doorCollider;
    public GameObject key;
    
    void Start()
    {
        controllablePlatform = FindObjectOfType<ControllablePlatform>();
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
        if((other.collider==playerMovement.myBodyCollider || other.collider==controllablePlatform.myCollider) && playerMovement.hasKey)
        {
            SoundManagerScript.PlaySound("gateOpen");
            doorCollider.enabled = false;
            doorRB.constraints = RigidbodyConstraints2D.FreezeAll;
            doorAnimator.SetTrigger("OpenDoor");
            playerMovement.hasKey = false;
            //Destroy(gameObject);
        }
        
    }
    public void openGates()
    {
        SoundManagerScript.PlaySound("confirm");
        doorCollider.enabled = false;
        doorRB.constraints = RigidbodyConstraints2D.FreezeAll;
        doorAnimator.SetTrigger("OpenDoor");
    }
    
}
