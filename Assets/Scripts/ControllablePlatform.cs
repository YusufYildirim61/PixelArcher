using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllablePlatform : MonoBehaviour
{
    
    Rigidbody2D rb;
    private int index;
    bool movePlatformLeft,movePlatformRight,movePlatformUp,movePlatformDown;
    playerMovement player;
    [SerializeField] float ControlSpeed = 5f;
    bool isPlayerOnTop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
    }

    
    void Update()
    {
        
        if(isPlayerOnTop)
        { 
            movePlatform();
            
        }
        else
        {
            //player.transform.SetParent(null);
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.collider ==player.myBodyCollider)
        {
            isPlayerOnTop = true;
            other.transform.SetParent(transform);
            if(other.collider == player.myBodyCollider && player.isAlive)
            {
                player.myRigidbody.gravityScale = 0;
                //player.LeftRight.SetActive(false);
                //player.platformController.SetActive(true);
            }
        }
        
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider ==player.myBodyCollider)
        {
            isPlayerOnTop = false;
            movePlatformLeft = false;
            movePlatformRight = false;
            movePlatformUp = false;
            movePlatformDown = false;
            //other.transform.SetParent(null);
            if(other.collider == player.myBodyCollider && player.isAlive)
            {
                player.myRigidbody.gravityScale = player.gravityAtStart;
                //player.platformController.SetActive(false);
                //player.LeftRight.SetActive(true);
                rb.velocity = new Vector2(0,0);
            }
        }
        
    }
    void movePlatform()
    {
        if(movePlatformLeft)
        {
            rb.velocity = new Vector2(-ControlSpeed,0);
        }
        if(movePlatformRight)
        {
            rb.velocity = new Vector2(ControlSpeed,0);
        }
        if(movePlatformUp)
        {
            //player.transform.SetParent(transform);
            player.myRigidbody.velocity = new Vector2(0,0);
            rb.constraints =  RigidbodyConstraints2D.None;
            rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x,ControlSpeed);
        }
        if(movePlatformDown)
        {
            player.transform.SetParent(transform);
            player.myRigidbody.velocity = new Vector2(0,-ControlSpeed);
            rb.constraints =  RigidbodyConstraints2D.None;
            rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x,-ControlSpeed);
        }
    }
    public void MoveLeft()
    {
        movePlatformLeft = true;
    }

    public void MoveRight()
    {
        movePlatformRight = true;    
    }
    public void MoveUp()
    { 
        movePlatformUp = true;
    }
    public void MoveDown()
    {
        movePlatformDown = true; 
    }
    public void stopMovingLeft()
    {
        movePlatformLeft = false;
        rb.velocity = new Vector2(0,0);
    }
    public void stopMovingRight()
    {
        movePlatformRight = false;
        rb.velocity = new Vector2(0,0);
    }
    public void stopMovingUp()
    {
        //player.transform.SetParent(transform);
        player.myRigidbody.velocity = new Vector2(0,0);
        movePlatformUp = false;
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.constraints =  RigidbodyConstraints2D.FreezePositionY;
        rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
        
    }
    public void stopMovingDown()
    {
        //player.transform.SetParent(transform);
        movePlatformDown = false;
        player.myRigidbody.velocity = new Vector2(0,0);
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.constraints =  RigidbodyConstraints2D.FreezePositionY;
        rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.GetComponent<BoxCollider2D>() == player.myFeetCollider)
        {
            player.moveLeft = false;
            player.moveRight = false;
            player.moveUp = false;
            player.moveDown = false;
            player.platformController.SetActive(true);
            player.LeftRight.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<BoxCollider2D>() == player.myFeetCollider)
        {
            player.platformController.SetActive(false);
            player.LeftRight.SetActive(true);
        }
    }
}


