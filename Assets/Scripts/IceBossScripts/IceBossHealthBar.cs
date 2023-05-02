using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IceBossHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    IceBoss iceBoss;
    public float barSize = 0.04f;
    FightAreaTrigger fightAreaTrigger;
    [SerializeField] GameObject iceBossBarHealth;
    [SerializeField] GameObject iceBossBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
        fightAreaTrigger = FindObjectOfType<FightAreaTrigger>();
        iceBossBar.SetActive(false);
        healthBar = iceBossBarHealth.transform.localScale;
        iceBoss = FindObjectOfType<IceBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fightAreaTrigger.isInFightArea)
        {
            iceBossBar.SetActive(true);
        }
        else
        {
            iceBossBar.SetActive(false);
        }
        healthBar.x = iceBoss.iceBossHealth * barSize;
        iceBossBarHealth.transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            iceBossBarHealth.transform.localScale = healthBar;
        }
    }
}
