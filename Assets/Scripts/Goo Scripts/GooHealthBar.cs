using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    Goo goo;
    void Start()
    {
        healthBar = transform.localScale;
        goo = transform.parent.GetComponent<Goo>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = goo.health * 0.8f;
        transform.localScale = healthBar;
    }
}
