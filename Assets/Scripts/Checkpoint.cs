using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    playerMovement playerMovement;
    Animator checkpointAnimator;
    bool isTocuhed = false;

    void Start() 
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        checkpointAnimator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !isTocuhed && FindObjectOfType<playerMovement>().isAlive==true)
        {
            SoundManagerScript.PlaySound("checkpoint");
            checkpointAnimator.SetBool("isTouchedTorch",true);
            playerMovement.respawnPoint = transform.position;
            
            isTocuhed = true;
        }
    }
    
}

