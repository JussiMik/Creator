using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMysticPlace : ChallengeBase
{
    MysticPlaceCS mysticPlace;
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
        mysticPlace = GameObject.FindGameObjectWithTag("MysticPlace").GetComponent<MysticPlaceCS>();

        if (mysticPlace.level == 2 && challengeDone == false)
        {
            Debug.Log("Mystic place level TWOOOO");
            challengeDone = true;
        }
    }
}
