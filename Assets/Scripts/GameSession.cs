using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class GameSession : MonoBehaviour
{
    
    //[SerializeField] int score;
    [Header("Arrow Types")]
    [SerializeField] public int ammo = 5;
    [SerializeField] public int poisonAmmo = 5;
    [SerializeField] public int iceAmmo = 5;
    [SerializeField] public int strongAmmo = 5;
    [SerializeField] GameObject arrowImage;
    [SerializeField] GameObject poisonArrowImage;
    [SerializeField] GameObject iceArrowImage;
    [SerializeField] GameObject strongArrowImage;
    [SerializeField] public TextMeshProUGUI ammoText;
    [SerializeField] public TextMeshProUGUI poisonAmmoText;
    [SerializeField] public TextMeshProUGUI iceAmmoText;
    [SerializeField] public TextMeshProUGUI strongAmmoText;
    [SerializeField] public GameObject defaultArrowButton;
    [SerializeField] public GameObject poisonArrowButton;
    [SerializeField] public GameObject iceArrowButton;
    [SerializeField] public GameObject strongArrowButton;
    [SerializeField] public GameObject KeyImage;
    public bool isOnPoisonArrow,isOnIceArrow,isOnStrongArrow = false;
    public bool isOnDefaultArrow;

    [SerializeField] public GameObject totalMoney;
    [SerializeField] public TextMeshProUGUI totalMoneyText;

    [SerializeField] GameObject pauseButton;
    public GameObject getInButton;
    //[SerializeField] TextMeshProUGUI scoreText;
    
    
    playerMovement playerMovement;
    
    ControllablePlatform controllablePlatform;
    
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
        KeyImage.SetActive(false);
        getInButton.SetActive(false);
        totalMoney.SetActive(false);
        isOnDefaultArrow = true;
        poisonArrowButton.SetActive(false);
        iceArrowButton.SetActive(false);
        strongArrowButton.SetActive(false);
        poisonAmmoText.enabled = false;
        iceAmmoText.enabled = false;
        strongAmmoText.enabled = false;
        poisonArrowImage.SetActive(false);
        iceArrowImage.SetActive(false);
        strongArrowImage.SetActive(false);
        //scoreText.text = score.ToString();
        ammoText.text = ammo.ToString();
        poisonAmmoText.text = poisonAmmo.ToString();
        iceAmmoText.text = iceAmmo.ToString();
        strongAmmoText.text = strongAmmo.ToString();
        playerMovement = FindObjectOfType<playerMovement>();
        
        
    }
     void Update() 
    {
        PlayerPrefs.GetInt("TotalMoney");
        totalMoneyText.text = FindObjectOfType<GameManager>().moneyText.text;
        poisonAmmoText.text = poisonAmmo.ToString();
        iceAmmoText.text = iceAmmo.ToString();
        strongAmmoText.text = strongAmmo.ToString();   
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

    public void changeArrowToPoison()
    {
        SoundManagerScript.PlaySound("poisonChange");
        isOnPoisonArrow = true;
        isOnDefaultArrow = false;
        poisonArrowButton.SetActive(true);
        poisonAmmoText.enabled = true;
        poisonArrowImage.SetActive(true);
        defaultArrowButton.SetActive(false);
        ammoText.enabled = false;
        arrowImage.SetActive(false);
    }
    public void changeArrowToIce()
    {
        SoundManagerScript.PlaySound("iceChange");
        isOnPoisonArrow = false;
        isOnIceArrow = true;
        poisonArrowButton.SetActive(false);
        poisonAmmoText.enabled = false;
        poisonArrowImage.SetActive(false);
        iceArrowButton.SetActive(true);
        iceAmmoText.enabled = true;
        iceArrowImage.SetActive(true);
    }
    public void changeArrowToStrong()
    {
        SoundManagerScript.PlaySound("strongChange");
        isOnIceArrow = false;
        isOnStrongArrow = true;
        iceArrowButton.SetActive(false);
        iceAmmoText.enabled = false;
        iceArrowImage.SetActive(false);
        strongArrowButton.SetActive(true);
        strongAmmoText.enabled = true;
        strongArrowImage.SetActive(true);
    }
    public void changeArrowToDefault()
    {
        SoundManagerScript.PlaySound("defaultChange");
        isOnStrongArrow = false;
        isOnDefaultArrow = true;
        strongArrowButton.SetActive(false);
        strongAmmoText.enabled = false;
        strongArrowImage.SetActive(false);
        defaultArrowButton.SetActive(true);
        ammoText.enabled = true;
        arrowImage.SetActive(true);
    }
    public void AddAmmo(int addedAmmo)
    {
        ammo+=addedAmmo;
        ammoText.text = ammo.ToString();
    }

    public void AddPoisonAmmo(int addedAmmo)
    {
        poisonAmmo+=addedAmmo;
        poisonAmmoText.text = poisonAmmo.ToString();
    }
    public void AddIceAmmo(int addedAmmo)
    {
        iceAmmo+=addedAmmo;
        iceAmmoText.text = iceAmmo.ToString();
    }
    public void AddStrongAmmo(int addedAmmo)
    {
        strongAmmo+=addedAmmo;
        strongAmmoText.text = strongAmmo.ToString();
    }

    public void removeAmmo()
    {
        
        if(ammo>0)
        {
            ammo--;
        }
        
        ammoText.text = ammo.ToString();
        
    }
    public void removePoisonAmmo()
    {
        
        if(poisonAmmo>0)
        {
            poisonAmmo--;
        }
        
        poisonAmmoText.text = poisonAmmo.ToString();
        
    }
    public void removeIceAmmo()
    {
        
        if(iceAmmo>0)
        {
            iceAmmo--;
        }
        
        iceAmmoText.text = iceAmmo.ToString();
        
    }
    public void removeStrongAmmo()
    {
        
        if(strongAmmo>0)
        {
            strongAmmo--;
        }
        
        strongAmmoText.text = strongAmmo.ToString();
        
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
