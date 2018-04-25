using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarryCS : Structure
{
    [Space(10)]
    public bool rockTimer;
    public float rockTime;
    [SerializeField]
    private bool rockTimerCollision;
    [SerializeField]
    public float originalRockTime;

    [Space(10)]
    public float gatheredStone;

    [Space(10)]
    public float gatheredStoneAmount;

    [Space(10)]
    public bool stoneCollected;

    [Space(10)]
    public float rocks;
    [SerializeField]
    private float totalRockAmount;

    [Space(10)]
    public float lvl1RockAmount;
    public float lvl2RockAmount;
    public float lvl3RockAmount;

    private bool sanctityPointsGiven;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        normalSpeedConstructing = true;
        ConstructingStructures();

        originalRockTime = rockTime;

        stoneCollected = true;

        gameManager.UseFaith(constructingCost);

        name = "Quarry";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && sanctityPointsGiven == false)
        {
            gameManager.GiveSanctityPoints(sanctityPointAmount);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            totalRockAmount++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            rockTimerCollision = true;
        }
    }

    private void RockTimer()
    {
        rockTime -= Time.deltaTime;

        if (rockTime <= 0)
        {
            rockTime = 0;

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

            gatheredStone += gatheredStoneAmount * rocks;

            rockTimer = false;
        }

        if (rockTimer == false)
        {
            rockTime = originalRockTime;
            rockTimer = true;
            stoneCollected = false;
        }
    }

    public void CollectStone()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.stone += gatheredStone;
        gatheredStone = 0;
        gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
        stoneCollected = true;
    }
}
