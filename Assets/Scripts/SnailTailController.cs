using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTailController : MonoBehaviour
{
    public GameObject root;
    public GameObject previousBone;
    protected Vector3 averageNormal;
    private SnailTailController snailTailController;
    private AnotherSnailController anotherSnailController;
    private bool isRoot;

    void Start()
    {
        if (previousBone.GetComponent<SnailTailController>())
        {
            snailTailController = previousBone.GetComponent<SnailTailController>();
            isRoot = false;
        }

        else if (previousBone.GetComponent<AnotherSnailController>())
        {
            anotherSnailController = previousBone.GetComponent<AnotherSnailController>();
            isRoot = true;
        }

    }


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
                Debug.Log("not same normals, root: " + previousNormal + "own: " + averageNormal);
                Vector3 averagePoint = rightHitInfo.point;
                Quaternion targetRotation = Quaternion.FromToRotation(/*Vector3.up*/averageNormal, previousNormal);
                Debug.Log("targetrotation: " + targetRotation);
                Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);
                Debug.Log("finalRotation: " + finalRotation);
                transform.localRotation = Quaternion.Euler(0, 0, -finalRotation.eulerAngles.z);
                Debug.Log("transforming rotation: " + finalRotation.eulerAngles.z);
            }

            else
            {
                Debug.Log("same normals, root: " + previousNormal + "own: " + averageNormal);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;

        Ray rightRay;
        float rayLength = 0.3f;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            //rightRay = new Ray(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * rayLength, Color.green);
            //Debug.DrawRay(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * rayLength, Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * rayLength, Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, rayLength, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }

}
