using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationRoomCS : Structure
{
    private bool addedToList;

    public override void Start()
    {
        base.Start();

        gameManager.UseFaith(levelUpCost);

        name = "Meditation room";
        type = "Devotion";
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

    public void AddToList()
    {
        gameManager.meditationRooms.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;

    }
}
