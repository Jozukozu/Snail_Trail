using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailBodyController : MonoBehaviour
{

    public float speed;
    public Vector3 averageNormal;
    public bool touchingGround;
    private float moveHorizontal;
    public static bool facingLeft;
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
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r = 0; r < allRBs.Length; r++)
            {
                allRBs[r].useGravity = false;
                allRBs[r].isKinematic = true;
            }
            //if(GetRaycastForwardAtNewPosition(movementDirection, out forwardHitInfo))
            //{
            //    averageNormal = (leftHitInfo.normal + forwardHitInfo.normal) / 2;
            //    averagePoint = (leftHitInfo.point + forwardHitInfo.point) / 2;
            //}
            //else
            //{
            averageNormal = (leftHitInfo.normal + rightHitInfo.normal) / 2;
                averagePoint = (leftHitInfo.point + rightHitInfo.point) / 2;
            //}
            //Debug.Log("real root: " + averageNormal);
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);

            if(moveHorizontal > 0)
            {
                facingLeft = false;
                transform.localRotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);
            }
            else if (moveHorizontal < 0)
            {
                facingLeft = true;
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
        //else if (movementDirection.x < 0)
        //{
        //    rightRay = new Ray(transform.position + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
        //    leftRay = new Ray(transform.position + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
        //    Debug.DrawRay(transform.position + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.green);
        //    Debug.DrawRay(transform.position + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.red);
        //}
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

    private bool GetRaycastForwardAtNewPosition(Vector3 movementDirection, out RaycastHit forwardInfo)
    {
        Ray forwardRay;
        float rayLength = 0.5f;
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

        if(forwardCast)
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
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GameObject[] snailBones = GameObject.FindGameObjectsWithTag("Bone Object");
            for (int i = 0; i < snailBones.Length; i++)
            {
                snailBones[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            Debug.Log("inside ground");
        }
    }

}
