﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    //This code controls the tutorial texts that pop and and disappear in the tutorial level.

    private bool snailInTrigger;
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bone Object" || other.tag == "Shell")
        {
            snailInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bone Object" || other.tag == "Shell")
        {
            snailInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bone Object" || other.tag == "Shell")
        {
            snailInTrigger = false;
        }
    }

    void OnGUI()
    {
        if (snailInTrigger == true)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
        }
    }
}
