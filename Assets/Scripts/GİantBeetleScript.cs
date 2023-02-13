using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GİantBeetleScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    [SerializeField] public int lives;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed,0);
       
    }

    

}
