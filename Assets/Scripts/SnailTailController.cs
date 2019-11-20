using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTailController : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float stick;


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal != 0)
        {
            Vector3 downForce = new Vector3(0.0f, -1.0f, 0.0f);
            rigidBody.AddForce(downForce * stick);
        }

    }

}
