using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;



public class GameSession : MonoBehaviour
{
    
    //[SerializeField] int score;
    [SerializeField] public int ammo = 5;

    [SerializeField] GameObject pauseButton;
    //[SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI ammoText;
    
    playerMovement playerMovement;
    
    
    
    void Awake() 
    {
        
        /*
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        */
        
    }
    

    void Start()
    {
        //scoreText.text = score.ToString();
        ammoText.text = ammo.ToString();
        
    }

    public void ProcessPlayerDeath()
    {
        
        Invoke(nameof(TakeLife),1);
        /*
        if(playerLives>1)
        {
           Invoke(nameof(TakeLife),1);
        }
        else
        {
            Invoke(nameof(ResetGameSession),1);
        }
        */
    }
    /*
    public void AddToScore(int pointToAdd)
    {
        score+= pointToAdd;
        scoreText.text = score.ToString();
    }
    */
    /*
    public void AddLife(int addedLife)
    {
        playerLives+=addedLife;
        livesText.text = playerLives.ToString();
    }
    */

    public void AddAmmo(int addedAmmo)
    {
        ammo+=addedAmmo;
        ammoText.text = ammo.ToString();
    }

    public void removeAmmo()
    {
        
        if(ammo>0)
        {
            ammo--;
        }
        
        ammoText.text = ammo.ToString();
        
    }
    
    void ResetGameSession()
    {
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }
    public void ResetGameSessionMain()
    {
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void TakeLife()
    {
        //playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        //livesText.text = playerLives.ToString();
    }

    public void pauseGame()
    {
        FindObjectOfType<PauseMenu>().Pause();
        
    }
    
}
