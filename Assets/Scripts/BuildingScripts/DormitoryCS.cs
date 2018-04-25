using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormitoryCS : Structure
{
    private bool addedToList;

    protected override void Start()
    {
        base.Start();

        normalSpeedConstructing = true;
        addedToList = false;
        ConstructingStructures();

        gameManager.UseFaith(constructingCost);

        name = "Dormitory";
        type = "Dormitory";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            //AddToList();
            gameManager.GiveSanctityPoints(sanctityPointAmount);

            addedToList = true;
        }
    }
}
