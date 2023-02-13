using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleArrow : MonoBehaviour
{
    [SerializeField] AudioClip ammoPickUpSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<playerMovement>().isStopped)
        {
            return;
        }
        transform.Rotate(new Vector3(0,2,0));
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" &&  FindObjectOfType<playerMovement>().isAlive==true)
        {
            FindObjectOfType<playerMovement>().tripleArrow = true;
            Destroy(gameObject);
            SoundManagerScript.PlaySound("arrowShot");  
        }
    }
}
