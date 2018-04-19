using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCS : Structure
{
    private bool addedToList;

    public override void Start()
    {
        base.Start();

        gameManager.UseFaith(levelUpCost);

        name = "Statue";
        type = "Faith";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
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
