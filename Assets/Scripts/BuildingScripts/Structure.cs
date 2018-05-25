using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    public Sprite underConstructionSprite;
    public Sprite finishedBuildingSprite;
    public Sprite levelTwoSprite;
    [Space(10)]
    [HideInInspector]
    public GameManager gameManager;
    public ObjectiveManager objectiveManager;
    public ObjectiveTracker objectiveTracker;
    [HideInInspector]
    public WoodWorkshopCS woodWorkshopCS;
    [HideInInspector]
    public QuarryCS quarryCS;

    
    public GameObject clickedBuilding;
    public Vector2 sizeOnGrid;

    [Space(10)]
    public float faithConstructingCost;
    public float devotionConstructingCost;
    public float woodConstructingCost;
    public float stoneConstructingCost;

    [Space(10)]
    public float lvl2FaithUpgradeCost;
    public float lvl2DevotionUpgradeCost;
    public float lvl2WoodUpgradeCost;
    public float lvl2StoneUpgradeCost;
    [Space(10)]
    public float lvl3FaithUpgradeCost;
    public float lvl3DevotionUpgradeCost;
    public float lvl3WoodUpgradeCost;
    public float lvl3StoneUpgradeCost;

    [Space(10)]
    public float sanctityPointsOnConsturction;
    public float sanctityPointsOnUpgrade;

    [Space(10)]
    public bool constructingTimer;

    [Space(10)]
    public float constructingTime;

    [HideInInspector]
    public bool normalSpeedConstructing, lowerSpeedConstructing1, lowerSpeedConstructing2, lowerSpeedConstructing3;
    [HideInInspector]
    public bool constructingDone;

    [HideInInspector]
    public bool changedValue;

    [Space(10)]
    public float generatedFaith;
    [Space(10)]
    public float faithAmountPerProductionCycle;
    public float faithAmountPerProductionCycleUpgraded;
    [Space(10)]
    public float productionCycleLength;
    [Space(10)]
    public float maxFaithAmount;
    public float faithMultiplier;
    [HideInInspector]
    public bool faithCollected;

    [Space(10)]
    [HideInInspector]
    public bool faithTimer;

    public float originalFaithTargetTime;

    [Space(10)]
    public int level;
    [HideInInspector]
    public bool lvlChange;
    public int maxLevelAmount;

    [HideInInspector]
    public string name, type;

    public string info;

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

    public bool playAudio;

    public AudioClip placementSound;
    public AudioClip constructingAudio;
    public AudioClip faithCollectSound;

    private void Awake()
    {
        //GetComponent<AudioSource>().playOnAwake = false;
        //GetComponent<AudioSource>().clip = faithCollectSound;
    }

    protected virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
      //  objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
       // objectiveTracker = GameObject.Find("ObjectiveTrackerCanvas").GetComponent<ObjectiveTracker>();

        level = 1;

        gameManager.UseResources(faithConstructingCost, devotionConstructingCost, woodConstructingCost, stoneConstructingCost);
    }

    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            ConstructingTimer(gameManager.constructingTimerMp);
        }

        if (lvlChange == true)
        {
            ChangeLevel();
        }

        if (faithTimer == true && faithCollected == true)
        {
            FaithTimer(gameManager.faithTimerMp);
        }
    }

    private void ChangeTheSprite()
    {

    }

    public void PlayAudio()
    {
        if (playAudio == true)
        {
            GetComponent<AudioSource>().PlayOneShot(placementSound);
            GetComponent<AudioSource>().PlayOneShot(constructingAudio);

            playAudio = false;
        }
    }

    public void ConstructingTimer(float timerMp)
    {
        constructingTime -= Time.deltaTime * timerMp;

        if (constructingTime <= 0)
        {
            constructingTime = 0;
            constructingTimer = false;
            constructingDone = true;
        }
    }

    //Faithtimer before faithgeneration starts
    public virtual void FaithTimer(float timerMp)
    {
        productionCycleLength -= Time.deltaTime * timerMp;

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

            if (level != 1)
            {
                CollectFaith();
            }
        }
    }

    //Generate faith
    public void GenerateFaith()
    {
        generatedFaith += faithAmountPerProductionCycle;

        generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplier);
    }

    //Player can collect generated faith for later use
    public void CollectFaith()
    {
        if (gameManager.devotion > gameManager.devotionChunkDecreaseAmount)
        {
            gameManager.DevotionDecreaseChunk();


            gameManager.faith += generatedFaith;
            generatedFaith = 0f;
            gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;

            GetComponent<AudioSource>().PlayOneShot(faithCollectSound);

            faithCollected = true;
        }
    }

    //Change structures level
    public virtual void ChangeLevel()
    {
        AddResourceCostAmountOnLevelUp();
        gameManager.UseResources(lvl2FaithUpgradeCost, lvl2DevotionUpgradeCost, lvl2WoodUpgradeCost, lvl2StoneUpgradeCost);

        if (level >= 1)
        {
            if (level < maxLevelAmount)
            {
                gameManager.GiveSanctityPoints(sanctityPointsOnUpgrade);
            }

            faithAmountPerProductionCycle = faithAmountPerProductionCycleUpgraded;
            level += 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = levelTwoSprite;

            if (faithAmountPerProductionCycle >= maxFaithAmount)
            {
                faithAmountPerProductionCycle = maxFaithAmount;
            }

            if (level >= maxLevelAmount)
            {
                level = maxLevelAmount;
            }
        }

        lvlChange = false;
    }

    void AddResourceCostAmountOnLevelUp()
    {
        if (level == 2)
        {
            lvl2FaithUpgradeCost = lvl3FaithUpgradeCost;
            lvl2DevotionUpgradeCost = lvl3DevotionUpgradeCost;
            lvl2WoodUpgradeCost = lvl3WoodUpgradeCost;
            lvl2StoneUpgradeCost = lvl3StoneUpgradeCost;
        }
    }
}
