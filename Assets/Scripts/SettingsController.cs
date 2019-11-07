using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Dropdown dropdown;
    private string[] strlist;

    //void Start()
    //{
    //    dropdown = GetComponent<Dropdown>();
    //    dropdown.onValueChanged.AddListener(delegate
    //    {
    //        DropdownValueChanged(dropdown);
    //    });
    //}

    public void DropdownValueChanged()
    {
        strlist = dropdown.captionText.text.Split('x');
    }

    public void Apply()
    {
        int width = int.Parse(strlist[0]);
        int height = int.Parse(strlist[1]);
        Debug.Log(strlist[0]);
        Debug.Log(strlist[1]);
        Screen.SetResolution(width, height, true);
    }
}
