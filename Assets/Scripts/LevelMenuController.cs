using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{

    public Text levelName;
    public Button startButton;
    public Text highscoreLabel;
    public Text highscore;

    private int levelIndex;

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
        Debug.Log("Game started, level " + levelIndex);
        string levelName = "Level" + levelIndex + "Scene";
        SceneManager.LoadScene(levelName);
    }

    public void BackButton()
    {
        ButtonSound();
        SceneManager.LoadScene("StartScene");
    }

    public void LevelButton(Text name)
    {
        ButtonSound();
        levelName.text = name.text;

        highscoreLabel.gameObject.SetActive(true);
        highscore.gameObject.SetActive(true);
        startButton.interactable = true;
    }

    public void LevelButton(int index)
    {
        levelIndex = index;
    }

    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }

}
