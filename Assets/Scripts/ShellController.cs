using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{

    //This code controls the shell. It makes the shell move when the body is "hidden inside" and shell to follow the body in right place when the body "is out of the shell".


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
    private Vector3[] bonePosition = new Vector3[8];
    private GameObject[] snailBones = new GameObject[8];


    void Start()
    {
        //saving a first state of all the snail tail pieces. This helps resetting them in the right way after coming out of shell.
        snailBones = GameObject.FindGameObjectsWithTag("Bone Object"); 
        for (int i = 1; i < snailBones.Length; i++)
        {
            bonePosition[i] = snailBones[i].transform.localPosition;
        }
        shellMode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shellModeKey", "Space"));
        Debug.Log(shellMode);
    }


    void FixedUpdate()
    {
        if (moveHorizontal > 0)
        {
            SnailBodyController.facingRight = true;
        }
        else if (moveHorizontal < 0)
        {
            SnailBodyController.facingRight = false;
        }

        //Code to move shell when in shell mode.
        if (shellVisible)
        {
            shell.GetComponent<SphereCollider>().enabled = true;
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            snailBody.SetActive(false);
            if (SnailBodyController.facingRight)
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
        //Else we make sure shell keeps up in the right place with the body.
        else
        {
            Vector3 shellPoint = bone.transform.position + transform.TransformVector(0.0f, bone.transform.localPosition.y + 0.5f, 0.0f);
            shell.GetComponent<SphereCollider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;
            shell.transform.rotation = bone.transform.rotation;
            shell.transform.position = shellPoint;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(shellMode))
        {
            shellVisible = !shellVisible;
            if (!shellVisible)
            {
                snailBody.SetActive(true);
                ResetSnail();
            }
            Debug.Log("space pressed");
        }
    }

    //Resetting snail after coming out of shell.
    private void ResetSnail()
    {
        for (int i = 0; i < snailBones.Length; i++)
        {
            Debug.LogError(snailBones[i]);
        }
        GameObject root = GameObject.FindGameObjectWithTag("Root");
        Vector3 spawnPoint = root.transform.position + new Vector3(0.0f, 0.2f, 0.0f);
        root.transform.position = spawnPoint;
        if (!SnailBodyController.facingRight)
        {
            root.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            root.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        for (int i = 1; i < snailBones.Length; i++)
        {
            snailBones[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            snailBones[i].transform.localPosition = bonePosition[i];
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
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            onGround = false;
        }
    }

    //Code for when pick up is touched. This destroys the pick up object and adds to the score and energy.
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
