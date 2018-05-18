using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertMonks : ChallengeBase
{

    public int requiredMonks;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

   public override void Objective()
    {
        if (gameManager.totalMonksConverted == requiredMonks && challengeDone == false)
        {
            Debug.Log("Monjes convertidos");
            challengeDone = true;
        }
    }
}
