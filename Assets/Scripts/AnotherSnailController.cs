using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherSnailController : MonoBehaviour
{


    public float speed;
    public Rigidbody rigidBody;


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement;
        if (transform.localRotation.x < 90 && transform.localRotation.x > -90)
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
            Vector3 correctedPoint = new Vector3(averagePoint.z, averagePoint.y, averagePoint.x);
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);


            transform.localRotation = Quaternion.Euler(-finalRotation.eulerAngles.z, 90, 90);


            transform.localPosition = averagePoint + transform.forward * 0.5f;
        }
    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo, out RaycastHit leftHitInfo)
    {
        Vector3 newPosition = transform.localPosition;
        Vector3 halfy = new Vector3(transform.localScale.y / 2, 0, 0);
        Ray rightRay;
        Ray leftRay;

        Vector3 flip = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);



        if (movementDirection.x > 0)
        {
            rightRay = new Ray(flip + (-transform.right * speed) + transform.localScale.y * transform.right, -transform.forward);
            leftRay = new Ray(flip + (-transform.right * speed) - transform.localScale.y * transform.right, -transform.forward);
            Debug.DrawRay(flip + (-transform.right * speed) + transform.localScale.y * transform.right, -transform.forward, Color.green);
            Debug.DrawRay(flip + (-transform.right * speed) - transform.localScale.y * transform.right, -transform.forward, Color.red);
        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(flip + (transform.right * speed) + transform.localScale.y * transform.right, -transform.forward);
            leftRay = new Ray(flip + (transform.right * speed) - transform.localScale.y * transform.right, -transform.forward);
            Debug.DrawRay(flip + (transform.right * speed) + transform.localScale.y * transform.right, -transform.forward, Color.green);
            Debug.DrawRay(flip + (transform.right * speed) - transform.localScale.y * transform.right, -transform.forward, Color.red);
        }
        else
        {
            rightRay = new Ray(flip + transform.localScale.y * transform.right, -transform.forward);
            leftRay = new Ray(flip - transform.localScale.y * transform.right, -transform.forward);
            Debug.DrawRay(flip + transform.localScale.y * transform.right, -transform.forward, Color.green);
            Debug.DrawRay(flip - transform.localScale.y * transform.right, -transform.forward, Color.red);
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
