using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherSnailController : MonoBehaviour
{

    public float speed;
    public Vector3 averageNormal;


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
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


        if (GetRaycastDownAtNewPosition(movementDirection, out rightHitInfo, out leftHitInfo))
        {
            Rigidbody[] allRBs = GetComponentsInChildren<Rigidbody>();
            for (int r = 0; r < allRBs.Length; r++)
            {
                allRBs[r].useGravity = false;
                allRBs[r].isKinematic = true;
            }
            averageNormal = (leftHitInfo.normal + rightHitInfo.normal) / 2;
            //Debug.Log("real root: " + averageNormal);
            Vector3 averagePoint = (leftHitInfo.point + rightHitInfo.point) / 2;
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);


            transform.localRotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);


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
        float rayLength = 0.3f;
        if (movementDirection.x > 0)
        {
            rightRay = new Ray(transform.position + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.position + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.position + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.green);
            Debug.DrawRay(transform.position + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.red);
        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(transform.position + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.position + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.position + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.green);
            Debug.DrawRay(transform.position + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up * rayLength, Color.red);
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
            return true;
        }


        return false;
    }

}
