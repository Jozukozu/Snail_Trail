using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailEndController : MonoBehaviour
{

    //This code gives some custom rules for the end of the tail. This is to help prevent the tail end from going inside ground.

    public static bool touchingGround;


    void FixedUpdate()
    {
        RaycastHit rightHitInfo;
        if (GetRaycastBackAtNewPosition(out rightHitInfo))
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }
    }

    //Draws raycast to point out of the tail end.
    private bool GetRaycastBackAtNewPosition(out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;
        float rayLength = 0.15f;
        Ray rightRay;

   
            rightRay = new Ray(newPosition, -transform.right);
            Debug.DrawRay(newPosition, -transform.right * rayLength, Color.green);

        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, rayLength, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }
}
