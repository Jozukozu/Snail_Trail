using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
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
