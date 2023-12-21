using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
	public AudioSource audioInstructions;
    
    void Start()
    {
        audioInstructions.Play();
    }

    public void onBackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
