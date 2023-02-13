using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTrap : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] public Transform barrel;
    float timeBetween;
    public float startTimeBetween;
    
    float xSpeed;
    
    void Start()
    {
        
        timeBetween = startTimeBetween;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(timeBetween<=0)
        {
            Instantiate(fireBall,barrel.position,barrel.rotation);
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
        
        
        
    }
    IEnumerator fireCannon()
    {
        Instantiate(fireBall, barrel.position,transform.rotation);
        yield return new WaitForSeconds(2);
    }
    
}
