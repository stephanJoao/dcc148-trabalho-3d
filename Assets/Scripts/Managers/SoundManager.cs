using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioAmbience;

    void Start()
    {
        audioAmbience.Play();
    }


    public static AudioClip GetRandomSound(List<AudioClip> audioList)
    {
        return audioList[Random.Range(0, audioList.Count)];
    }

    public static void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
