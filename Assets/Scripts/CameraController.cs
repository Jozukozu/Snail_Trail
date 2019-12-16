using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //This code controls the movement of the camera. It keeps it following the snail. Depending whether player is in shell mode or not, the camera follows either shell or 
    //snail body.

    public GameObject snail;
    public GameObject snailRoot;
    public GameObject shell;
    private Vector3 offset;

    void Start()
    {
        if(snail.activeSelf)
        {
            offset = transform.position - snailRoot.transform.position;
        } else
        {
            offset = transform.position - shell.transform.position;
        }
    }

    void LateUpdate()
    {
        if(snail.activeSelf)
        {
            transform.position = snailRoot.transform.position + offset;
        }
        else
        {
            transform.position = shell.transform.position + offset;
        }
    }
}
