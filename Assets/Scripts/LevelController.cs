﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    //This code controls the level being played. It sets the audio, keeps track of score and works the code of pause menu.

    public static int baseScore;
    public static int energyScore;
    public Text scoreText;
    public Text energyText;
    public static int energy;
    public static int finalScore;
    public GameObject escMenu;
    public static bool eat;
    public GameObject snail;
    public GameObject shell;

    void Start()
    {
        baseScore = 0;
        energyScore = 0;
        energy = 20;
        InvokeRepeating("DecreaseEnergy", 5.0f, 1.0f);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f); 
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().StopMusic("Background Music");
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Level Background Music", musicVolume);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Level Background Music");

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundVolume);
    }

    void Update()
    {
        //Activating pause menu.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escMenu.SetActive(true);
            CancelInvoke("DecreaseEnergy");
            snail.SetActive(false);
            shell.SetActive(false);

        }
        if (eat)
        {
            AudioSource chew = GetComponent<AudioSource>();
            chew.volume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
            chew.Play();
            eat = false;
        }
    }

    void FixedUpdate()
    {
        if(LevelEndController.gameOver)
        {
            CancelInvoke("DecreaseEnergy");
        }
        finalScore = baseScore + energyScore;
        energyText.text = "Energy:" + energy.ToString();
        scoreText.text ="Points: " + finalScore.ToString();
    }

    void DecreaseEnergy()
    {
        Debug.Log("DecreaseEnergy");
        if (energy > 0) 
        {
            energy--;
        }

        else if(energy <= 0 && energyScore > 0)
        {
            energyScore--;
        }
    }

    public void YesButton()
    {
        ButtonSound();
        SceneManager.LoadScene("LevelMenuScene");
    }

    public void NoButton()
    {
        ButtonSound();
        escMenu.SetActive(false);
        InvokeRepeating("DecreaseEnergy", 1.0f, 1.0f);
        snail.SetActive(true);
        shell.SetActive(true);
    }

    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }
}
