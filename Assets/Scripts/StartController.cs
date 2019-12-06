using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{


    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().StopMusic("Level Background Music");
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Background Music", musicVolume);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Background Music");

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundVolume);
    }


    public void StartButton()
    {
        ButtonSound();
        SceneManager.LoadScene("LevelMenuScene");
    }

    public void SettingsButton()
    {
        ButtonSound();
        SceneManager.LoadScene("SettingScene");
    }

    public void ExitButton()
    {
        ButtonSound();
        Application.Quit();
    }

    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }
}
