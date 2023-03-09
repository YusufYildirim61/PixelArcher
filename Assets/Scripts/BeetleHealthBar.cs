using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    BeetleMovement beetleMovement;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        beetleMovement = transform.parent.GetComponent<BeetleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = beetleMovement.beetleHealth * 0.4f;
        transform.localScale = healthBar;
        
    }
}
