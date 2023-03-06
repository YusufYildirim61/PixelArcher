using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAmmoPack : MonoBehaviour
{
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    
    void Start() 
    {
        FindObjectOfType<GameSession>().iceAmmo =  PlayerPrefs.GetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
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
            FindObjectOfType<GameSession>().AddIceAmmo(addedAmmo);
            PlayerPrefs.SetInt("iceAmmo",FindObjectOfType<GameSession>().iceAmmo);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
}
