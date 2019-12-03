using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : MonoBehaviour
{

    public static int baseScore;
    public static int energyScore;
    private int energy;

    void Start()
    {
        baseScore = 0;
        energyScore = 0;
        energy = 100;
    }

    void FixedUpdate()
    {

        energy--;




    }
}
