using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentFollow : MonoBehaviour
{

    public GameObject parent;

    void Update()
    {
        Vector3 childLocalPosition = new Vector3(-transform.localPosition.y, transform.localPosition.z, transform.localPosition.x);
        parent.transform.position = transform.position - childLocalPosition;
    }
}
