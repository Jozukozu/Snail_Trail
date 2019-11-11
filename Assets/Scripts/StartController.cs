using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();

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
