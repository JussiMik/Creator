﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleCS : Structure
{
    private bool addedToList;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = productionCycleLength;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;

        constructingTimer = true;

        name = "Temple";
        type = "Faith";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shrine")
        {
            Debug.Log("Koskettaa");
            Destroy(collision.gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown("d"))
        {
            Destroy(gameObject);
        }

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            AddToObjectivesList();
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
    private void AddToObjectivesList()
    {
        objectiveManager.templeList.Add(gameObject);
        objectiveManager.CheckForCompletedObjectives();
    }
}
