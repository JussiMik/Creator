﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCS : Structure
{
    private bool addedToList;

    private void Awake()
    {
        playAudio = true;
    }

    protected override void Start()
    {
        base.Start();

        originalFaithTargetTime = productionCycleLength;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;

        constructingTimer = true;

        name = "Statue";
        type = "Faith";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
            faithTimer = true;
        }
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
}
