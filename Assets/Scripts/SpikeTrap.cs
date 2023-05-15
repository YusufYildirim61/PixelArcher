using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    AudioSource spikeSFX;
    private Camera mainCamera;
    bool isInCameraRange = false;
    public bool hasAudio = true;
    playerMovement player;
   
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        mainCamera = Camera.main;
        spikeSFX = GetComponent<AudioSource>();
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
       
        if(!isInCameraRange || player.isStopped)
        {
            spikeSFX.Stop();
        }
        
    }
    void playSpikeSFX()
    {
        if(isInCameraRange && !spikeSFX.isPlaying && PlayerPrefs.GetInt("isAudioMuted")==0)
        {
            spikeSFX.Play();
        }
    }
    
}
