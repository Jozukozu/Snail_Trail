using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    public AudioSource sound;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().SetVolume(musicVolume);
    }


    public void StartButton()
    {
        ButtonSound();
        //SceneManager.LoadScene("LevelMenuScene");
    }

    public void SettingsButton()
    {
        ButtonSound();
        SceneManager.LoadScene("SettingScene");
    }

    public void ButtonSound()
    {
        sound.volume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        sound.Play();
    }
}
