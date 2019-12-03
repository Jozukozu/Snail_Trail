using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public static int baseScore;
    public static int energyScore;
    public Text scoreText;
    public Text energyText;
    public static int energy;

    void Start()
    {
        baseScore = 0;
        energyScore = 0;
        energy = 20;
        InvokeRepeating("decreaseEnergy", 5.0f , 1.0f);
    }

    void FixedUpdate()
    {
        energyText.text = "Energy:" + energy.ToString();
        scoreText.text ="Points: " + (baseScore + energyScore).ToString();



    }

    void decreaseEnergy()
    {
        if (energy > 0) 
        {
            energy--;
        }

        else if(energy <= 0 && energyScore > 0)
        {
            energyScore--;
        }
    }
}
