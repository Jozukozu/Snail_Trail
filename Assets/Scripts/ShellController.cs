using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float speed;
    private KeyCode shellMode;
    private bool shellVisible = false;
    public GameObject shell;
    public GameObject snail;
    public GameObject snailBody;
    private bool onGround;
    public GameObject bone;
    private float moveHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        shellMode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shellModeKey", "Space"));
        Debug.Log(shellMode);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (shellVisible)
        {
            shell.GetComponent<SphereCollider>().enabled = true;
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            snailBody.SetActive(false);
            if(SnailBodyController.facingRight)
            {
                snail.transform.position = shell.transform.position + new Vector3(0.5f, -0.3f, 0.0f);
            }
            else if (!SnailBodyController.facingRight)
            {
                snail.transform.position = shell.transform.position + new Vector3(-0.5f, -0.3f, 0.0f);
            }
            if (onGround)
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
                RaycastHit hitInfo;
                if (!GetRaycastForwardAtNewPosition(movement, out hitInfo))
                {
                    rigidBody.AddForce(movement * speed);
                }
                else
                {
                    rigidBody.AddForce(movement * speed * 0.1f);
                }
            }
        }
        else
        {
            Vector3 shellPoint = bone.transform.position + transform.TransformVector(0.0f, bone.transform.localPosition.y + 0.5f, 0.0f);
            shell.GetComponent<SphereCollider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
            snailBody.SetActive(true);
            shell.transform.rotation = bone.transform.rotation;
            shell.transform.position = shellPoint;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(shellMode))
        {
            shellVisible = !shellVisible;
            if(!shellVisible)
            {
                resetSnail();
            }
            Debug.Log("space pressed");
        }
        if (moveHorizontal > 0)
        {
            SnailBodyController.facingRight = true;
        }
        else if(moveHorizontal < 0)
        {
            SnailBodyController.facingRight = false;
        }
    }

    private void resetSnail()
    {
        GameObject[] snailBones = GameObject.FindGameObjectsWithTag("Bone Object");
        Vector3 spawnPoint = snailBones[0].transform.position + snailBones[0].transform.TransformVector(0.0f, snailBones[0].transform.localPosition.y + 1f, 0.0f);
        snailBones[0].transform.position = spawnPoint;
        if (!SnailBodyController.facingRight)
        {
            snailBones[0].transform.localRotation = Quaternion.Euler(0, 180, 0);
            for (int i = 1; i < snailBones.Length; i++)
            {
                snailBones[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < snailBones.Length; i++)
            {
                snailBones[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private bool GetRaycastForwardAtNewPosition(Vector3 movementDirection, out RaycastHit rightHitInfo)
    {
        Vector3 newPosition = transform.position;

        Ray rightRay;
        float rayLength = 1f;

        if (movementDirection.x > 0)
        {
            rightRay = new Ray(newPosition - Vector3.up * 0.5f, Vector3.right);
            Debug.DrawRay(newPosition - Vector3.up * 0.5f, Vector3.right * rayLength, Color.green);

        }
        else if (movementDirection.x < 0)
        {
            rightRay = new Ray(newPosition - Vector3.up * 0.5f, -Vector3.right);
            Debug.DrawRay(newPosition - Vector3.up * 0.5f, -Vector3.right * rayLength, Color.green);
        }
        else
        {
            rightRay = new Ray(newPosition - Vector3.up * 0.5f, Vector3.right);
            Debug.DrawRay(newPosition - Vector3.up * 0.5f, -Vector3.right * rayLength, Color.green);
        }
        bool rightCast = Physics.Raycast(rightRay, out rightHitInfo, rayLength, LayerMask.GetMask("Ground"));


        if (rightCast)
        {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            onGround = true;
            Debug.Log("on ground");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            onGround = true;
            Debug.Log("on ground");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            onGround = false;
            Debug.Log("not on ground");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            LevelController.baseScore += 10;
            LevelController.energyScore += 20;
            LevelController.energy = 20;
            LevelController.eat = true;

            Destroy(other.transform.parent.gameObject);
        }
    }
}
