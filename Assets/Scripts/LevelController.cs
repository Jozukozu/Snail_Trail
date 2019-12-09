using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

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
        InvokeRepeating("decreaseEnergy", 5.0f, 1.0f);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f); 
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().StopMusic("Background Music");
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Level Background Music", musicVolume);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Level Background Music");

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundVolume);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escMenu.SetActive(true);
            CancelInvoke("decreaseEnergy");
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
            CancelInvoke("decreaseEnergy");
        }
        finalScore = baseScore + energyScore;
        energyText.text = "Energy:" + energy.ToString();
        scoreText.text ="Points: " + finalScore.ToString();
    }

    void decreaseEnergy()
    {
        Debug.Log("decreaseEnergy");
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
        InvokeRepeating("decreaseEnergy", 1.0f, 1.0f);
        snail.SetActive(true);
        shell.SetActive(true);
    }

    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }
}
