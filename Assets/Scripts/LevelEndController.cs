using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    //This code controls the level ending events. When player approaches the friend snail, level ends, and score is saved if it is a high score.

    public GameObject endDialog;
    public GameObject snail;
    public GameObject shell;
    public Text scoreText;
    public static bool gameOver;

    void Start()
    {
        gameOver = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bone Object" || other.tag == "Shell")
        {
            endDialog.SetActive(true);
            snail.SetActive(false);
            shell.SetActive(false);
            gameOver = true;
            scoreText.text = LevelController.finalScore.ToString();
            AudioSource win = GetComponent<AudioSource>();
            win.volume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
            win.Play();
        }
    }

    public void ContinueButton()
    {
        string levelScore = "level" + LevelMenuController.levelIndex + "Score";
        if(PlayerPrefs.GetInt(levelScore, 0) < LevelController.finalScore)
        {
            PlayerPrefs.SetInt(levelScore, LevelController.finalScore);
        }
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
        SceneManager.LoadScene("LevelMenuScene");
    }
}
