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
    bool isPlayerOnTop;
    GameSession gameSession;
    public PolygonCollider2D myCollider;
    bool isPressedGetIn = false;
    
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.getInButton.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<PolygonCollider2D>();
        player = FindObjectOfType<playerMovement>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
    }

    void Update()
    {
        Invoke("respawnPlatform",0.4f);
        
        if(isPlayerOnTop)
        {
            movePlatform();
            if(player.myRigidbody.velocity == new Vector2(0,0) && !isPressedGetIn)
            {
                gameSession.getInButton.SetActive(true);
            }
            else
            {
                gameSession.getInButton.SetActive(false);
            }
        }
        
        
            
    }
     public void pressGetIn()
    {
        if(isPlayerOnTop && player.myRigidbody.velocity == new Vector2(0,0))
        {
            gameSession.getInButton.SetActive(false);
            isPressedGetIn = true;
            player.transform.position = transform.position + new Vector3(0.16f,0.8f,0);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            player.transform.SetParent(transform);
            player.platformController.SetActive(true);
            player.LeftRight.SetActive(false);

        }
        else
        {
            
        }
        
    }
    
    
    
    void OnTriggerEnter2D(Collider2D other) 
    {
         if(other.GetComponent<BoxCollider2D>() == player.myFeetCollider)
         {
            
            isPlayerOnTop = true;
            
             //player.myRigidbody.bodyType = RigidbodyType2D.Kinematic;
             
             
             //other.transform.SetParent(transform);
             
             //rb.constraints = RigidbodyConstraints2D.None;
             //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
             //player.platformController.SetActive(true);
             //player.LeftRight.SetActive(false);
             //rb.constraints = RigidbodyConstraints2D.None;
             //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
         }
        
        
    }
    
    void OnTriggerExit2D(Collider2D other) 
    {
         if(other.GetComponent<BoxCollider2D>() == player.myFeetCollider && isPlayerOnTop)
         {
             //other.transform.SetParent(null);
             //player.myRigidbody.bodyType = RigidbodyType2D.Dynamic;
             isPressedGetIn = false;
             isPlayerOnTop = false;
             gameSession.getInButton.SetActive(false);
             player.transform.SetParent(null);
             player.platformController.SetActive(false); 
             player.LeftRight.SetActive(true);
             rb.constraints = RigidbodyConstraints2D.FreezeAll;
             
             movePlatformLeft = false;
             movePlatformRight = false;
             movePlatformUp = false;
             movePlatformDown = false;
             
             //rb.constraints = RigidbodyConstraints2D.FreezeAll;
             //player.platformController.SetActive(false);
             //player.LeftRight.SetActive(true);
             //rb.constraints = RigidbodyConstraints2D.FreezeAll;
             //player.myRigidbody.bodyType = RigidbodyType2D.Dynamic;
         }
        
    }
    void movePlatform()
    {
        if(movePlatformLeft)
        {
            rb.velocity = new Vector2(-ControlSpeed,0);
            //player.myRigidbody.velocity = new Vector2(0,0);
        }
        if(movePlatformRight)
        {
            rb.velocity = new Vector2(ControlSpeed,0);
            //player.myRigidbody.velocity = new Vector2(0,0);
        }
        if(movePlatformUp)
        {
            
            rb.constraints =  RigidbodyConstraints2D.None;
            rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(rb.velocity.x,ControlSpeed);
        }
        if(movePlatformDown)
        {
            
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
        player.myRigidbody.velocity = new Vector2(0,-ControlSpeed);
        movePlatformUp = false;
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.constraints =  RigidbodyConstraints2D.FreezePositionY;
        rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
        
    }
    public void stopMovingDown()
    {
        //player.transform.SetParent(transform);
        movePlatformDown = false;
        
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.constraints =  RigidbodyConstraints2D.FreezePositionY;
        rb.constraints =  RigidbodyConstraints2D.FreezeRotation;
    }
    
    void respawnPlatform()
    {
        if(player.isAlive == false)
        {
            if(!player.isInEscapeLevel)
            {
                player.transform.SetParent(null);
                transform.position = player.respawnPoint + new Vector3(2,-0.2f,0);
            }
            
        }
    }
}


