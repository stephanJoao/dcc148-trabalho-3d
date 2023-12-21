using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioMenu;
    
    void Start()
    {
        audioMenu.Play();
    }
    
    public void onPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void onInstructionsButton()
    {
        SceneManager.LoadScene("InstructionsScene");
    }
}
