﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle windowedToggle;
    public Slider musicSlider;
    public Slider soundSlider;
    public AudioSource sound;
    public GameObject dialog;
    private string[] strlist;
    private int width;
    private int height;
    private Boolean windowed;
    private string resolution;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 1.0f);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Background Music", musicSlider.value);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Background Music");
        width = Screen.width;
        height = Screen.height;
        resolution = width + "x" + height;
        var listAvailableStrings = resolutionDropdown.options.Select(option => option.text).ToList();
        resolutionDropdown.value = listAvailableStrings.IndexOf(resolution);
        Debug.Log(listAvailableStrings.IndexOf(resolution));

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

    public void Apply()
    {
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

    public void Back()
    {
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
        SceneManager.LoadScene("StartScene");
    }

    public void SoundValueChanged()
    {
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().SetVolume("Button Sound", soundSlider.value);
        GameObject.FindGameObjectWithTag("Audio Controller").GetComponent<AudioController>().PlayMusic("Button Sound");
    }
}
