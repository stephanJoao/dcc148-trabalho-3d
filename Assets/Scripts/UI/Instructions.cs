using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public void onBackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
