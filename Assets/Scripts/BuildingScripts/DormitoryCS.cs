﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormitoryCS : Structure
{
    private bool addedToList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

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
    }
}
