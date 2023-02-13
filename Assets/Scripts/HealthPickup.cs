using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    /*
    bool wasCollected = false;
    [SerializeField] int addedHealth = 1;
    [SerializeField] AudioClip healthPickupSFX; 
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(healthPickupSFX,Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddLife(addedHealth);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    */
}
