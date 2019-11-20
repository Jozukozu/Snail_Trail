﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{

    private AudioSource _audioSource;
    private AudioSource backgroundMusic;
    private AudioSource buttonSound;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);


        Component[] audios;
        audios = GetComponents(typeof(AudioSource));
        buttonSound = (AudioSource)audios[0];
        backgroundMusic = (AudioSource)audios[1];

        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        SetVolume("Background Music", musicVolume);
        SceneManager.LoadScene("StartScene");
    }

    public void PlayMusic(string tag)
    {
        if(tag == "Background Music")
        {
            if (backgroundMusic.isPlaying)
            {
                return;
            }

            backgroundMusic.Play();
        }

        else if(tag == "Button Sound")
        {
            buttonSound.Play();
        }
        
    }


    public void StopMusic(string tag)
    {
        if (tag == "Background Music")
        {
            backgroundMusic.Stop();
        }

        else if (tag == "Button Sound")
        {
            buttonSound.Stop();
        }
    }

    public void SetVolume(string tag, float volume)
    {
        if (tag == "Background Music")
        {
            backgroundMusic.volume = volume;
        }

        else if (tag == "Button Sound")
        {
            buttonSound.volume = volume;
        }

    }
}