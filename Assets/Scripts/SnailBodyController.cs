﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailBodyController : MonoBehaviour
{
    //This code controls the whole snail's movement from the root object.

    public float speed;
    public Vector3 averageNormal;
    public bool touchingGround;
    private float moveHorizontal;
    public static bool facingRight = true;
    private Vector3 groundPoint;


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


    private void UpdatePlayerTransform(Vector3 movementDirection)
    {
        RaycastHit rightHitInfo;
        RaycastHit leftHitInfo;
        RaycastHit forwardHitInfo;
        Vector3 averagePoint;


        if (GetRaycastDownAtNewPosition(movementDirection, out rightHitInfo, out leftHitInfo))
        {
            //Not falling, so all rigidbodies of the snail are turned kinematic and gravity off.
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r = 0; r < allRBs.Length; r++)
            {
                allRBs[r].useGravity = false;
                allRBs[r].isKinematic = true;
            }
            //Getting the right angles depending whether there is obstacle coming up infront.
            if (GetHeadRayHigh(movementDirection, out forwardHitInfo))
            {
                averageNormal = (leftHitInfo.normal + forwardHitInfo.normal) / 2;
                averagePoint = (leftHitInfo.point + forwardHitInfo.point) / 2;
            }
            else if (GetHeadRayLow(movementDirection, out forwardHitInfo))
            {
                averageNormal = (leftHitInfo.normal + forwardHitInfo.normal) / 2;
                averagePoint = (leftHitInfo.point + forwardHitInfo.point) / 2;
            }
            else
            {
                averageNormal = (leftHitInfo.normal + rightHitInfo.normal) / 2;
                averagePoint = (leftHitInfo.point + rightHitInfo.point) / 2;
            }

            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);

            if (moveHorizontal > 0)
            {
                facingRight = true;
                transform.localRotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);
            }
            else if (moveHorizontal < 0)
            {
                facingRight = false;
                transform.localRotation = Quaternion.Euler(0, 180, -finalRotation.eulerAngles.z);
            }


            transform.position = averagePoint + transform.up * 0.25f;
        }

        else
        {
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r = 0; r < allRBs.Length; r++)
            {
                allRBs[r].useGravity = true;
                allRBs[r].isKinematic = false;
            }

        }
    }

    //Raycast tracking ground underneath.
    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo, out RaycastHit leftHitInfo)
    {

        Ray rightRay;
        Ray leftRay;
        float rayLength = 1f;
        if (movementDirection.x != 0)
        {
            rightRay = new Ray(transform.position + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.position + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.position + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.green);
            Debug.DrawRay(transform.position + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.red);
        }

        else
        {
            rightRay = new Ray(transform.position + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.position - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.position + transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.green);
            Debug.DrawRay(transform.position - transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.red);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, rayLength, LayerMask.GetMask("Ground"));
        bool leftCast = Physics.Raycast(leftRay, out leftHitInfo, rayLength, LayerMask.GetMask("Ground"));


        if (rightCast && leftCast)
        {
            touchingGround = true;
            return true;
        }

        touchingGround = false;
        return false;
    }

    //Raycast tracking obstacle that is coming from forward. This one is pointed downwards.
    private bool GetHeadRayLow(Vector3 movementDirection, out RaycastHit forwardInfo)
    {
        Ray forwardRay;
        float rayLength = 0.82f;
        if (movementDirection.x != 0)
        {
            forwardRay = new Ray(transform.position + (transform.right * speed), transform.right);
            Debug.DrawRay(transform.position + (transform.right * speed), transform.right * rayLength, Color.green);
        }
        else
        {
            forwardRay = new Ray(transform.position, transform.right);
            Debug.DrawRay(transform.position, transform.right * rayLength, Color.green);
        }
        bool forwardCast = Physics.Raycast(forwardRay, out forwardInfo, rayLength, LayerMask.GetMask("Ground"));

        if (forwardCast)
        {
            return true;
        }

        return false;
    }

    //Raycast tracking obstacle that is coming from forward. This one is pointed upwards.
    private bool GetHeadRayHigh(Vector3 movementDirection, out RaycastHit forwardInfo)
    {
        Ray forwardRay;
        float rayLength = 0.82f;
        if (movementDirection.x != 0)
        {
            forwardRay = new Ray(transform.position + (transform.right * speed), transform.right + (transform.up * 0.5f));
            Debug.DrawRay(transform.position + (transform.right * speed), (transform.right + (transform.up * 0.5f)).normalized * rayLength, Color.green);
        }
        else
        {
            forwardRay = new Ray(transform.position, transform.right + (transform.up * 0.5f));
            Debug.DrawRay(transform.position, (transform.right + (transform.up * 0.5f)).normalized * rayLength, Color.green);
        }
        bool forwardCast = Physics.Raycast(forwardRay, out forwardInfo, rayLength, LayerMask.GetMask("Ground"));

        if (forwardCast)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Environment")
        {
            groundPoint = transform.position;
            Debug.Log("touching ground");
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Environment")
        {
            Vector3 difference = transform.position - groundPoint;
            transform.position = groundPoint - difference;

            GameObject[] snailBones = GameObject.FindGameObjectsWithTag("Bone Object");
            for (int i = 0; i < snailBones.Length - 1; i++)
            {
                snailBones[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            Debug.Log("inside ground");
            Debug.Log(collision);
        }
    }

}
