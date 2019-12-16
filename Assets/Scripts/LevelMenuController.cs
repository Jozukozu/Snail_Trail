using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    //This code controls the level menu. In level menu you can choose the level you want to play, if it is unlocked.

    public Text levelName;
    public Button startButton;
    public Text highscoreLabel;
    public Text highscore;
    public Button level1Button;

    public static int levelIndex;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().StopMusic("Level Background Music");
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Background Music", musicVolume);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Background Music");

        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundVolume);

        //If player has played through tutorial level, it unlocks level 1. Level 1 does not exist at the moment.
        if(PlayerPrefs.GetInt("level0Score") > 0)
        {
            level1Button.interactable = true;
        }
        else
        {
            level1Button.interactable = false;
        }
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

    //This functions makes the info of chosen level show up in the box right of the screen. It shows the name of the level and player's high score on it.
    public void LevelButton(Text name)
    {
        ButtonSound();
        levelName.text = name.text;

        if (levelIndex == 1)
        {
            highscoreLabel.gameObject.SetActive(true);
            highscore.gameObject.SetActive(false);
            highscoreLabel.text = "Coming soon!";
            startButton.interactable = false;
        }
        else
        {
            string levelScore = "level" + levelIndex + "Score";
            int highScore = PlayerPrefs.GetInt(levelScore, 0);
            highscoreLabel.gameObject.SetActive(true);
            highscore.gameObject.SetActive(true);
            highscore.text = highScore.ToString();
            startButton.interactable = true;
        }
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
