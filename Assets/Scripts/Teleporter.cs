using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    playerMovement player;
    public GameObject teleporter2;
    public bool spawnDirection;
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        Vector3 teleporter1Pos = transform.position;
        Vector3 teleporter2Pos = teleporter2.transform.position;
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player")
        {
            if(spawnDirection)
            {
                player.transform.position = teleporter2.transform.position + new Vector3(2,0,0);
            }
            else
            {
                player.transform.position = teleporter2.transform.position + new Vector3(-2,0,0);
            }
            
        }
    }
}
