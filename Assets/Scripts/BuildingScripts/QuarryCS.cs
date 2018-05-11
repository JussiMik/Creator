using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarryCS : Structure
{
    [Space(10)]
    public float gatheredStone;

    [Space(10)]
    public float stoneAmountPerProductionCyclePerRock;
    public float stoneProductionTimeLength;

    private bool rockTimerCollision;
    [HideInInspector]
    public bool rockTimer, stoneCollected;
    [HideInInspector]
    public float originalstoneProductionTimeLength;
    
    [Space(10)]
    public float rocks;
    [SerializeField]
    private float totalRockAmount;

    [Space(10)]
    public float lvl1RockAmount;
    public float lvl2RockAmount;
    public float lvl3RockAmount;

    private bool sanctityPointsGiven;

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalstoneProductionTimeLength = stoneProductionTimeLength;
        stoneCollected = true;
        normalSpeedConstructing = true;

        constructingTimer = true;

        name = "Quarry";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && sanctityPointsGiven == false)
        {
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);

            sanctityPointsGiven = true;

            if (rockTimerCollision == true)
            {
                rockTimer = true;
            } 
        }

        if (rockTimer == true && stoneCollected == true)
        {
            RockTimer();
        }
    }

    public void UpdateValues()
    {
        totalRockAmount = transform.GetChild(0).GetComponent<QuarryRockCollider>().totalRockAmount;
        rockTimerCollision = transform.GetChild(0).GetComponent<QuarryRockCollider>().rockTimerCollision;
    }

    private void RockTimer()
    {
        stoneProductionTimeLength -= Time.deltaTime;

        if (stoneProductionTimeLength <= 0)
        {
            stoneProductionTimeLength = 0;

            rocks = totalRockAmount;

            if (level == 1 && rocks >= lvl1RockAmount)
            {
                rocks = lvl1RockAmount;
            }

            if (level == 2 && rocks >= lvl2RockAmount)
            {
                rocks = lvl2RockAmount;
            }

            if (level == 3 && rocks >= lvl3RockAmount)
            {
                rocks = lvl3RockAmount;
            }

            gatheredStone += stoneAmountPerProductionCyclePerRock * rocks;

            rockTimer = false;
        }

        if (rockTimer == false)
        {
            stoneProductionTimeLength = originalstoneProductionTimeLength;
            rockTimer = true;
            stoneCollected = false;
        }
    }

    public void CollectStone()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.stone += gatheredStone;
        gatheredStone = 0;
        objectiveManager.CheckForCompletedObjectives();
        gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
        stoneCollected = true;
    }
}
