using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    public float speed;
    public GameObject snail;
    private Rigidbody rigidBody;
    private float angle;
    private int counter = 0;
    private float direction;
    private RaycastHit hit;
    private Ray ray;
    public float rayHeight;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rigidBody.AddForce(movement * speed);
        if (moveHorizontal < 0)
        {
            direction = 180.0f;
            snail.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
        else if (moveHorizontal > 0)
        {
            direction = 0.0f;
            snail.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        if (angle != rigidBody.transform.eulerAngles.z)
        {
            if(direction == 0)
            {
                rigidBody.transform.eulerAngles = new Vector3(0.0f, direction, angle);
            } else if(direction == 180)
            {
                rigidBody.transform.eulerAngles = new Vector3(0.0f, direction, -angle);
            }
        }
        ray = new Ray(snail.transform.position, Vector3.down);
        Debug.DrawRay(snail.transform.position, Vector3.down * rayHeight);
        if(!Physics.Raycast(ray, out hit, rayHeight))
        {
            Vector3 downForce = new Vector3(0.0f, -1.0f, 0.0f);
            rigidBody.AddForce(downForce * speed);
        }
        if(Input.GetAxis("Horizontal") == 0)
        {
            rigidBody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        angle = collision.transform.eulerAngles.z;
    }
}
