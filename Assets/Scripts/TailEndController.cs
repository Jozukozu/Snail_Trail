using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailEndController : MonoBehaviour
{
    public static bool touchingGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit rightHitInfo;
        if (GetRaycastBackAtNewPosition(out rightHitInfo))
        {
            touchingGround = true;
            //Debug.Log("touching ground");
        }
        else
        {
            touchingGround = false;
            //Debug.Log("not touching ground");
        }
    }

    private bool GetRaycastBackAtNewPosition(/*Vector3 movementDirection,*/ out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;
        float rayLength = 0.15f;
        Ray rightRay;

        //if (movementDirection.x > 0)
        //{
        //    rightRay = new Ray(newPosition, -transform.right);
        //    //rightRay = new Ray(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.right);
        //    //Debug.DrawRay(newPosition, -transform.right, Color.green);
        //    Debug.DrawRay(newPosition, -transform.right * rayLength, Color.green);
        //    //Debug.DrawRay(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.right, Color.green);

        //}
        //else if (movementDirection.x < 0)
        //{
        //    rightRay = new Ray(newPosition, -transform.right);
        //    //Debug.DrawRay(newPosition, -transform.right, Color.green);
        //    Debug.DrawRay(newPosition, -transform.right * rayLength, Color.green);
        //}
        //else
        //{
            rightRay = new Ray(newPosition, -transform.right);
            Debug.DrawRay(newPosition, -transform.right * rayLength, Color.green);
        //}
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, rayLength, LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            return true;
        }


        return false;
    }
}
