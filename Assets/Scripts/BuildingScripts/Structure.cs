﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameManager gameManager;
    public WoodWorkshopCS woodWorkshopCS;
    public QuarryCS quarryCS;

    public GameObject clickedBuilding;

    public Vector2 sizeOnGrid;
    [Space(10)]
    public float sanctityPointsOnConsturction;
    public float sanctityPointsOnLevelup;

    [Space(10)]
    [SerializeField]
    private bool constructingTimer;

    [Space(10)]
    public float constructingTime;

    [Space(10)]
    public float faithCost;
    public float woodCost;
    public float stoneCost;

    [Space(10)]
    public float normalSpeedConstructingMp;
    public float lowerSpeedConstructingMp1;
    public float lowerSpeedConstructingMp2;
    public float lowerSpeedConstructingMp3;

    [Space(10)]
    public bool normalSpeedConstructing;
    public bool lowerSpeedConstructing1;
    public bool lowerSpeedConstructing2;
    public bool lowerSpeedConstructing3;

    public bool constructingDone;

    public bool changedValue;

    [Space(10)]
    public float generatedFaith;

    [Space(10)]
    public float faithAmount;
    public float maxFaithAmount;

    public float faithMultiplier;

    public bool faithCollected;

    public bool faithTimer;
    public float faithTargetTime;
    public float originalFaithTargetTime;

    [Space(10)]
    public bool defaultFaithGeneration;
    public bool slowerFaithGeneration1;
    public bool slowerFaithGeneration2;
    public bool slowerFaithGeneration3;

    [Space(10)]
    public int level;
    public bool lvlChange;
    public float lvlUpFaithIncrease;
    public int maxLevelAmount;
    public float faithLevelUpCost;
    public float woodLevelUpCost;
    public float stoneLevelUpCost;

    public string name;
    public string type;



    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.UseResources(faithCost, woodCost, stoneCost);
    }

    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            ConstructingTimer();
        }

        if (gameManager.devotionDecrease1 == true)
        {
            lowerSpeedConstructing1 = true;
        }
        if (gameManager.devotionDecrease2 == true)
        {
            lowerSpeedConstructing2 = true;
        }
        if (gameManager.devotionDecrease3 == true)
        {
            lowerSpeedConstructing3 = true;
        }

        if (lvlChange == true)
        {
            ChangeLevel();
        }

        if (faithTimer == true && faithCollected == true)
        {
            FaithTimer();
        }
    }

    public void ConstructingStructures()
    {
        gameManager.DevotionDecreaseChunk();

        constructingTimer = true;
    }

    public void ConstructingTimer()
    {
        if (normalSpeedConstructing == true)
        {
            constructingTime -= Time.deltaTime * normalSpeedConstructingMp;
        }

        if (lowerSpeedConstructing1 == true)
        {
            normalSpeedConstructing = false;
            constructingTime -= Time.deltaTime * lowerSpeedConstructingMp1;
        }

        if (lowerSpeedConstructing2 == true)
        {
            normalSpeedConstructing = false;
            lowerSpeedConstructing1 = false;
            constructingTime -= Time.deltaTime * lowerSpeedConstructingMp2;
        }

        if (lowerSpeedConstructing3 == true)
        {
            normalSpeedConstructing = false;
            lowerSpeedConstructing2 = false;
            constructingTime -= Time.deltaTime;
        }

        if (constructingTime <= 0)
        {
            constructingTime = 0f;
            constructingTimer = false;
            constructingDone = true;
        }

    }

    //Faithtimer before faithgeneration starts
    public virtual void FaithTimer()
    {
        faithTargetTime -= Time.deltaTime;

        if (faithTargetTime <= 0)
        {
            faithTargetTime = 0;
            TimerEnd();
        }
    }

    //Timer ends and starts faith generation
    public void TimerEnd()
    {
        faithTimer = false;

        GenerateFaith();

        if (faithTimer == false)
        {
            faithTargetTime = originalFaithTargetTime;
            faithTimer = true;
            faithCollected = false;
        }
    }

    //Generate faith
    public void GenerateFaith()
    {
        generatedFaith += faithAmount;

        if (defaultFaithGeneration == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplier);
        }

        if (slowerFaithGeneration1 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow1);
        }

        if (slowerFaithGeneration2 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow2);
        }

        if (slowerFaithGeneration3 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow3);
        }
    }

    //Player can collect generated faith for later use
    public void CollectFaith()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.faith += generatedFaith;
        generatedFaith = 0f;
        gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
        faithCollected = true;
    }

    //Change structures level
    public void ChangeLevel()
    {
        gameManager.UseResources(faithLevelUpCost, woodLevelUpCost, stoneLevelUpCost);

        if (level >= 1)
        {
            if (level < maxLevelAmount)
            {
                gameManager.GiveSanctityPoints(sanctityPointsOnLevelup);
            }

            faithAmount += lvlUpFaithIncrease;
            level += 1;

            if (faithAmount >= maxFaithAmount && level >= maxLevelAmount)
            {
                faithAmount = maxFaithAmount;
                level = maxLevelAmount;
            }
        }

        lvlChange = false;
    }
}
