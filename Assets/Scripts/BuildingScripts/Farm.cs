using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Structure
{
    private bool addedToList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        normalSpeedConstructing = true;
        addedToList = false;
        ConstructingStructures();

        gameManager.UseFaith(levelUpCost);

        name = "Farm";
        type = "Food";
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
        gameManager.farms.Add(gameObject);
        addedToList = true;
    }
}
