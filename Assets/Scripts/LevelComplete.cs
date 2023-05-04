using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI dieCountToText;
    [SerializeField] TextMeshProUGUI killCountToText;
    [SerializeField] TextMeshProUGUI coinCountToText;
     int totalScore;
     float[] highScore;
     int coinCount;
     int dieCount;
     int killCount;

     [SerializeField] int totalKill;
     [SerializeField] int totalCoin;
     public Animator animator;

    GameManager gameManager;
    playerMovement player;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        transform.localScale = new Vector3(0,0,0);
        player = FindObjectOfType<playerMovement>();
        //totalScore = 0;
        coinCount  = 0;
        dieCount   = 0;
        killCount  = 0;
    }
    void Update() 
    {
        
        saveHighScores();
        //PlayerPrefs.SetFloat("HighScore",gameManager.highScores[currentSceneIndex]);
        
    }
    void saveHighScores()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameManager.highScores[currentSceneIndex] = totalScore;
        if(currentSceneIndex==1)
        {
            if(totalScore>=PlayerPrefs.GetFloat("Level1HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level1HS",totalScore);

            }
        }
        if(currentSceneIndex==2)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level2HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level2HS",totalScore);

            }
        }
        if(currentSceneIndex==3)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level3HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level3HS",totalScore);

            }
        }
        if(currentSceneIndex==4)
        {
            if(totalScore>=PlayerPrefs.GetFloat("Level4HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level4HS",totalScore);

            }
        }
        if(currentSceneIndex==5)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level5HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level5HS",totalScore);

            }
        }
        if(currentSceneIndex==6)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level6HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level6HS",totalScore);

            }
        }
        if(currentSceneIndex==7)
        {
            if(totalScore>=PlayerPrefs.GetFloat("Level7HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level7HS",totalScore);

            }
        }
        if(currentSceneIndex==8)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level8HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level8HS",totalScore);

            }
        }
        if(currentSceneIndex==9)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level9HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level9HS",totalScore);

            }
        }
        if(currentSceneIndex==10)
        {
            if(totalScore>=PlayerPrefs.GetFloat("Level10HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level10HS",totalScore);

            }
        }
        if(currentSceneIndex==11)
        {
            
            if(totalScore>=PlayerPrefs.GetFloat("Level11HS") && player.isLevelFinished)
            {
                PlayerPrefs.SetFloat("Level11HS",totalScore);

            }
        }
        
    }
    public void AddCoinToScore(int coinPointToAdd)
    {
        coinCount++;
        totalScore+= coinPointToAdd;
        
        coinCountToText.text = "Coins Collected: " + coinCount.ToString() +"/"+totalCoin.ToString();
        totalScoreText.text = "Total Score: " +totalScore.ToString();
        
        
    }
    public void timesDied(int dieScore)
    {
        dieCount++;
        totalScore-=dieScore;
        dieCountToText.text = "Times Died: " + dieCount.ToString();
        totalScoreText.text = "Total Score: " +totalScore.ToString();
        
    }
     public void creatureKilled(int killScore)
    {
        killCount=killCount+1;
        totalScore+=killScore;
        killCountToText.text = "Creatures Killed: " + killCount.ToString()+"/"+totalKill.ToString();
        totalScoreText.text = "Total Score: " +totalScore.ToString();
        
    }
    

    public void goToNextLevel()
    {
        FindObjectOfType<LevelExit>().LoadNextLevel();
        
    }
    public void restartLevel()
    {
      var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);
      Time.timeScale = 1;
        
    }

    public void quitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    

    public void ShowStatAnim()
    {
        animator.SetTrigger("Fade");
    }
}
