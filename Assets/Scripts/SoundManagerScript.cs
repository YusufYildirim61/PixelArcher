using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip cursorSFX, errorSFX, confirmSFX, ammoPickUpSFX, beetleDeathSFX, checkpointSFX, coinPickupSFX, enemyHitSFX,enemyDeathSFX,
    deathSFX,arrowShotSFX,jumpSFX,bossDamagedSFX,bossDeathSFX,gateSFX,footstepsSFX,levelCompleteSFX,poisonChangeSFX,iceChangeSFX,strongChangeSFX,defaultChangeSFX,
    poisonShotSFX,iceShotSFX,strongShotSFX,buyAmmoSFX,fireTrapSFX,windSFX,idleGooAttackSFX,gooDeathSFX,gooAttackSFX,WSAttackSFX,WSDeathSFX,bossAttackSFX,
    fallingPlatformSFX,teleporterSFX,fireBossAttackSFX,fireBossDeathSFX,iceBossAttackSFX,iceBossDeathSFX,arrowBreakSFX,iceImpactSFX,poisonImpactSFX;
    public static AudioSource audioSrc;
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
        bossDamagedSFX = Resources.Load<AudioClip>("hit35");
        bossDeathSFX = Resources.Load<AudioClip>("bossDeath");
        gateSFX = Resources.Load<AudioClip>("gateOpen");
        footstepsSFX = Resources.Load<AudioClip>("sfx_step_grass_r");
        levelCompleteSFX = Resources.Load<AudioClip>("levelComplete");
        poisonChangeSFX = Resources.Load<AudioClip>("poisonArrowChange");
        iceChangeSFX = Resources.Load<AudioClip>("iceArrowChange");
        strongChangeSFX = Resources.Load<AudioClip>("strongArrowChange");
        defaultChangeSFX = Resources.Load<AudioClip>("defaultArrowChange");
        poisonShotSFX = Resources.Load<AudioClip>("poisonArrowShot");
        iceShotSFX = Resources.Load<AudioClip>("iceArrowShot");
        strongShotSFX = Resources.Load<AudioClip>("strongArrowShot");
        buyAmmoSFX = Resources.Load<AudioClip>("Buy");
        fireTrapSFX = Resources.Load<AudioClip>("fireTrap");
        windSFX = Resources.Load<AudioClip>("wind1");
        idleGooAttackSFX = Resources.Load<AudioClip>("idleGooAttack");
        gooDeathSFX = Resources.Load<AudioClip>("gooDeath");
        gooAttackSFX = Resources.Load<AudioClip>("gooAttack");
        WSAttackSFX = Resources.Load<AudioClip>("whiteSkeletonAttack");
        WSDeathSFX = Resources.Load<AudioClip>("whiteSkeletonDeath");
        bossAttackSFX = Resources.Load<AudioClip>("bossAttack");
        fallingPlatformSFX = Resources.Load<AudioClip>("fallingPlatform");
        teleporterSFX = Resources.Load<AudioClip>("Teleport");
        fireBossAttackSFX = Resources.Load<AudioClip>("fireBossAttack");
        fireBossDeathSFX = Resources.Load<AudioClip>("fireBossDeath");
        iceBossAttackSFX = Resources.Load<AudioClip>("iceBossAttack");
        iceBossDeathSFX = Resources.Load<AudioClip>("iceBossDeath");
        arrowBreakSFX = Resources.Load<AudioClip>("arrowBreak1");
        iceImpactSFX = Resources.Load<AudioClip>("iceImpact");
        poisonImpactSFX = Resources.Load<AudioClip>("poisonImpact");
        
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
            case "bossHit":
                audioSrc.PlayOneShot(bossDamagedSFX);
                break;
            case "bossDeath":
                audioSrc.PlayOneShot(bossDeathSFX);
                break;
            case "gateOpen":
                audioSrc.PlayOneShot(gateSFX);
                break;
            case "footsteps":
                audioSrc.PlayOneShot(footstepsSFX);
                break;
            case "levelComplete":
                audioSrc.PlayOneShot(levelCompleteSFX);
                break;
            case "poisonChange":
                audioSrc.PlayOneShot(poisonChangeSFX);
                break;
            case "iceChange":
                audioSrc.PlayOneShot(iceChangeSFX);
                break;
            case "strongChange":
                audioSrc.PlayOneShot(strongChangeSFX);
                break;
            case "defaultChange":
                audioSrc.PlayOneShot(defaultChangeSFX);
                break;
            case "poisonShot":
                audioSrc.PlayOneShot(poisonShotSFX);
                break;
            case "iceShot":
                audioSrc.PlayOneShot(iceShotSFX);
                break;
            case "strongShot":
                audioSrc.PlayOneShot(strongShotSFX);
                break;
            case "buyAmmo":
                audioSrc.PlayOneShot(buyAmmoSFX);
                break;
            case "fireTrap":
                audioSrc.PlayOneShot(fireTrapSFX);
                break; 
            case "wind":
                audioSrc.PlayOneShot(windSFX);
                break;
            case "idleGooAttack":
                audioSrc.PlayOneShot(idleGooAttackSFX);
                break;
            case "gooDeath":
                audioSrc.PlayOneShot(gooDeathSFX);
                break;
            case "gooAttack":
                audioSrc.PlayOneShot(gooAttackSFX);
                break;
            case "WSAttack":
                audioSrc.PlayOneShot(WSAttackSFX);
                break;
            case "WSDeath":
                audioSrc.PlayOneShot(WSDeathSFX);
                break;
            case "bossAttack":
                audioSrc.PlayOneShot(bossAttackSFX);
                break;
            case "fallingPlatform":
                audioSrc.PlayOneShot(fallingPlatformSFX);
                break;
            case "teleporter":
                audioSrc.PlayOneShot(teleporterSFX);
                break;
            case "fireBossAttack":
                audioSrc.PlayOneShot(fireBossAttackSFX);
                break;
            case "fireBossDeath":
                audioSrc.PlayOneShot(fireBossDeathSFX);
                break;
            case "iceBossAttack":
                audioSrc.PlayOneShot(iceBossAttackSFX);
                break;
            case "iceBossDeath":
                audioSrc.PlayOneShot(iceBossDeathSFX);
                break;
            case "arrowBreak":
                audioSrc.PlayOneShot(arrowBreakSFX);
                break;
            case "iceImpact":
                audioSrc.PlayOneShot(iceImpactSFX);
                break;
            case "poisonImpact":
                audioSrc.PlayOneShot(poisonImpactSFX);
                break;                        
        }
        }
        
    }
}
