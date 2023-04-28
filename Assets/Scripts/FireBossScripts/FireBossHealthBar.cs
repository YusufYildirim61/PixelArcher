using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    FireBoss fireBoss;
    public float barSize = 0.04f;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        fireBoss = FindObjectOfType<FireBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = fireBoss.fireBossHealth * barSize;
        transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            transform.localScale = healthBar;
        }
    }
}
