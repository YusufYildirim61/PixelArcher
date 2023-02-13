using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerSkin;
    public TextMeshProUGUI skinCostText;
    public Button saveButton;
    public GameObject lockedButton;
    public Image coinImage;
    int totalMoney;
    int isSkin1Sold;
    int isSkin2Sold;
    
   
    void Start() 
    {
        
        lockedButton.SetActive(true);
        totalMoney = PlayerPrefs.GetInt("TotalMoney");
        
        
    }
    void Update() 
    {
        
        if(selectedSkin==0)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        if(selectedSkin == 1 && isSkin1Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "30 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 1 && isSkin1Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        
        if(selectedSkin == 2 && isSkin2Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "100 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 2 && isSkin2Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        isSkin1Sold = PlayerPrefs.GetInt("IsSkin1Sold");
        isSkin2Sold = PlayerPrefs.GetInt("IsSkin2Sold");
        totalMoney = PlayerPrefs.GetInt("TotalMoney");
        
    }
    

    public void NextOption()
    {
        SoundManagerScript.PlaySound("cursor");
        selectedSkin = selectedSkin+1;
       
        if(selectedSkin == skins.Count)
        {
            selectedSkin = 0;
            
        }
        sr.sprite = skins[selectedSkin];
        Debug.Log(selectedSkin);
    }
    public void BackOption()
    {
        SoundManagerScript.PlaySound("cursor");
        selectedSkin = selectedSkin-1;
        
        if(selectedSkin < 0)
        {
           
            selectedSkin = skins.Count -1;
        }
        sr.sprite = skins[selectedSkin];
        Debug.Log(selectedSkin);
    }
    public void unlockSkin()
    {
        if(selectedSkin ==1)
        {
            if(totalMoney>=30)
            {
                
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=30;
                PlayerPrefs.SetInt("IsSkin1Sold",1);
                PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
                PlayerPrefs.SetInt("TotalMoney",totalMoney);
                FindObjectOfType<GameManager>().moneyText.text = totalMoney.ToString()+" X";
                //SceneManager.LoadScene(0);
            }
            else
            {
                SoundManagerScript.PlaySound("error");
            }
            
        }
        if(selectedSkin==2)
        {
            if(totalMoney>=100)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=100;
                PlayerPrefs.SetInt("IsSkin2Sold",1);
                PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
                PlayerPrefs.SetInt("TotalMoney",totalMoney);
                FindObjectOfType<GameManager>().moneyText.text = totalMoney.ToString() +" X";
                //SceneManager.LoadScene(0);
            }
            else
            {   
                SoundManagerScript.PlaySound("error");
            }
        }
            
        
        
    }
    public void selectSkin()
    {
        
        
        if(selectedSkin==0)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==1)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==2)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        
    }
    
    
}
