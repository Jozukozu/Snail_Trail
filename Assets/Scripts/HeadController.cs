using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            LevelController.baseScore += 10;
            LevelController.energyScore += 20;
            LevelController.energy = 20;

            Destroy(other.gameObject);
        }
    }

}
