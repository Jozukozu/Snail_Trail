using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    public GameObject endDialog;
    public GameObject snail;
    public GameObject shell;
    public Text scoreText;
    public static bool gameOver = false;

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
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
        SceneManager.LoadScene("LevelMenuScene");
    }
}
