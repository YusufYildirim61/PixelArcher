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
    private Camera mainCamera;
    bool isInCameraRange = false;
    public bool hasAudio = true;
    void Start()
    {
        mainCamera = Camera.main;
        timeBetween = startTimeBetween;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height && hasAudio)
        {
            isInCameraRange = true;
        }
        else
        {
            isInCameraRange = false;
        }
        if(timeBetween<=0)
        {
            Instantiate(fireBall,barrel.position,barrel.rotation);
            if(isInCameraRange)
            {
                SoundManagerScript.PlaySound("fireTrap");
            }
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
