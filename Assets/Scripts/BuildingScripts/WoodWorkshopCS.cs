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
    private float originalWoodTime;

    [Space(10)]
    public float gatheredWood;

    [Space(10)]
    public float gatheredWoodAmount;

    [Space(10)]
    [SerializeField]
    private bool woodCollected;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalWoodTime = woodTime;

        woodCollected = true;
        normalSpeedConstructing = true;
        ConstructingStructures();

        gameManager.UseFaith(500);

        name = "Wood workshop";
        type = "Production";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && woodTimerCollision == true)
        {
            woodTimer = true;
        }

        if (woodTimer == true && woodCollected == true)
        {
            WoodTimer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            Debug.Log("Jou koskettaa");

            woodTimerCollision = true;
        }
    }

    public void WoodTimer()
    {
        woodTime -= Time.deltaTime;

        if (woodTime <= 0)
        {
            woodTime = 0;
            gatheredWood += gatheredWoodAmount;
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

        woodCollected = true;
    }
}
