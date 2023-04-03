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
    int isSkin3Sold;
    int isSkin4Sold;
    int isSkin5Sold;
    int isSkin6Sold;
    int isSkin7Sold;
    
   
    void Start() 
    {
        
        lockedButton.SetActive(true);
        //totalMoney = PlayerPrefs.GetInt("TotalMoney");
        
        
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
            skinCostText.text = "50 X";
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
        if(selectedSkin == 3 && isSkin3Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "150 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 3 && isSkin3Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        if(selectedSkin == 4 && isSkin4Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "200 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 4 && isSkin4Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        if(selectedSkin == 5 && isSkin5Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "250 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 5 && isSkin5Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        if(selectedSkin == 6 && isSkin6Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "300 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 6 && isSkin6Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        if(selectedSkin == 7 && isSkin7Sold == 0)
        {
            coinImage.enabled = true;
            skinCostText.text = "400 X";
            saveButton.interactable = false;
            lockedButton.SetActive(true);
        }
        else if(selectedSkin == 7 && isSkin7Sold==1)
        {
            coinImage.enabled = false;
            skinCostText.text = "";
            saveButton.interactable = true;
            lockedButton.SetActive(false);
        }
        isSkin1Sold = PlayerPrefs.GetInt("IsSkin1Sold");
        isSkin2Sold = PlayerPrefs.GetInt("IsSkin2Sold");
        isSkin3Sold = PlayerPrefs.GetInt("isSkin3Sold");
        isSkin4Sold = PlayerPrefs.GetInt("isSkin4Sold");
        isSkin5Sold = PlayerPrefs.GetInt("isSkin5Sold");
        isSkin6Sold = PlayerPrefs.GetInt("isSkin6Sold");
        isSkin7Sold = PlayerPrefs.GetInt("isSkin7Sold");
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
            if(totalMoney>=50)
            {
                
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=50;
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
        if(selectedSkin==3)
        {
            if(totalMoney>=150)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=150;
                PlayerPrefs.SetInt("IsSkin3Sold",1);
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
        if(selectedSkin==4)
        {
            if(totalMoney>=200)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=200;
                PlayerPrefs.SetInt("IsSkin4Sold",1);
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
        if(selectedSkin==5)
        {
            if(totalMoney>=250)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=250;
                PlayerPrefs.SetInt("IsSkin5Sold",1);
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
        if(selectedSkin==6)
        {
            if(totalMoney>=300)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=300;
                PlayerPrefs.SetInt("IsSkin6Sold",1);
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
        if(selectedSkin==7)
        {
            if(totalMoney>=400)
            {
                SoundManagerScript.PlaySound("confirm");
                totalMoney -=400;
                PlayerPrefs.SetInt("IsSkin7Sold",1);
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
        if(selectedSkin==3)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==4)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==5)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==6)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        if(selectedSkin==7)
        {
            SoundManagerScript.PlaySound("cursor");
            PlayerPrefs.SetInt("SelectedSkin",selectedSkin);
            SceneManager.LoadScene(0);
        }
        
    }
    
    
}
