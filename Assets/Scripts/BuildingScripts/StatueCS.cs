using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCS : Structure
{
    public GameManager gameManager;

    private bool addedToList;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        ConstructingStructures();

        addedToList = false;

        faithAmount = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
        }
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        addedToList = true;
        gameManager.faithMultipliers.Add(faithAmount);
    }
}
