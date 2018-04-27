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
    public bool woodTimerCollision;
    [SerializeField]
    public float originalWoodTime;

    [Space(10)]
    public float gatheredWood;

    [Space(10)]
    public float woodAmount;

    [Space(10)]
    [SerializeField]
    private bool woodCollected;

    [Space(10)]
    public float trees;
    [SerializeField]
    public float totalTreeAmount;

    [Space(10)]
    public float lvl1TreeAmount;
    public float lvl2TreeAmount;
    public float lvl3TreeAmount;

    private bool sanctityPointsGiven;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalWoodTime = woodTime;

        woodCollected = true;
        normalSpeedConstructing = true;
        ConstructingStructures();

        level = 1;

        name = "Wood workshop";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && sanctityPointsGiven == false)
        {
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);

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

    public void UpdateValues()
    {
        totalTreeAmount = transform.GetChild(0).GetComponent<WorkshopTreeCollider>().totalTreeAmount;
        woodTimerCollision = transform.GetChild(0).GetComponent<WorkshopTreeCollider>().woodTimerCollision;
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

            gatheredWood += woodAmount * trees;

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
