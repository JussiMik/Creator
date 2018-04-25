using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWorkshopCS : Structure
{
    [Space(10)]
    public bool woodTimer;
    public float woodTime;
    [SerializeField]
    private bool woodTimerCollision;
    [SerializeField]
    public float originalWoodTime;

    [Space(10)]
    public float gatheredWood;

    [Space(10)]
    public float gatheredWoodAmount;

    [Space(10)]
    [SerializeField]
    private bool woodCollected;

    [Space(10)]
    public float trees;
    [SerializeField]
    private float totalTreeAmount;

    [Space(10)]
    public float lvl1TreeAmount;
    public float lvl2TreeAmount;
    public float lvl3TreeAmount;

    private bool sanctityPointsGiven;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalWoodTime = woodTime;

        woodCollected = true;
        normalSpeedConstructing = true;
        ConstructingStructures();

        gameManager.UseFaith(constructingCost);

        level = 1;

        name = "Wood workshop";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && sanctityPointsGiven == false)
        {
            gameManager.GiveSanctityPoints(sanctityPointAmount);

            sanctityPointsGiven = true;

            if (woodTimerCollision == true)
            {
                woodTimer = true;
            }
        }

        if (woodTimer == true && woodCollected == true)
        {
            WoodTimer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            totalTreeAmount++;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            woodTimerCollision = true;
        }
    }

    public void WoodTimer()
    {
        woodTime -= Time.deltaTime;

        if (woodTime <= 0)
        {
            woodTime = 0;

            trees = totalTreeAmount;

            if (level == 1 && trees >= lvl1TreeAmount)
            {
                trees = lvl1TreeAmount;
            }

            if (level == 2 && trees >= lvl2TreeAmount)
            {
                trees = lvl2TreeAmount;
            }

            if (level == 3 && trees >= lvl3TreeAmount)
            {
                trees = lvl3TreeAmount;
            }

            gatheredWood += gatheredWoodAmount * trees;

            woodTimer = false;
        }

        if (woodTimer == false)
        {
            woodTime = originalWoodTime;
            woodTimer = true;
            woodCollected = false;
        }
    }

    public void CollectWood()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.wood += gatheredWood;
        gatheredWood = 0;
        gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
        woodCollected = true;
    }
}
