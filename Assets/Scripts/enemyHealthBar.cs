using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    
    EnemyMovement enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = enemyMovement.enemyHealth * 0.4f;
        transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            transform.localScale = healthBar;
        }
    }
}
