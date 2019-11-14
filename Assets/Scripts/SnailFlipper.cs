using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailFlipper : MonoBehaviour
{
    public GameObject snail;
    public GameObject snailHead;

    void FixedUpdate()
    {
        //float direction = Input.GetAxis("Horizontal");
        //if(direction < 0)
        //{
        //    snail.transform.localScale = new Vector3(-1, 1, 1);
        //}
        //else if(direction > 0)
        //{
        //    snail.transform.localScale = new Vector3(1, 1, 1);
        //}
    }

    private void LateUpdate()
    {
        snail.transform.position = snailHead.transform.position - snailHead.transform.localPosition;
    }
}
