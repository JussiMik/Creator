using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCS : Structure
{
    private bool addedToList;

    protected virtual void Start()
    {
        base.Start();

        originalFaithTargetTime = faithTargetTime;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;
        ConstructingStructures();

        gameManager.UseFaith(constructingCost);

        name = "Statue";
        type = "Faith";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointAmount);
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
