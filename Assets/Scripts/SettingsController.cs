using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown refreshRateDropdown;
    public Toggle windowedToggle;
    private string[] strlist;
    private string refRatestr;
    private int width;
    private int height;
    private int refreshRate;
    private Boolean windowed;

    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        string resolution = width + "x" + height;
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

    public void ResolutionDropdownValueChanged()
    {
        strlist = resolutionDropdown.captionText.text.Split('x');
    }

    public void RefreshRateValueChanged()
    {
        refRatestr = refreshRateDropdown.captionText.text.Substring(0, 2);
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
        try
        {
            refreshRate = int.Parse(refRatestr);
        }
        catch (Exception e)
        {
            refreshRate = 0;
        }
        Debug.Log(width.ToString() + ", " + height.ToString() + ", " + windowed + ", " + refreshRate.ToString());
        Screen.SetResolution(width, height, !windowed, refreshRate);
        Debug.Log(Screen.currentResolution);
    }
}
