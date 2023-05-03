using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiantBeetle : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] TextMeshProUGUI runText;
    Rigidbody2D myRigidbody;
    playerMovement player;
    
    
    void Start()
    {
        runText.text = "RUN!!";
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed,0);
       
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player")
        {
            player.isAlive = false;
        }
        if(other.tag=="Destroy")
        {
            Destroy(gameObject);
            runText.text = "";
        }
    }
}
