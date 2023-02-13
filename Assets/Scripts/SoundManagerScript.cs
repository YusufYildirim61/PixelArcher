using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip cursorSFX, errorSFX, confirmSFX, ammoPickUpSFX, beetleDeathSFX, checkpointSFX, coinPickupSFX, enemyHitSFX,enemyDeathSFX,
    deathSFX,arrowShotSFX,jumpSFX,bossDamagedSFX,bossDeathSFX;
    static AudioSource audioSrc;
    void Start()
    {
        cursorSFX = Resources.Load<AudioClip>("cursor_style_2");
        errorSFX = Resources.Load<AudioClip>("error_style_2_001");
        confirmSFX = Resources.Load<AudioClip>("confirm_style_2_001");
        ammoPickUpSFX = Resources.Load<AudioClip>("AmmoPickUpSFX");
        beetleDeathSFX = Resources.Load<AudioClip>("beetleDeath");
        checkpointSFX = Resources.Load<AudioClip>("torchSFX");
        coinPickupSFX = Resources.Load<AudioClip>("Bonus");
        enemyHitSFX = Resources.Load<AudioClip>("enemyDamaged");
        enemyDeathSFX = Resources.Load<AudioClip>("mutantdie");
        deathSFX = Resources.Load<AudioClip>("deathSFX");
        arrowShotSFX = Resources.Load<AudioClip>("shoot");
        jumpSFX = Resources.Load<AudioClip>("Jump_9_Sound_effects_Pack_2");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        if(FindObjectOfType<AudioManager>().audioMuted==0)
        {
            switch(clip)
        {
            case "cursor":
                audioSrc.PlayOneShot(cursorSFX);
                break;
            case "error":
                audioSrc.PlayOneShot(errorSFX);
                break;
            case "confirm":
                audioSrc.PlayOneShot(confirmSFX);
                break;
            case "ammoPickUp":
                audioSrc.PlayOneShot(ammoPickUpSFX);
                break;
            case "beetleDeath":
                audioSrc.PlayOneShot(beetleDeathSFX);
                break;
            case "checkpoint":
                audioSrc.PlayOneShot(checkpointSFX);
                break;
            case "coinPickUp":
                audioSrc.PlayOneShot(coinPickupSFX);
                break;
            case "enemyHit":
                audioSrc.PlayOneShot(enemyHitSFX);
                break;
            case "enemyDeath":
                audioSrc.PlayOneShot(enemyDeathSFX);
                break;
            case "death":
                audioSrc.PlayOneShot(deathSFX);
                break;
            case "arrowShot":
                audioSrc.PlayOneShot(arrowShotSFX);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSFX);
                break;                   
        }
        }
        
    }
}
