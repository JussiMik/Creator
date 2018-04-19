using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenCS : Structure
{
    private bool addedToList;

    public override void Start()
    {
        base.Start();

        gameManager.UseFaith(levelUpCost);

        name = "Garden";
        type = "Devotion";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
        }
    }

    public void AddToList()
    {
        gameManager.gardens.Add(gameObject);
        addedToList = true;
    }
}
