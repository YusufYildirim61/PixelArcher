using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonAmmoPack : MonoBehaviour
{
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    
    void Start() 
    {
        FindObjectOfType<GameSession>().poisonAmmo =  PlayerPrefs.GetInt("poisonAmmo",FindObjectOfType<GameSession>().poisonAmmo);
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
            FindObjectOfType<GameSession>().AddPoisonAmmo(addedAmmo);
            PlayerPrefs.SetInt("poisonAmmo",FindObjectOfType<GameSession>().poisonAmmo);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
}
