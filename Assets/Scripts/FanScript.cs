using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    public float fanSpeed = 30f;
    [SerializeField] bool isTurnedRight;
    [SerializeField] bool isTurnedLeft;
    [SerializeField] bool isTurnedUp;
    [SerializeField] bool isTurnedDown;
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
     if(other.tag=="Player" && isTurnedUp)
     {
        other.attachedRigidbody.AddForce(Vector2.up*fanSpeed*Time.deltaTime);
     }
     if(other.tag=="Player" && isTurnedRight)
     {
        other.attachedRigidbody.AddForce(Vector2.right*fanSpeed*Time.deltaTime);
     }
     if(other.tag=="Player" && isTurnedLeft)
     {
        other.attachedRigidbody.AddForce(Vector2.left*fanSpeed*Time.deltaTime);
     }
     if(other.tag=="Player" && isTurnedDown)
     {
        other.attachedRigidbody.AddForce(Vector2.down*fanSpeed*Time.deltaTime);
     }
     
     
 
 }
}
