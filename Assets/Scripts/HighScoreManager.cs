using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI level1HS;
    [SerializeField] TextMeshProUGUI level2HS;
    [SerializeField] TextMeshProUGUI level3HS;
    [SerializeField] TextMeshProUGUI level4HS;
    [SerializeField] TextMeshProUGUI level5HS;
    [SerializeField] TextMeshProUGUI level6HS;
    [SerializeField] TextMeshProUGUI level7HS;
    [SerializeField] TextMeshProUGUI level8HS;
    [SerializeField] TextMeshProUGUI level9HS;
    [SerializeField] TextMeshProUGUI level10HS;
    [SerializeField] TextMeshProUGUI level11HS;
    void Start()
    {
        level1HS.text = "High Score: "+PlayerPrefs.GetFloat("Level1HS").ToString();
        level2HS.text = "High Score: "+PlayerPrefs.GetFloat("Level2HS").ToString();
        level3HS.text = "High Score: "+PlayerPrefs.GetFloat("Level3HS").ToString();
        level4HS.text = "High Score: "+PlayerPrefs.GetFloat("Level4HS").ToString();
        level5HS.text = "High Score: "+PlayerPrefs.GetFloat("Level5HS").ToString();
        level6HS.text = "High Score: "+PlayerPrefs.GetFloat("Level6HS").ToString();
        level7HS.text = "High Score: "+PlayerPrefs.GetFloat("Level7HS").ToString();
        level8HS.text = "High Score: "+PlayerPrefs.GetFloat("Level8HS").ToString();
        level9HS.text = "High Score: "+PlayerPrefs.GetFloat("Level9HS").ToString();
        level10HS.text = "High Score: "+PlayerPrefs.GetFloat("Level10HS").ToString();
        level11HS.text = "High Score: "+PlayerPrefs.GetFloat("Level11HS").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
