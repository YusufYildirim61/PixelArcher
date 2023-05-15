using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpike : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3[] positions;
    private int index;
    playerMovement player;
    private Camera mainCamera;
    bool isInCameraRange = false;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        player = FindObjectOfType<playerMovement>();
    }

    
    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            isInCameraRange = true;
        }
        else
        {
            isInCameraRange = false;
        }
        playSpikeSFX();
        if(!isInCameraRange || player.isStopped)
        {
            audioSource.Stop();
        }
        

        transform.position = Vector2.MoveTowards(transform.position,positions[index],Time.deltaTime*speed);
        if(transform.position == positions[index])
        {
            if(index == positions.Length-1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
        
    }
     void playSpikeSFX()
    {
        if(isInCameraRange && !audioSource.isPlaying && PlayerPrefs.GetInt("isAudioMuted")==0)
        {
            audioSource.Play();
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {   
        if(other.collider ==player.myBodyCollider)
        {
            other.transform.SetParent(transform);
        }
        
    }
    void OnCollisionExit2D(Collision2D other) 
    {
        if(other.collider ==player.myBodyCollider)
        {
            other.transform.SetParent(null);
        }
    }
}
