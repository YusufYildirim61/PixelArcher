using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiantBeetle : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] TextMeshProUGUI runText;
    Rigidbody2D myRigidbody;
    playerMovement player;
    AudioSource audioSource;
    
    
    void Start()
    {
        runText.text = "RUN!!";
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<playerMovement>();
        audioSource = GetComponent<AudioSource>();
        if( PlayerPrefs.GetInt("isAudioMuted")==0 && !player.isStopped)
        {
            audioSource.Play();
        }
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed,0);
        if(player.isStopped)
        {
            audioSource.Stop();
        }
        if(!player.isStopped)
        {
             playGiantSFX();
        }
       

    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player")
        {
            player.isAlive = false;
        }
        if(other.tag=="Destroy")
        {
            Destroy(gameObject);
            runText.text = "";
        }
    }
    void playGiantSFX()
    {
        if (!audioSource.isPlaying && PlayerPrefs.GetInt("isAudioMuted")==0)
        {
            audioSource.PlayDelayed(4f);
        }
    }

}
