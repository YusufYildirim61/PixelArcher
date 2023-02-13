using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    Vector2 healthBar;

    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = boss.bossHealth * 0.04f;
        transform.localScale = healthBar;
    }
}
