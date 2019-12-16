using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
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

