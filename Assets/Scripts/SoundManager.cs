using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void Start()
    {
        audioAmbience = GetComponent<AudioSource>();
    }


    public static AudioClip GetRandomSound(List<AudioClip> audioList)
    {
        return audioList[Random.Range(0, audioList.Count)];
    }
}
