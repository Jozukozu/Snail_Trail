using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    private bool snailInTrigger;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
        Debug.Log(snailInTrigger);
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
