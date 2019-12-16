using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTailController : MonoBehaviour
{
    //This code controls each tail-piece except for tail end. Aim is that the piece rests on the ground in right angle.


    public GameObject root;
    public GameObject previousBone;
    protected Vector3 averageNormal;
    private SnailTailController snailTailController;
    private SnailBodyController anotherSnailController;
    private bool isRoot;
    public float rayLength;
    public bool touchingGround;
    public bool colliderTouchingGround;
    public float offset;
    private float moveHorizontal;

    void Start()
    {
        //we need to know if the piece higher in hierarchy is another tail piece or the root.
        if (previousBone.GetComponent<SnailTailController>())
        {
            snailTailController = previousBone.GetComponent<SnailTailController>();
            isRoot = false;
        }

        else if (previousBone.GetComponent<SnailBodyController>())
        {
            anotherSnailController = previousBone.GetComponent<SnailBodyController>();
            isRoot = true;
        }
    }


    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement;
        if (transform.localRotation.z < 90 && transform.localRotation.z > -90)
        {
            movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        }
        else
        {
            movement = new Vector3(-moveHorizontal, 0.0f, 0.0f);
        }
        UpdatePlayerTransform(movement);
    }

    //Here we do actual rotation if needed.
    private void UpdatePlayerTransform(Vector3 movementDirection)
    {
        RaycastHit rightHitInfo;

        if (GetRaycastDownAtNewPosition(movementDirection, out rightHitInfo))
        {
            Vector3 previousNormal;
            averageNormal = rightHitInfo.normal;

            if (isRoot)
            {
                previousNormal = anotherSnailController.averageNormal;
            }

            else
            {
                previousNormal = snailTailController.averageNormal;
            }


            if (previousNormal != averageNormal)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(averageNormal, previousNormal);
                Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);

                if(moveHorizontal >= 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, -(finalRotation.eulerAngles.z + offset));
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, (finalRotation.eulerAngles.z + offset));
                }

            }

            else
            {

                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if(!colliderTouchingGround)
            {
                transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), Space.Self);
            }

            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }

    }

    //Drawing raycast downwards to follow where and what angle ground is.
    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;

        Ray rightRay;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, (rayLength + 0.02f), LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            touchingGround = true;
            return true;
        }

        touchingGround = false;
        return false;
    }

    //Extra tracking of ground with snail's trigger colliders. We had issues with the snail going through ground sometimes despite tracking it with rays. Because we had to use 
    //kinematic without gravity on snail's rigidbodies to make it move in right manner, we couldn't use non trigger colliders, as those do not simply work with kinematic 
    //rigidbodies.
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Environment")
        {
            colliderTouchingGround = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Environment")
        {
            transform.Rotate(new Vector3(0, 0, -1.0f), Space.Self);
            colliderTouchingGround = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Environment")
        {
            transform.Rotate(new Vector3(0, 0, 1.0f), Space.Self);
            colliderTouchingGround = false;
        }
    }

}
