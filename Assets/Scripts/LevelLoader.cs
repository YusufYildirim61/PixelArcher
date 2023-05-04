using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public void LoadLevel(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneIndex));
        //Time.timeScale = 1;
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        

        while(!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress/.9f);
            slider.value = progess;
            progressText.text = progess*100f +"%";
            yield return null;
        }
    }
}
