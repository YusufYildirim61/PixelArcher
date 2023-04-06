using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private CinemachineImpulseSource myImpulseSource;

    [Header("MOVEMENT")]
    [SerializeField] public float runSpeed = 6f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbspeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    

    [Header("GUN")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    [SerializeField] GameObject bullet3;
    [SerializeField] GameObject poisonArrow;
    [SerializeField] GameObject iceArrow;
    [SerializeField] GameObject strongArrow;

    
    [SerializeField] public Transform gun;
    

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    public CapsuleCollider2D myBodyCollider;
    public BoxCollider2D myFeetCollider;
    
    float gravityAtStart;
    public bool isAlive = true;
    public bool isStopped = false;
    GameSession gameSession;
    int totalAmmo;
    public Vector3 respawnPoint;
    public bool isLevelFinished = false;
    private bool moveLeft, moveRight, moveUp, moveDown = false;
    public GameObject levelCompleteScreen;
    private int currentSceneIndex;
    public bool tripleArrow;
    public bool isTouchedHazards = false;
    
    
    [Header("UI")]
    [SerializeField] public GameObject ShootandJump;
    [SerializeField] GameObject climbUpandDown;
    [SerializeField] public GameObject buyButton;

    [SerializeField] GameObject platformTilemap;
    [Header("Materials")]
    [SerializeField] PhysicsMaterial2D frictionless;
    [SerializeField] PhysicsMaterial2D friction;

    

    [Header("SKINS")]
    [SerializeField] private AnimatorOverrideController blondeSkin;
    [SerializeField] private AnimatorOverrideController blackHairSkin;
    [SerializeField] private AnimatorOverrideController greyHairSkin;
    [SerializeField] private AnimatorOverrideController santaSkin;
    [SerializeField] private AnimatorOverrideController hatterSkin;
    [SerializeField] private AnimatorOverrideController pirateSkin;
    [SerializeField] private AnimatorOverrideController robinSkin;
    private RuntimeAnimatorController defaultSkin;
    
    [Header("Audio")]
    AudioSource footsteps;

    
    public bool isInBossLevel = false;
    
    public float bounceSpeed = 6f;
    public  bool hasKey = false;
    public bool isPressedBuy = false;

    void Start()
    {

        gameSession = FindObjectOfType<GameSession>();
        footsteps = GetComponent<AudioSource>();
        tripleArrow = false;
        Application.targetFrameRate = 60;
        totalAmmo = FindObjectOfType<GameSession>().ammo;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = myRigidbody.gravityScale;
        myImpulseSource = GetComponent<CinemachineImpulseSource>();
        respawnPoint = transform.position;
        buyButton.SetActive(false);
        climbUpandDown.SetActive(false);
        FindObjectOfType<GameSession>().poisonAmmo =  PlayerPrefs.GetInt("poisonAmmo",FindObjectOfType<GameSession>().poisonAmmo); // Ammo kaydetme
        FindObjectOfType<GameSession>().iceAmmo =  PlayerPrefs.GetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
        //FindObjectOfType<GameSession>().strongAmmo =  PlayerPrefs.GetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene",currentSceneIndex);

        if(PlayerPrefs.GetInt("SelectedSkin")== 0)
        {
            defaultSkin = myAnimator.runtimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 1)
        {
            myAnimator.runtimeAnimatorController = blondeSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 2)
        {
            myAnimator.runtimeAnimatorController = blackHairSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 3)
        {
            myAnimator.runtimeAnimatorController = greyHairSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 4)
        {
            myAnimator.runtimeAnimatorController = santaSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 5)
        {
            myAnimator.runtimeAnimatorController = hatterSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 6)
        {
            myAnimator.runtimeAnimatorController = pirateSkin as RuntimeAnimatorController;
        }
        else if(PlayerPrefs.GetInt("SelectedSkin")== 7)
        {
            myAnimator.runtimeAnimatorController = robinSkin as RuntimeAnimatorController;
        }
        
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        //playFootsteps();
        moveCharacter();
        moveUpandDown();
        Die();
        //Run();
       //FlipSprite();
       //climbLadder();
        
    }
    
    
    void moveCharacter()
    {
        
        if(moveLeft)
        {
            
            myBodyCollider.sharedMaterial = frictionless;
            myFeetCollider.sharedMaterial = frictionless;
            myRigidbody.velocity = new Vector2(-runSpeed,myRigidbody.velocity.y);
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon;
            transform.localScale = new Vector2(-1,1);
            if(playerHasHorizontalSpeed)
            {
                myAnimator.SetBool("isRunning",true);
                
            }
            else
            {
                myAnimator.SetBool("isRunning",false);
            }
        }
        if(moveRight)
        {
            
            myBodyCollider.sharedMaterial = frictionless;
            myFeetCollider.sharedMaterial = frictionless;
            myRigidbody.velocity = new Vector2(runSpeed,myRigidbody.velocity.y);
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x)> Mathf.Epsilon;
            transform.localScale = new Vector2(1,1);
            if(playerHasHorizontalSpeed)
            {
                myAnimator.SetBool("isRunning",true);
                
            }
            else
            {
                myAnimator.SetBool("isRunning",false);
            }
        }
    }
    
    void moveUpandDown()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            ShootandJump.SetActive(false);
            climbUpandDown.SetActive(true);
            if(moveUp)
            {
            Vector2 climbVelocity = new Vector2(0,climbspeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;
            
            myAnimator.SetBool("isClimbing",true);

                if(climbVelocity.y == 0)
                {

                    myAnimator.speed = 0;
                } 
                else
                {
                    myAnimator.speed = 1;
                }

            }
            else if(moveDown)
            {
                
                Vector2 climbVelocity = new Vector2(0,-climbspeed);
                myRigidbody.velocity = climbVelocity;
                myRigidbody.gravityScale = 0f;
                
                myAnimator.SetBool("isClimbing",true);
                if(climbVelocity.y == 0)
                {

                    myAnimator.speed = 0;
                } 
                else
                {
                    myAnimator.speed = 1;
                }
            }
        }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityAtStart;
            myAnimator.SetBool("isClimbing",false);
            myAnimator.speed = 1;
            moveUp = false;
            moveDown = false;
            ShootandJump.SetActive(true);
            climbUpandDown.SetActive(false);
            
            //return;
             
                 
        }
    }
    
    void levelFinishMovement()
    {
        if(isLevelFinished)
        {
            myRigidbody.velocity = new Vector2(runSpeed,myRigidbody.velocity.y);
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x)> Mathf.Epsilon;
            transform.localScale = new Vector2(1,1);
            if(playerHasHorizontalSpeed)
            {
                myAnimator.SetBool("isRunning",true);
                
            }
            else
            {
                myAnimator.SetBool("isRunning",false);
            }
        }
    }
    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;    
    }
    public void MoveUp()
    { 
        moveUp = true;
    }
    public void MoveDown()
    {
        moveDown = true; 
    }
    
    
    public void StopMovingLeft()
    {
         myBodyCollider.sharedMaterial = friction;
        myFeetCollider.sharedMaterial = friction;
        moveLeft = false;
        moveRight = false;
        myRigidbody.velocity = new Vector2(0,myRigidbody.velocity.y);
        transform.localScale = new Vector2(-1,1);
        
        myAnimator.SetBool("isRunning",false);
   
    }
    public void StopMovingRight()
    {
         myBodyCollider.sharedMaterial = friction;
        myFeetCollider.sharedMaterial = friction;
        moveLeft = false;
        moveRight = false;
        myRigidbody.velocity = new Vector2(0,myRigidbody.velocity.y);
        
        transform.localScale = new Vector2(1,1);
        myAnimator.SetBool("isRunning",false);
 
    }
    public void StopClimbing()
    {
        moveUp = false;
        moveDown = false;
        myRigidbody.velocity = new Vector2(0,0);
        myAnimator.speed = 0;
    }
     
    
    public void fireButton()
    { 
        if(!isAlive || myAnimator.GetBool("isClimbing") || isStopped)
        {
            return;
        }
        if(gameSession.ammo>0 && gameSession.isOnDefaultArrow)
        {   
            if(tripleArrow)
            {
                gameSession.removeAmmo();
                //PlayerPrefs.SetInt("ammo",FindObjectOfType<GameSession>().ammo);
                SoundManagerScript.PlaySound("arrowShot");
                myAnimator.SetTrigger("isShooting");
                Instantiate(bullet, gun.position,transform.rotation);
                Instantiate(bullet2, gun.position,transform.rotation);
                Instantiate(bullet3, gun.position,transform.rotation);
            }
            
            else
            {
                gameSession.removeAmmo();
                //PlayerPrefs.SetInt("ammo",FindObjectOfType<GameSession>().ammo);
                SoundManagerScript.PlaySound("arrowShot");
                myAnimator.SetTrigger("isShooting");
                Instantiate(bullet, gun.position,transform.rotation);
            }
            
        }
        if(gameSession.poisonAmmo>0 && gameSession.isOnPoisonArrow)
        {
                gameSession.removePoisonAmmo();
                PlayerPrefs.SetInt("poisonAmmo",gameSession.poisonAmmo);
                SoundManagerScript.PlaySound("arrowShot");
                myAnimator.SetTrigger("isShooting");
                Instantiate(poisonArrow, gun.position,transform.rotation);
        }
        if(gameSession.iceAmmo>0 && gameSession.isOnIceArrow)
        {
                gameSession.removeIceAmmo();
                PlayerPrefs.SetInt("iceAmmo",gameSession.iceAmmo);
                SoundManagerScript.PlaySound("arrowShot");
                myAnimator.SetTrigger("isShooting");
                Instantiate(iceArrow, gun.position,transform.rotation);
        }
        if(gameSession.strongAmmo>0 && gameSession.isOnStrongArrow)
        {
                gameSession.removeStrongAmmo();
                PlayerPrefs.SetInt("strongAmmo",gameSession.strongAmmo);
                SoundManagerScript.PlaySound("arrowShot");
                myAnimator.SetTrigger("isShooting");
                Instantiate(strongArrow, gun.position,transform.rotation);
        }     
        
            
        
    }
    public void jumpButton()
    {   
            if(!isAlive || isLevelFinished)
            {
                return;
            }

            if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                return; // Yere inmeden zıplamayı engelleme
            }
            SoundManagerScript.PlaySound("jump");
            myRigidbody.velocity += new Vector2(0f,jumpSpeed);        
    }
 
    public void Die() 
    {
       if(!isInBossLevel)
       {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            
            myBodyCollider.enabled = false;
            myFeetCollider.enabled = false;
            FindObjectOfType<LevelComplete>().timesDied(10);
            isAlive = false;
            myAnimator.SetBool("Die",true);
            myRigidbody.velocity = deathKick;
            SoundManagerScript.PlaySound("death");
            myImpulseSource.GenerateImpulse(1);
            Invoke("Respawn",0.5f);

        }
        if(isTouchedHazards)
        {
            myBodyCollider.enabled = false;
            myFeetCollider.enabled = false;
            FindObjectOfType<LevelComplete>().timesDied(10);
            isAlive = false;
            myAnimator.SetBool("Die",true);
            myRigidbody.velocity = deathKick;
            SoundManagerScript.PlaySound("death");
            myImpulseSource.GenerateImpulse(1);
            Invoke("Respawn",0.5f);
            
        }
       }
       if(isInBossLevel)
       {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            myBodyCollider.enabled = false;
            FindObjectOfType<bossLevelManager>().removeHealth(1);
            FindObjectOfType<bossLevelManager>().restartLevel();
            FindObjectOfType<LevelComplete>().timesDied(10);
            SoundManagerScript.PlaySound("death");
            //isTouchedHazards = true;
            myRigidbody.velocity += Vector2.up * bounceSpeed;
            
            Invoke("damagedByHazards",0.2f);
        }
        if(isTouchedHazards)
        {
            isTouchedHazards = false;
            FindObjectOfType<bossLevelManager>().removeHealth(1);
            FindObjectOfType<bossLevelManager>().restartLevel();
            FindObjectOfType<LevelComplete>().timesDied(10);
            SoundManagerScript.PlaySound("death");
            //isTouchedHazards = true;
            myRigidbody.velocity += Vector2.up * bounceSpeed;
            
            
        }
       } 
       
    }
    public void dieAndReload()
    {
        isAlive = false;
        myAnimator.SetBool("Die",true);
        myImpulseSource.GenerateImpulse(1);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isInBossLevel)
        {
            if(collision.collider == FindObjectOfType<Boss>().myCollider )
        { 
            FindObjectOfType<bossLevelManager>().removeHealth(1);
            FindObjectOfType<bossLevelManager>().restartLevel();
            FindObjectOfType<LevelComplete>().timesDied(10);
            SoundManagerScript.PlaySound("death");
            //isTouchedHazards = true;
            myRigidbody.velocity = deathKick;
            
            FindObjectOfType<Boss>().myCollider.enabled = false;
            FindObjectOfType<Boss>().myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            Invoke("StopBounce", 1.5f);
        }
        }
        
    }
    void StopBounce()
    {
        
        //isTouchedHazards = false;
        FindObjectOfType<Boss>().myCollider.enabled =true;
        FindObjectOfType<Boss>().myRigidbody.constraints = RigidbodyConstraints2D.None;
        FindObjectOfType<Boss>().myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Respawn()
    {
        myRigidbody.velocity = new Vector2(0,0); 
        myBodyCollider.enabled = true;
        myFeetCollider.enabled = true;
        transform.position = respawnPoint;
        myAnimator.SetBool("Die",false);
        isAlive = true;
        isTouchedHazards = false;
         
    }
    public void DamagedbyBoss()
    {
        FindObjectOfType<bossLevelManager>().removeHealth(1);
        FindObjectOfType<bossLevelManager>().restartLevel();
        FindObjectOfType<LevelComplete>().timesDied(10);
        SoundManagerScript.PlaySound("death");
        //isTouchedHazards = true;
        myRigidbody.velocity = deathKick;
        
        FindObjectOfType<Boss>().myCollider.enabled = false;
        FindObjectOfType<Boss>().myRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        Invoke("StopBounce", 1.5f);
    
    }
    void damagedByHazards()
    {
        myBodyCollider.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Fireball")
        {
            isTouchedHazards = true;
            Destroy(other.gameObject);
        }
        if(other.tag == "Key")
        {
            SoundManagerScript.PlaySound("confirm");
            hasKey = true;
            Destroy(other.gameObject);  
        }
        if(other.tag == "Shop")
        {
            FindObjectOfType<GameSession>().totalMoney.SetActive(true);
            
        }    
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Shop")
        {
            FindObjectOfType<GameSession>().totalMoney.SetActive(false);
            
        } 
    }
    public void buyAmmoPacks()
    {
        isPressedBuy = true;
    }
    


    void playFootsteps()
    {
         bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

         if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerHasHorizontalSpeed && !footsteps.isPlaying)
        {
            footsteps.Play();
        }
        else if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !playerHasHorizontalSpeed)
        {
            footsteps.Stop();
        }
    }
    /*
    void FlipSprite() // Karakterin bastığın yöne göre dönmesi
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x),1);
        }
    }

    void playFootsteps()
    {
         bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

         if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) 
            && playerHasHorizontalSpeed 
            && !footsteps.isPlaying)
        {
            footsteps.Play();
        }
        else if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) 
            || !playerHasHorizontalSpeed)
        {
            footsteps.Stop();
        }
    }

    
    void OnJump(InputValue value)
    {   
        
        if(!isAlive || isLevelFinished)
        {
            return;
        }
        
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return; // Yere inmeden zıplamayı engelleme
        }
        
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f,jumpSpeed); // Zıplama
            
        }            
        
    }

    void OnMove(InputValue value)
    {
        if(!isAlive || isStopped || isLevelFinished)
        {
            return;
        }
        
        moveInput = value.Get<Vector2>();
        
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x *runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
       
        bool runningOrNot = Mathf.Abs(myRigidbody.velocity.x)> Mathf.Epsilon;
        if(runningOrNot)
        {
            myAnimator.SetBool("isRunning",true);
        }
        else
        {
            myAnimator.SetBool("isRunning",false);
        }
        
    }
    

    void climbLadder()
    {

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
             myRigidbody.gravityScale = gravityAtStart;
             myAnimator.SetBool("isClimbing",false);
             myAnimator.speed = 1;
             return;
             
                 
        }
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y*climbspeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y)>Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",true);

        if(climbVelocity.y == 0)
        {

            myAnimator.speed = 0;
        } 
        else
        {
            myAnimator.speed = 1;
        }
    }
    
    void OnFire(InputValue value)
    {
        
        if(!isAlive || myAnimator.GetBool("isClimbing") || isStopped)
        {
            return;
        }
        if(value.isPressed && FindObjectOfType<GameSession>().ammo>0)
        {   
            
            FindObjectOfType<GameSession>().removeAmmo();
            AudioSource.PlayClipAtPoint(arrowShotSFX,Camera.main.transform.position);
            myAnimator.SetTrigger("isShooting");
            Instantiate(bullet, gun.position,transform.rotation);
        }
    
    }
    */
    
    
}

