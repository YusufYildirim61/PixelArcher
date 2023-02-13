using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
  
    public AudioSource levelMusic;

    int musicMuted;
    public int audioMuted;


    public Button greenMusicButton;
    public GameObject redMusicButton;
    public Button greenAudioButton;
    public GameObject redAudioButton;


    void Start() 
    {
        redMusicButton.SetActive(false);
        
    }
    void Update() 
    {
        if(musicMuted==0)
        {
            levelMusic.mute = false;
            redMusicButton.SetActive(false);
            greenMusicButton.interactable = true;
        }
        if(musicMuted==1)
        {
            levelMusic.mute = true;
            redMusicButton.SetActive(true);
            greenMusicButton.interactable = false;
        }
        if(audioMuted==0)
        {
            
            redAudioButton.SetActive(false);
            greenAudioButton.interactable = true;
        }
        if(audioMuted==1)
        {
            
            redAudioButton.SetActive(true);
            greenAudioButton.interactable = false;
        }

        musicMuted = PlayerPrefs.GetInt("isMusicMuted");
        audioMuted = PlayerPrefs.GetInt("isAudioMuted");
    }

    public void muteMusic()
    {
        PlayerPrefs.SetInt("isMusicMuted",1);
    }
    public void muteAudio()
    {
        PlayerPrefs.SetInt("isAudioMuted",1);
    }
    public void unMuteMusic()
    {
        PlayerPrefs.SetInt("isMusicMuted",0);

    }
    public void unMuteAudio()
    {
        PlayerPrefs.SetInt("isAudioMuted",0);
    }
}
