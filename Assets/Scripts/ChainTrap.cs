using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTrap : MonoBehaviour
{
    public float speed = 2f;
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
        transform.Rotate(new Vector3(0,0,speed));
    }
}
