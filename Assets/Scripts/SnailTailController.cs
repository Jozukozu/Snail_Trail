using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTailController : MonoBehaviour
{
    public float speed;
    public GameObject root;
    public GameObject previousBone;



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
        RaycastHit previousInfo;


        if (GetRaycastDownAtNewPosition(movementDirection, out rightHitInfo) && PreviousBoneGetRaycastDownAtNewPosition(movementDirection, out previousInfo))
        {

            if (rightHitInfo.normal != previousInfo.normal)
            {
                Vector3 averageNormal = rightHitInfo.normal;
                Vector3 averagePoint = rightHitInfo.point;
                Vector3 correctedPoint = new Vector3(averagePoint.z, averagePoint.y, averagePoint.x);
                Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, averageNormal);
                Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);
                Debug.Log(gameObject.name + " righthitinfo: " + rightHitInfo + " averagenormal: " + averageNormal + " targetrotation: " + targetRotation + " finalrotation " + finalRotation);

                transform.localRotation = Quaternion.Euler((finalRotation.eulerAngles.z), 0, 0);
            }

            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            transform.localPosition = transform.forward;
        }
    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;
        Vector3 halfX = new Vector3(transform.localScale.x / 2, 0, 0);

        Vector3 offset = new Vector3(0, 0, 0);
        Vector3 rootAxis = new Vector3(root.transform.localPosition.x, root.transform.localPosition.y, root.transform.localPosition.z);
        Ray rightRay;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition, -transform.forward);
            Debug.DrawRay(newPosition, -transform.forward, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition, -transform.forward);
            Debug.DrawRay(newPosition, -transform.forward, Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition, -transform.forward);
            Debug.DrawRay(newPosition, -transform.forward, Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, 2, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }



    private bool PreviousBoneGetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = previousBone.transform.position;

        Ray rightRay;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition, -previousBone.transform.forward);
            Debug.DrawRay(newPosition, -previousBone.transform.forward, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition, -previousBone.transform.forward);
            Debug.DrawRay(newPosition, -previousBone.transform.forward, Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition, -previousBone.transform.forward);
            Debug.DrawRay(newPosition, -previousBone.transform.forward, Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, 2, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }

}
