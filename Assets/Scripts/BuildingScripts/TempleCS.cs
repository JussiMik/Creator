using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleCS : Structure
{
    private bool addedToList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = faithTargetTime;

        ConstructingStructures();

        addedToList = false;
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

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
}
