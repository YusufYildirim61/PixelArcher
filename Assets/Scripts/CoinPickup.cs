using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    
    [SerializeField] int pointsForCoinPickup = 100;
    [SerializeField] int totalMoneyIncreaseValue;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" && !wasCollected && FindObjectOfType<playerMovement>().isAlive==true)
        {
            FindObjectOfType<GameManager>().money += totalMoneyIncreaseValue;
            PlayerPrefs.SetInt("TotalMoney",FindObjectOfType<GameManager>().money); 
            wasCollected = true;
            //FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            FindObjectOfType<LevelComplete>().AddCoinToScore(pointsForCoinPickup);
            SoundManagerScript.PlaySound("coinPickUp");
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
