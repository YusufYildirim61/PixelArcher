using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    public float[] highScores;
    void Start()
    {
        //money = PlayerPrefs.GetInt("TotalMoney");
        //moneyText.text = money.ToString() +" X";
    }
    

    // Update is called once per frame
    void Update()
    {
       money = PlayerPrefs.GetInt("TotalMoney");
       moneyText.text = money.ToString() +" X <sprite=0>";
       getHighScores();

    }
    void getHighScores()
    {
        highScores[1] = PlayerPrefs.GetFloat("Level1HS");
        highScores[2] = PlayerPrefs.GetFloat("Level2HS");
        highScores[3] = PlayerPrefs.GetFloat("Level3HS");
        highScores[4] = PlayerPrefs.GetFloat("Level4HS");
        highScores[5] = PlayerPrefs.GetFloat("Level5HS");
        highScores[6] = PlayerPrefs.GetFloat("Level6HS");
        highScores[7] = PlayerPrefs.GetFloat("Level7HS");
        highScores[8] = PlayerPrefs.GetFloat("Level8HS");
        highScores[9] = PlayerPrefs.GetFloat("Level9HS");
        highScores[10] = PlayerPrefs.GetFloat("Level10HS");
        highScores[11] = PlayerPrefs.GetFloat("Level11HS");
    }   
    public void deleteMoney()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
