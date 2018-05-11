using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertMonks : ChallengeBase
{


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    void Objective()
    {
        if (objectiveManager.useMonkConvertChallenge == true)
        {
            Debug.Log("Juhhuu");
            if (gameManager.totalMonksConverted == 10 && challengeDone == false)
            {
                Debug.Log("Monjes convertidos");
                challengeDone = true;
            }
        }
    }
}
