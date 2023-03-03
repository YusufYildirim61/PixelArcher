using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    public float fanSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
 {
     if(other.tag=="Player")
     {
        other.attachedRigidbody.AddForce(Vector2.up*fanSpeed*Time.deltaTime);
     }
     
 
 }
}
