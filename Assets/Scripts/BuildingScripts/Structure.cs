using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public WoodWorkshopCS woodWorkshopCS;
    [HideInInspector]
    public QuarryCS quarryCS;

    public Sprite levelTwoSprite;
    public GameObject clickedBuilding;
    public Vector2 sizeOnGrid;

    [Space(10)]
    public float faithConstructingCost;
    public float woodConstructingCost;
    public float stoneConstructingCost;
    [Space(10)]
    public float faithUpgardeCost;
    public float woodUpgardeCost;
    public float stoneUpgardeCost;

    [Space(10)]
    public float sanctityPointsOnConsturction;
    public float sanctityPointsOnUpgrade;

    [Space(10)]
    private bool constructingTimer;

    [Space(10)]
    public float constructingTime;

    [Space(10)]
    public float normalSpeedConstructingMp;
    public float lowerSpeedConstructingMp1;
    public float lowerSpeedConstructingMp2;
    public float lowerSpeedConstructingMp3;

    [HideInInspector]
    public bool normalSpeedConstructing, lowerSpeedConstructing1, lowerSpeedConstructing2, lowerSpeedConstructing3;

    public bool constructingDone;

    [HideInInspector]
    public bool changedValue;

    [Space(10)]
    public float faithAmountPerProductionCycle;
    public float faithAmountPerProductionCycleUpgraded;
    public float productionCycleLength;
    public float generatedFaith;
    public float maxFaithAmount;

    public float faithMultiplier;
    public bool faithCollected;

    [Space(10)]
    [HideInInspector]
    public bool faithTimer;

    public float originalFaithTargetTime;

    [Space(10)]
    public bool defaultFaithGeneration;
    public bool slowerFaithGeneration1;
    public bool slowerFaithGeneration2;
    public bool slowerFaithGeneration3;

    [Space(10)]
    public int level;
    [HideInInspector]
    public bool lvlChange;
    public int maxLevelAmount;

    [HideInInspector]
    public string  name, type;

    public float WoodConstuctingCost
    {
        get
        {
            return woodConstructingCost;
        }

        set
        {
            woodConstructingCost = value;
        }
    }

    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.UseResources(faithConstructingCost, woodConstructingCost, stoneConstructingCost);
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
        productionCycleLength -= Time.deltaTime;

        if (productionCycleLength <= 0)
        {
            productionCycleLength = 0;
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
            productionCycleLength = originalFaithTargetTime;
            faithTimer = true;
            faithCollected = false;
        }
    }

    //Generate faith
    public void GenerateFaith()
    {
        generatedFaith += faithAmountPerProductionCycle;

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
        gameManager.UseResources(faithUpgardeCost, woodUpgardeCost, stoneUpgardeCost);

        if (level >= 1)
        {
            if (level < maxLevelAmount)
            {
                gameManager.GiveSanctityPoints(sanctityPointsOnUpgrade);
            }

            faithAmountPerProductionCycle = faithAmountPerProductionCycleUpgraded;
            level += 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = levelTwoSprite;

            if (faithAmountPerProductionCycle >= maxFaithAmount && level >= maxLevelAmount)
            {
                faithAmountPerProductionCycle = maxFaithAmount;
                level = maxLevelAmount;
            }
        }

        lvlChange = false;
    }
}
