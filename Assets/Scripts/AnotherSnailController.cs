using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherSnailController : MonoBehaviour
{

    public Rigidbody rigidBody;
    public float speed = 10.0f;


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rigidBody.AddForce(movement * speed);
    }
}
