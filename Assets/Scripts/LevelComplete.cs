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
     int coinCount;
     int dieCount;
     int killCount;

     [SerializeField] int totalKill;
     [SerializeField] int totalCoin;
     public Animator animator;

    
    void Start()
    {
        transform.localScale = new Vector3(0,0,0);
        totalScore = 0;
        coinCount  = 0;
        dieCount   = 0;
        killCount  = 0;
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
