using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    private bool snailInTrigger;
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
        if(other.CompareTag("Bone Object") || other.CompareTag("Shell"))
        {
            snailInTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bone Object") || other.CompareTag("Shell"))
        {
            snailInTrigger = true;
        }
    }

    void OnGUI()
    {
        //If the boolean is active, display the text
        if (snailInTrigger == true)
        {
            GUI.Label(new Rect(transform.position.x, transform.position.y, 0, 0), "Hello!");
        }
    }
}
