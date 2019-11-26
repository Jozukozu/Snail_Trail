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
    public float rayLength;
    public bool touchingGround;
    public bool colliderTouchingGround;
    public float offset;
    private bool childTouchesGround;

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


    void FixedUpdate()
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
            //Rigidbody rigidBody = GetComponent<Rigidbody>();
            //rigidBody.useGravity = false;
            //rigidBody.isKinematic = true;
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
                Debug.Log(this + "not same normals, root: " + previousNormal + "own: " + averageNormal);
                Vector3 averagePoint = rightHitInfo.point;
                Quaternion targetRotation = Quaternion.FromToRotation(/*Vector3.up*/averageNormal, previousNormal);
                //Debug.Log("targetrotation: " + targetRotation);
                Quaternion finalRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, float.PositiveInfinity);
                //Debug.Log("finalRotation: " + finalRotation);
                transform.localRotation = Quaternion.Euler(0, 0, -(finalRotation.eulerAngles.z + offset));
                //Debug.Log("transforming rotation: " + finalRotation.eulerAngles.z);
            }

            else
            {
                //Debug.Log("same normals, root: " + previousNormal + "own: " + averageNormal);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            //Rigidbody rigidBody = GetComponent<Rigidbody>();
            //rigidBody.useGravity = true;
            //rigidBody.isKinematic = false;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            SnailTailController[] allRBs = GetComponentsInChildren<SnailTailController>();
            childTouchesGround = false;
            for (int r = 0; r < allRBs.Length; r++)
            {
                if(allRBs[r].touchingGround)
                {
                    childTouchesGround = true;
                }
            }
            if(!childTouchesGround && !TailEndController.touchingGround)
            {
                if(transform.localEulerAngles.z < 50.0f)
                {
                    transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), Space.Self);
                }
                
            }
            
        }

    }


    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;

        Ray rightRay;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            //rightRay = new Ray(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up);
            //Debug.DrawRay(newPosition, -transform.up, Color.green);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);
            //Debug.DrawRay(transform.localPosition + (transform.right * speed) + transform.localScale.x / 8 * transform.right, -transform.up, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition, -transform.up);
            //Debug.DrawRay(newPosition, -transform.up, Color.green);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition, -transform.up);
            Debug.DrawRay(newPosition, -transform.up * (rayLength + 0.02f), Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, (rayLength + 0.02f), LayerMask.GetMask("Ground"));



        if (rightCast)
        {
            touchingGround = true;
            return true;
        }

        touchingGround = false;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.GetMask("LAYER_NAME"))
        {
            colliderTouchingGround = true;
            Debug.Log("touching ground");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.GetMask("LAYER_NAME"))
        {
            colliderTouchingGround = false;
            Debug.Log("not touching ground");
        }
    }

}
