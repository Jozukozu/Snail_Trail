using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherSnailController : MonoBehaviour
{

    public float speed;

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
            Vector3 averageNormal = (leftHitInfo.normal + rightHitInfo.normal) / 2;
            Vector3 averagePoint = (leftHitInfo.point + rightHitInfo.point) / 2;
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);


            transform.localRotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);


            transform.localPosition = averagePoint + transform.up * 0.25f;
        }
    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo, out RaycastHit leftHitInfo)
    {
        Vector3 newPosition = transform.localPosition;
        Vector3 halfX = new Vector3(transform.localScale.x / 2, 0, 0);
        Ray rightRay;
        Ray leftRay;
        if (movementDirection.x > 0)
        {
            rightRay = new Ray(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.localPosition + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up, Color.green);
            Debug.DrawRay(transform.localPosition + (transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up, Color.red);
        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(transform.localPosition + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.localPosition + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.localPosition + (-transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up, Color.green);
            Debug.DrawRay(transform.localPosition + (-transform.right * speed) - transform.localScale.x / 8 * transform.right, -transform.up, Color.red);
        }
        else
        {
            rightRay = new Ray(transform.localPosition + transform.localScale.x / 8 * transform.right, -transform.up);
            leftRay = new Ray(transform.localPosition - transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(transform.localPosition + transform.localScale.x / 8 * transform.right, -transform.up, Color.green);
            Debug.DrawRay(transform.localPosition - transform.localScale.x / 8 * transform.right, -transform.up, Color.red);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, 2, LayerMask.GetMask("Ground"));
        bool leftCast = Physics.Raycast(leftRay, out leftHitInfo, 2, LayerMask.GetMask("Ground"));


        if (rightCast && leftCast)
        {
            return true;
        }


        return false;
    }

}
