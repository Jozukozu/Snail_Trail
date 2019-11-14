using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject snail;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - snail.transform.position;
    }

    void LateUpdate()
    {
        transform.position = snail.transform.position + offset;
    }
}
