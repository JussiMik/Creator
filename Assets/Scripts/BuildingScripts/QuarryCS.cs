﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarryCS : Structure
{
    [Space(10)]
    public bool rockTimer;
    public float rockTime;
    private float originalRockTime;

    [Space(10)]
    public float gatheredStone;
    public float gatheredStoneAmount;

    [Space(10)]
    public bool stoneCollected;

    public override void Start()
    {
        base.Start();

        originalRockTime = rockTime;

        stoneCollected = true;

        gameManager.UseFaith(500);

        name = "Quarry";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (rockTimer == true && stoneCollected == true)
        {
            RockTimer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock" && constructingDone == true)
        {
            rockTimer = true;
        }
    }

    private void RockTimer()
    {
        rockTime -= Time.deltaTime;

        if (rockTime <= 0)
        {
            rockTime = 0;
            gatheredStone += gatheredStoneAmount;

            stoneCollected = false;
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

        stoneCollected = true;
    }
}
