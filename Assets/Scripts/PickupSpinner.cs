using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpinner : MonoBehaviour
{
    //This code spins the pick up object.
    void Update()
    {
        transform.Rotate(new Vector3(0, 40, 0) * Time.deltaTime, Space.World);
    }
}
