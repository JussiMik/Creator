using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormitoryCS : Structure
{
    private bool addedToList;
    [Space(10)]
    public int monkSlotsPerDormitory;
    protected override void Start()
    {
        base.Start();

        normalSpeedConstructing = true;
        addedToList = false;
        ConstructingStructures();

        name = "Dormitory";
        type = "Dormitory";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            //AddToList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
            gameManager.monkSlots += monkSlotsPerDormitory;
            addedToList = true;
        }


    }

}
