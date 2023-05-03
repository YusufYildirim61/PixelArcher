using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeLevelManager : MonoBehaviour
{
    playerMovement player;
    Vector2 deathKick = new Vector2(10f,10f);
    LevelChanger levelChanger;
    void Start()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
        player = FindObjectOfType<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isAlive)
        {
            player.myBodyCollider.enabled = true;
            levelChanger.FadetoLevel();
            //player.dieAndReload();
            Invoke("reloadScene",1.5f);
            
        }
    }
    void reloadScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
