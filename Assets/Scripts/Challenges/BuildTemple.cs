using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTemple : ChallengeBase
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
        if (objectiveManager.templeList.Count == 1 && challengeDone == false)
        {
            Debug.Log("Uno templo");
            challengeDone = true;
        }
    }
}
