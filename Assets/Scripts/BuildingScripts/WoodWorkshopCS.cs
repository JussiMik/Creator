using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWorkshopCS : Structure
{
    public float gatheredWood;

    [Space(10)]
    public float woodAmountPerProductionCyclePerTree;
    public float woodProductionTimeLength;

    private bool woodTimerCollision;
    [HideInInspector]
    public bool woodTimer, woodCollected;
    [HideInInspector]
    public float originalWoodTime;

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

        originalWoodTime = woodProductionTimeLength;
        woodCollected = true;
        normalSpeedConstructing = true;

        constructingTimer = true;

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
        woodProductionTimeLength -= Time.deltaTime;

        if (woodProductionTimeLength <= 0)
        {
            woodProductionTimeLength = 0;

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

            gatheredWood += woodAmountPerProductionCyclePerTree * trees;

            woodTimer = false;
        }

        if (woodTimer == false)
        {
            woodProductionTimeLength = originalWoodTime;
            woodTimer = true;
            woodCollected = false;
        }
    }

    public void CollectWood()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.wood += gatheredWood;
        objectiveManager.CheckForCompletedObjectives();
        gatheredWood = 0;
        gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
        woodCollected = true;
    }
}
