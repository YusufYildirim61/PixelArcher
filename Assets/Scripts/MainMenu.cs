using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject chaptersCanvas;
    [SerializeField] GameObject undergroundCanvas;
    //[SerializeField] GameObject villageCanvas;
    [SerializeField] GameObject skinSelectCanvas;
    [SerializeField] GameObject settingsCanvas;
    private int sceneToContinue;
    public GameObject selectedSkin;

    
    
    
    
    

    void Start() 
    {
        //FindObjectOfType<GameSession>().enabled=false;
        
        selectedSkin.SetActive(false);
        settingsCanvas.SetActive(false);
        chaptersCanvas.SetActive(false);
        undergroundCanvas.SetActive(false);
        //villageCanvas.SetActive(false);
        skinSelectCanvas.SetActive(false);
        PlayerPrefs.DeleteKey("ammo");
        
        
    }

    
    public void playGame()
    {
        
        SoundManagerScript.PlaySound("cursor");
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");
        if(sceneToContinue == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneToContinue-1);
             Time.timeScale = 1;
        }
        if(sceneToContinue ==0)
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene(sceneToContinue);
             Time.timeScale = 1;
        }
        
        
        
    }

    public void openChapters()
    {
        
        SoundManagerScript.PlaySound("cursor");
        mainMenuCanvas.SetActive(false);
        chaptersCanvas.SetActive(true);
        undergroundCanvas.SetActive(false);
        //villageCanvas.SetActive(false);
        
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void openSettingsPage()
    {
        SoundManagerScript.PlaySound("cursor");
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void openSkinSelection()
    {
        SoundManagerScript.PlaySound("cursor");
        skinSelectCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        selectedSkin.SetActive(true);
    }
    

    public void backToMenu()
    {
        SoundManagerScript.PlaySound("cursor");
        mainMenuCanvas.SetActive(true);
        chaptersCanvas.SetActive(false);
        undergroundCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        skinSelectCanvas.SetActive(false);
        selectedSkin.SetActive(false);
    }
    public void undergroundChapter()
    {
        SoundManagerScript.PlaySound("cursor");
        mainMenuCanvas.SetActive(false);
        chaptersCanvas.SetActive(false);
        undergroundCanvas.SetActive(true);
    }
    public void villageChapter()
    {
        SoundManagerScript.PlaySound("cursor");
        chaptersCanvas.SetActive(false);
        //villageCanvas.SetActive(true);
    }
    public void openLevelOne()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void openLevelTwo()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void openLevelThree()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void openLevelFour()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }
    public void openLevelFive()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(5);
        Time.timeScale = 1;
    }
    public void openLevelSix()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(6);
        Time.timeScale = 1;
    }
    public void openLevelSeven()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(7);
        Time.timeScale = 1;
    }
    public void openLevelEight()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(8);
        Time.timeScale = 1;
    }
    public void openLevelNine()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(9);
        Time.timeScale = 1;
    }
    public void openLevelTen()
    {
        SoundManagerScript.PlaySound("cursor");
        SceneManager.LoadScene(10);
        Time.timeScale = 1;
    }
    public void deletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
