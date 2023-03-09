using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSkeletonHealthBar : MonoBehaviour
{
    Vector2 healthBar;
    WhiteSkeleton whiteSkeleton;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.localScale;
        whiteSkeleton = transform.parent.GetComponent<WhiteSkeleton>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.x = whiteSkeleton.health * 0.8f;
        transform.localScale = healthBar;
        if(healthBar.x<=0)
        {
            healthBar.x = 0;
            transform.localScale = healthBar;
        }
    }
}
