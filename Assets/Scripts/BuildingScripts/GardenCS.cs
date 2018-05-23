using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenCS : Structure
{
    private bool addedToList;

    private void Awake()
    {
        playAudio = true;
    }

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        normalSpeedConstructing = true;
        addedToList = false;

        constructingTimer = true;

        name = "Garden";
        type = "Devotion";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            objectiveManager.gardenList.Add(gameObject);
            objectiveManager.CheckForCompletedObjectives();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
        }
    }

    public void AddToList()
    {
        gameManager.gardens.Add(gameObject);
        gameManager.CheckFarmCount();
        addedToList = true;
    }
}
