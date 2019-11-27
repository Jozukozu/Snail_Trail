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
    private bool facingRight;

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
            if(facingRight)
            {
                snail.transform.position = shell.transform.position + new Vector3(0.8f, -0.4f, 0.0f);
            }
            else
            {
                snail.transform.position = shell.transform.position + new Vector3(-0.8f, -0.4f, 0.0f);
            }
            if (onGround)
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
                rigidBody.AddForce(movement * speed);
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
            facingRight = true;
        }
        else if(moveHorizontal < 0)
        {
            facingRight = false;
        }
    }

    private void resetSnail()
    {
        GameObject[] snailBones = GameObject.FindGameObjectsWithTag("Bone Object");
        if (SnailBodyController.facingLeft || moveHorizontal < 0)
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
}
