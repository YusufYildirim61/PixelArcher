using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAmmoPack : MonoBehaviour
{
    bool wasCollectedAmmo = false;
    [SerializeField] int addedAmmo = 5;
    
    void Start() 
    {
        FindObjectOfType<GameSession>().strongAmmo =  PlayerPrefs.GetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
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
            FindObjectOfType<GameSession>().AddStrongAmmo(addedAmmo);
            PlayerPrefs.SetInt("strongAmmo",FindObjectOfType<GameSession>().strongAmmo);
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
    }
}
