using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAmmoPack : MonoBehaviour
{
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    public int icePrice = 10;
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
        FindObjectOfType<GameSession>().iceAmmo =  PlayerPrefs.GetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
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
        if(other.tag=="Player" &&  !wasCollectedAmmo && playerMovement.isAlive==true && icePrice==0)
        {
            SoundManagerScript.PlaySound("ammoPickUp");
            wasCollectedAmmo = true;
            FindObjectOfType<GameSession>().AddIceAmmo(addedAmmo);
            //PlayerPrefs.SetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
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
            playerMovement.isPressedBuy = false;
        }
    }
    void buyAndAdd()
    {
        if(FindObjectOfType<GameManager>().money>=icePrice && playerMovement.isPressedBuy)
            {
                
                SoundManagerScript.PlaySound("confirm");
                wasCollectedAmmo = true;
                FindObjectOfType<GameSession>().AddIceAmmo(addedAmmo);
                FindObjectOfType<GameManager>().money -= icePrice;
                PlayerPrefs.SetInt("TotalMoney",FindObjectOfType<GameManager>().money);
                PlayerPrefs.SetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
                gameObject.SetActive(false);
                Destroy(gameObject);
                playerMovement.isPressedBuy = false;
                playerMovement.buyButton.SetActive(false);
                playerMovement.ShootandJump.SetActive(true);

            }
    }
}
