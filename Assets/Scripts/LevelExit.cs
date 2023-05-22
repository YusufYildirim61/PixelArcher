using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private int currentSceneIndex; 
    public int nextSceneLoad;
    //[SerializeField] float levelLoadDelay = 1f;
    
    playerMovement playerMovement;
    public GameObject levelCompleteScreen;
    public GameObject gameSession;
    void Start() 
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        currentSceneIndex= SceneManager.GetActiveScene().buildIndex;
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex +1;
        
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player" && FindObjectOfType<playerMovement>().isAlive==true)
        {
            FindObjectOfType<AudioManager>().levelMusic.Stop();
            SoundManagerScript.PlaySound("levelComplete");
            //PlayerPrefs.SetInt("TotalMoney",FindObjectOfType<GameManager>().money); 
            PlayerPrefs.SetInt("SavedScene",nextSceneLoad);
            FindObjectOfType<LevelComplete>().ShowStatAnim();
            FindObjectOfType<LevelComplete>().transform.localScale = new Vector3(1,1,1);
            playerMovement.isLevelFinished = true;
            levelCompleteScreen.transform.localScale =new Vector3(1,1,1);
            gameSession.SetActive(false);
            if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt",FindObjectOfType<LevelExit>().nextSceneLoad);
            }
            //playerMovement.transform.Translate(Vector2.right *Time.deltaTime *6f);
            
         // StartCoroutine(LoadNextLevel());
         // if(nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
         // {
         //     PlayerPrefs.SetInt("levelAt",nextSceneLoad);
         // }
        }
        
        
    }

    public void LoadNextLevel()
    {
        //yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        
        if(nextSceneLoad == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneLoad =0;
        }
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneLoad);
    }

}



