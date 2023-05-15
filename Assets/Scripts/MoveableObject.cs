using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    AudioSource pushSFX;
    Rigidbody2D objectRigidbody;
    BoxCollider2D objectCollider;
    playerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        pushSFX = GetComponent<AudioSource>();
        objectRigidbody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playPushSFX();
        if(objectRigidbody.velocity == new Vector2(0,0) || player.isStopped)
        {
            pushSFX.Stop();
        }
    }
    void playPushSFX()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(objectRigidbody.velocity.x) > Mathf.Epsilon;
        if (objectCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && playerHasHorizontalSpeed && !pushSFX.isPlaying && PlayerPrefs.GetInt("isAudioMuted")==0)
        {
            pushSFX.Play();
        }
    }
}
