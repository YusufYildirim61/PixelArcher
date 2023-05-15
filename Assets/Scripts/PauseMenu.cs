using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool GameIsPaused = false;
    
    public GameObject gameSession;
    
    playerMovement playerMovement;
    AudioManager audioManager;
    int musicMuted;
    int audioMuted;
    void Start() 
    {
        
    }
    void Update()
    {
        audioManager = FindObjectOfType<AudioManager>();
        musicMuted = PlayerPrefs.GetInt("isMusicMuted");
        audioMuted = PlayerPrefs.GetInt("isAudioMuted");
        //PauseGame();
    }
    
    void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            
        {
            if(GameIsPaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
                
            }
        }
    }
    
    public void Resume()
    {
        SoundManagerScript.PlaySound("cursor");
        pauseMenu.SetActive(false);
        gameSession.SetActive(true);
        Time.timeScale=1;
        GameIsPaused = false;
        FindObjectOfType<playerMovement>().isStopped = false;
        if(PlayerPrefs.GetInt("isMusicMuted")==0)
        {
            audioManager.levelMusic.Play();
        }
        
    }
    public void Pause()
    {
        SoundManagerScript.PlaySound("cursor");
        pauseMenu.SetActive(true);
        gameSession.SetActive(false);
        Time.timeScale=0;
        GameIsPaused = true;
        FindObjectOfType<playerMovement>().isStopped = true;
        audioManager.levelMusic.Pause();
    }

    public void restartLevel()
    {
        SoundManagerScript.PlaySound("cursor");
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1;
        
    }
    
    public void quitToMenu()
    {   
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(0);
    }

/*
    public void pauseGame()
    {
        FindObjectOfType<playerMovement>().isStopped = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        
        //gameSession.SetActive(false);
        
        
    }

    public void continueGame()
    {
        FindObjectOfType<playerMovement>().isStopped = false;
        pauseMenu.SetActive(false);
        
        //FindObjectOfType<GameSession>().startGame();
        Time.timeScale = 1;
    }
    public void exitToMenu()
    {
        SceneManager.LoadScene(0);
        //gameSession.SetActive(false);
    }

    */
}
