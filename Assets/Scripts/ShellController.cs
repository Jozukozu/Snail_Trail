using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rigidBody.AddForce(movement * speed);
        UpdatePlayerTransform(movement);
    }

    private void UpdatePlayerTransform(Vector3 movementDirection)
    {
        RaycastHit hitInfo;

        if (GetRaycastDownAtNewPosition(movementDirection, out hitInfo))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, float.PositiveInfinity);

            transform.rotation = finalRotation;
            transform.position = hitInfo.point + hitInfo.normal * .5f;
        }
    }

    private bool GetRaycastDownAtNewPosition(Vector3 movementDirection, out RaycastHit hitInfo)
    {
        Vector3 newPosition = transform.position;
        Ray ray = new Ray(transform.position + movementDirection * speed, -transform.up);

        if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, LayerMask.GetMask("Ground")))
        {
            return true;
        }

        return false;
    }
}
