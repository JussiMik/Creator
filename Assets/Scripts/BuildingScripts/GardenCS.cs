using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenCS : Structure
{
    private bool addedToList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        normalSpeedConstructing = true;
        addedToList = false;
        ConstructingStructures();

        gameManager.UseFaith(constructingCost);

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
