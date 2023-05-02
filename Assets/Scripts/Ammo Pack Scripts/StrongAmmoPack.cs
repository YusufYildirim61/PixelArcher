using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAmmoPack : MonoBehaviour
{
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    public int StrongPrice = 10;
    playerMovement playerMovement;
    //adjust this to change speed
    [SerializeField] float speed = 5f;
    //adjust this to change how high it goes
    [SerializeField] float height = 0.5f;
    Vector3 pos;
    
    void Start() 
    {
        pos = transform.position;
        playerMovement = FindObjectOfType<playerMovement>();
        FindObjectOfType<GameSession>().strongAmmo =  PlayerPrefs.GetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
        
    }
    void Update()
    {
        if(playerMovement.isStopped)
        {
            return;
        }
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player" &&  !wasCollectedAmmo && playerMovement.isAlive==true && StrongPrice==0)
        {
            SoundManagerScript.PlaySound("ammoPickUp");
            wasCollectedAmmo = true;
            FindObjectOfType<GameSession>().AddStrongAmmo(addedAmmo);
            //PlayerPrefs.SetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag=="Player" &&  !wasCollectedAmmo && playerMovement.isAlive==true)
        {
            playerMovement.buyButton.SetActive(true);
            playerMovement.ShootandJump.SetActive(false);
            buyAndAdd();
            
            
            
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player" &&  !wasCollectedAmmo && playerMovement.isAlive==true)
        {
            playerMovement.buyButton.SetActive(false);
            playerMovement.ShootandJump.SetActive(true);
        }
    }
    void buyAndAdd()
    {
        if(FindObjectOfType<GameManager>().money>=StrongPrice && playerMovement.isPressedBuy)
            {
                
                SoundManagerScript.PlaySound("confirm");
                wasCollectedAmmo = true;
                FindObjectOfType<GameSession>().AddStrongAmmo(addedAmmo);
                FindObjectOfType<GameManager>().money -= StrongPrice;
                PlayerPrefs.SetInt("TotalMoney",FindObjectOfType<GameManager>().money);
                PlayerPrefs.SetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
                gameObject.SetActive(false);
                Destroy(gameObject);
                playerMovement.isPressedBuy = false;
                playerMovement.buyButton.SetActive(false);
                playerMovement.ShootandJump.SetActive(true);

            }
    }
}
