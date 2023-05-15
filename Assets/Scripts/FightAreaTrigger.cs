using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAreaTrigger : MonoBehaviour
{
    public bool isInFightArea = false;
     Vector3 fightAreaStartPosition;
     playerMovement player;
     
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        isInFightArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player")
        {
            isInFightArea = true;
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            isInFightArea = false;
        }
    }
}
