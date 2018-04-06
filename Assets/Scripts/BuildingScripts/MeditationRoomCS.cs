using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationRoomCS : Structure
{
    public GameManager gameManager;

    private bool addedToList;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        ConstructingStructures();

        addedToList = false;
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

    public void AddToList()
    {
        gameManager.devotionBuildings.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        //gameManager.faithAmounts.Add(faithAmount);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;

    }
}
