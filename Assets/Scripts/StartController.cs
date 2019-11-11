using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().SetVolume(musicVolume);
    }


    public void StartButton()
    {
        //SceneManager.LoadScene("LevelMenuScene");
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("SettingScene");
    }

}
