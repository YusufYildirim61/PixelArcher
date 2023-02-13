using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    [SerializeField] AudioClip ammoPickUpSFX;
    void Start() 
    {
        //FindObjectOfType<GameSession>().ammo =  PlayerPrefs.GetInt("ammo",FindObjectOfType<GameSession>().ammo);
    }
    void Update()
    {
        if(FindObjectOfType<playerMovement>().isStopped)
        {
            return;
        }
        transform.Rotate(Vector2.left *2);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" &&  !wasCollectedAmmo && FindObjectOfType<playerMovement>().isAlive==true)
        {
            SoundManagerScript.PlaySound("ammoPickUp");
            wasCollectedAmmo = true;
            FindObjectOfType<GameSession>().AddAmmo(addedAmmo);
            //PlayerPrefs.SetInt("ammo",FindObjectOfType<GameSession>().ammo);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
    
}
