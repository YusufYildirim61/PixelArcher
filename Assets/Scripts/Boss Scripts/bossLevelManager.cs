using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class bossLevelManager : MonoBehaviour
{
    [SerializeField] public int playerHealth = 10;
    [SerializeField] public TextMeshProUGUI playerHealthText;
    [SerializeField] GameObject player;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    playerMovement PlayerMovement;
    
    
    void Start()
    {
        
        
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnimator = player.GetComponent<Animator>();
        playerHealthText.text = playerHealth.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restartLevel()
    {
        if(playerHealth<=0)
        {
            PlayerMovement.dieAndReload();
            Invoke("reloadScene",1f);
            
        }
    }
    public void removeHealth(int removedHealth)
    {
        playerHealth-=removedHealth;
        playerHealthText.text = playerHealth.ToString();

    }
    void reloadScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
    
}
