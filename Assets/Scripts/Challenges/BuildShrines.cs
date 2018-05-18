using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildShrines : ChallengeBase
{
    public int requiredShrines;
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
        if (objectiveManager.shrineList.Count == requiredShrines && challengeDone == false)
        {
            Debug.Log("Cuatro santuarios");
            challengeDone = true;
        }
    }
}
