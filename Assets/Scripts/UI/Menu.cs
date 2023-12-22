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

    private int i = 0;
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

        loadingSlider.value = i;
        return new WaitUntil(() => loadingSlider.value == 100);

    }
    public void onInstructionsButton()
    {
        SceneManager.LoadSceneAsync("InstructionsScene");
    }
}
