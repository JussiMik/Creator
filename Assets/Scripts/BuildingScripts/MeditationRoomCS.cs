using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationRoomCS : Structure
{
    private bool addedToList;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = faithTargetTime;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;
        ConstructingStructures();

        name = "Meditation room";
        type = "Devotion";
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

    public void AddToList()
    {
        gameManager.meditationRooms.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;

    }
}
