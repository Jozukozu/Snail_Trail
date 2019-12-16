using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    //This code controls the settins screen.

    public Dropdown resolutionDropdown;
    public Toggle windowedToggle;
    public Slider musicSlider;
    public Slider soundSlider;
    public GameObject dialog;
    private string[] strlist;
    private int width;
    private int height;
    private Boolean windowed;
    private string resolution;


    void Start()
    {
        //First we set the music and sound slider values to what they were saved at or to default if none is saved.
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        //Then we play the background music track with the volume value from the slider.
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Background Music", musicSlider.value);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Background Music");
        //Getting resolution values.
        width = Screen.width;
        height = Screen.height;
        resolution = width + "x" + height;
        var listAvailableStrings = resolutionDropdown.options.Select(option => option.text).ToList();
        //Setting the value of resolution dropdown list to what the current resolution is.
        resolutionDropdown.value = listAvailableStrings.IndexOf(resolution);

        if (Screen.fullScreen == true)
        {
            windowed = false;
            windowedToggle.isOn = false;
        } else
        {
            windowed = true;
            windowedToggle.isOn = true;
        }
    }

    void OnGUI()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Background Music", musicSlider.value);
    }

    public void ResolutionDropdownValueChanged()
    {
        strlist = resolutionDropdown.captionText.text.Split('x');
    }

    public void WindowedToggleChanged()
    {
        windowed = windowedToggle.isOn;
    }

    //When apply button is pressed, setting are saved to playerpreferences. Playerpreferences get saved in registry.
    public void Apply()
    {
        ButtonSound();
        try
        {
            width = int.Parse(strlist[0]);
            height = int.Parse(strlist[1]);
        } catch(Exception e)
        {
            width = 1920;
            height = 1200;
        }
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        Debug.Log(width.ToString() + ", " + height.ToString() + ", " + windowed);
        Screen.SetResolution(width, height, !windowed, 0);
        resolution = width + "x" + height;
        if (dialog.activeSelf)
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    //If there are any unsaved settings when pressing back button, it asks if you want to save them or if you just want to exit to start screen without saving them.
    public void Back()
    {
        ButtonSound();
        if(!resolution.Equals(resolutionDropdown.captionText.text) || Screen.fullScreen == windowed || musicSlider.value != PlayerPrefs.GetFloat("musicVolume", 1.0f) || soundSlider.value != PlayerPrefs.GetFloat("soundVolume", 1.0f))
        {
            Debug.Log("Back button pressed");
            dialog.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    public void DialogExit()
    {
        ButtonSound();
        SceneManager.LoadScene("StartScene");
    }

    public void SoundValueChanged()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundSlider.value);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }

    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }
}
