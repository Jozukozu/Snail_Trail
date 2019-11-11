using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{

    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().PlayMusic();
<<<<<<< HEAD

=======
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackgroundMusic>().SetVolume(musicVolume);
>>>>>>> 75f9ac52da020a638274ddf429509d190d85a84a
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
