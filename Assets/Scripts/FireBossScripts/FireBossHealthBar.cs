using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireBossHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    FireBoss fireBoss;
    IceBoss iceBoss;
    public float barSize = 0.04f;
    FightAreaTrigger fightAreaTrigger;
    [SerializeField] GameObject fireBossBarHealth;
    [SerializeField] GameObject fireBossBar;
    [SerializeField] TextMeshProUGUI bossText;
    playerMovement player;
    DoorScript door;
    
    // Start is called before the first frame update
    void Start()
    {
        bossText.text = "";
        player = FindObjectOfType<playerMovement>();
        door = FindObjectOfType<DoorScript>();
        iceBoss = FindObjectOfType<IceBoss>();
        fireBossBar.SetActive(false);
        fightAreaTrigger = FindObjectOfType<FightAreaTrigger>();
        healthBar = fireBossBarHealth.transform.localScale;
        fireBoss = FindObjectOfType<FireBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(fightAreaTrigger.isInFightArea)
        {
            fireBossBar.SetActive(true);
            if(fireBoss.fireBossHealth>0 || iceBoss.iceBossHealth>0)
            {
                bossText.text = "Kill Fire and Ice Elementals!";
                bossText.color = Color.red;
            }
            else
            {
                bossText.text = "Gate is Now Open!";
                bossText.color = Color.green;
                door.openGates();
                
            }
        }
        else
        {
            fireBossBar.SetActive(false);
            bossText.text="";
        }
        healthBar.x = fireBoss.fireBossHealth * barSize;
        fireBossBarHealth.transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            fireBossBarHealth.transform.localScale = healthBar;
        }
    }
}
