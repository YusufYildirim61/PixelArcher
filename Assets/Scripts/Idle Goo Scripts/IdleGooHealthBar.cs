using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGooHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    IdleGoo idleGoo;
    void Start()
    {
        healthBar = transform.localScale;
        idleGoo = transform.parent.GetComponent<IdleGoo>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = idleGoo.health * 0.8f;
        transform.localScale = healthBar;
    }
}
