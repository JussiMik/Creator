using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameManager gameManager;
    public WoodWorkshopCS woodWorkshopCS;
    public QuarryCS quarryCS;

    public GameObject clickedBuilding;

    [Space(10)]
    public float sanctityPointAmount;

    [Space(10)]
    [SerializeField]
    private bool constructingTimer;

    [Space(10)]
    public float constructingTime;

    [Space(10)]
    public float constructingCost;

    [Space(10)]
    public float constructingTimeSlow1;
    public float constructingTimeSlow2;
    public float constructingTimeSlow3;

    public bool normalSpeedConstructing;
    public bool changedValue;
    public bool lowerSpeedConstructing1;
    public bool lowerSpeedConstructing2;
    public bool lowerSpeedConstructing3;

    public bool constructingDone;

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
    public float levelUpCost;

    public string name;
    public string type;
    

    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            ConstructingTimer();
        }

        if (gameManager.devotionDecreaseMp1 == true)
        {   
            lowerSpeedConstructing1 = true;
        }
        if (gameManager.devotionDecreaseMp2 == true)
        {
            lowerSpeedConstructing2 = true;
        }
        if (gameManager.devotionDecreaseMp3 == true)
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
            constructingTime -= Time.deltaTime * 4;
        }

        if (lowerSpeedConstructing1 == true)
        {
            normalSpeedConstructing = false;
            constructingTime -= Time.deltaTime * 3;
        }

        if (lowerSpeedConstructing2 == true)
        {
            normalSpeedConstructing = false;
            lowerSpeedConstructing1 = false;
            constructingTime -= Time.deltaTime * 2;
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
        gameManager.UseFaith(levelUpCost);

        if (level >= 1)
        {
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
