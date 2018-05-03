using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Structure
{
    private bool addedToList;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        normalSpeedConstructing = true;
        addedToList = false;

        constructingTimer = true;

        name = "Farm";
        type = "Food";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
        }
    }

    public void AddToList()
    {
        gameManager.farms.Add(gameObject);
        addedToList = true;
    }
}
