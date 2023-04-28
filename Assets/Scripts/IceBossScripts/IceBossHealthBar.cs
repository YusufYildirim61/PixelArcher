using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    IceBoss iceBoss;
    public float barSize = 0.04f;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        iceBoss = FindObjectOfType<IceBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = iceBoss.iceBossHealth * barSize;
        transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            transform.localScale = healthBar;
        }
    }
}
