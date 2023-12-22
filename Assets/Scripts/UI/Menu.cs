using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource audioMenu;

    public GameObject playBtn;
    public GameObject instructionsBtn;
    public GameObject loadingTxt;
    public Slider loadingSlider;
    
    void Start()
    {
        audioMenu.Play();
    }
    
    public void onPlayButton()
    {
        StartCoroutine(nameof(LoadingCoroutine));
        loadingTxt.SetActive(true);
        loadingSlider.gameObject.SetActive(true);
    }
    
    IEnumerator LoadingCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameScene");
        SceneManager.
        if(!asyncOperation.isDone) 
        {
            loadingSlider.value = asyncOperation.progress;
        }

        return new WaitUntil(() => asyncOperation.isDone);

    }
    public void onInstructionsButton()
    {
        SceneManager.LoadScene("InstructionsScene");
    }
}
