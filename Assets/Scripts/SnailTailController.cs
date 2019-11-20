using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTailController : MonoBehaviour
{
    public float speed;
    public GameObject root;



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



        if (GetRaycastDownAtNewPosition(movementDirection, out rightHitInfo))
        {
            Vector3 averageNormal = rightHitInfo.normal;
            Vector3 averagePoint = rightHitInfo.point;
            Vector3 correctedPoint = new Vector3(averagePoint.z, averagePoint.y, averagePoint.x);
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, averageNormal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);


            transform.localRotation = Quaternion.Euler(finalRotation.eulerAngles.z, 0, 0);


            transform.localPosition = correctedPoint + transform.forward * 0.25f;
        }
    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.localPosition;
        Vector3 halfX = new Vector3(transform.localScale.x / 2, 0, 0);

        Vector3 offset = new Vector3(0, 0, 0);
        Vector3 rootAxis = new Vector3(root.transform.localPosition.x, root.transform.localPosition.y, root.transform.localPosition.z);
        Ray rightRay;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(rootAxis - offset + root.transform.right * speed, -root.transform.up);
            Debug.DrawRay(rootAxis - offset + root.transform.right * speed, -root.transform.up, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(rootAxis - offset - root.transform.right * speed, -root.transform.up);
            Debug.DrawRay(rootAxis - offset - root.transform.right * speed, -root.transform.up, Color.green);
        }
        else
        {
            rightRay = new Ray(rootAxis - offset, -root.transform.up);
            Debug.DrawRay(rootAxis - offset, -root.transform.up, Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, 2, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }

}
