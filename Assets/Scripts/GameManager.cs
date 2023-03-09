using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    void Start()
    {
        //money = PlayerPrefs.GetInt("TotalMoney");
        //moneyText.text = money.ToString() +" X";
    }
    

    // Update is called once per frame
    void Update()
    {
       money = PlayerPrefs.GetInt("TotalMoney");
       moneyText.text = money.ToString() +" X";
    }
    public void deleteMoney()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
