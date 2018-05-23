using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTemple : ChallengeBase
{
    public int requiredTemples;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public override void Objective()
    {
        if (objectiveManager.templeList.Count == requiredTemples && challengeDone == false)
        {
            Debug.Log("Uno templo");
            challengeDone = true;
        }
    }
}
