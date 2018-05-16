using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Structure structure;
    GameObject resourceTracker;

    [Space(10)]
    public float sanctity;
    [SerializeField]
    private float sanctityPoints;
    public float maxSanctityPoints;

    [Space(10)]
    public float requiredAmountForLvlUp;
    public float requiredAmountIncrease;

    [Space(10)]
    public float faith;
    public float wood;
    public float stone;
    public float devotion;

    [Space(10)]
    public bool devotionDecrease;
    [Space(10)]
    public float maxDevotionAmount;
    public float minDevotionAmountCollecting;

    [Space(10)]
    public float devotionChunkDecreaseAmount;

    [SerializeField]
    [Space(10)]
    private float devotionDecreaseMp;
    [SerializeField]
    [Space(10)]
    private float devotionIncreaseMp;
    [Space(10)]
    public float constructingTimerMp;
    [Space(10)]
    public float faithTimerMp;

    [Space(10)]
    public bool devotionIncrease;
    [Space(10)]
    public float devotionChunkIncreaseAmount;
    [Space(10)]
    public float devotionChunkIncreaseAfterKilledMonks;
    [Space(10)]
    public float monkFaithMultiplier;
    [Space(10)]
    public float monkProductionMultiplier;

    [Space(10)]
    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> gardens = new List<GameObject>();
    public List<GameObject> meditationRooms = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<float> faithMultipliers = new List<float>();
    public List<GameObject> convertableMonks = new List<GameObject>();

    FaithText faithResourceTracker;
    DevotionText devotionResourceTracker;
    WoodText woodResourceTracker;
    StoneText stoneResourceTracker;
    MonkText monkResourceTracker;

    public int monkSlots;
    public int totalMonksConverted;

    [SerializeField]
    private float numberOfMonksToKill;

    [Space(10)]
    public float goodMonkAndFarmRatio;
    public float badMonkAndFarmRatio75;
    public float badMonkAndFarmRatio50;
    public float badMonkAndFarmRatio25;
    [Space(10)]
    public float defaultDevotionDecreaseMp;
    public float devotionDecreaseMp1;
    public float devotionDecreaseMp2;
    public float devotionDecreaseMp3;
    [Space(10)]
    public float defaultDevotionIncreaseMp;
    [Space(10)]
    public float defaultConstructingTimerMp;
    public float constructingTimerMp1;
    public float constructingTimerMp2;
    public float constructingTimerMp3;
    [Space(10)]
    public float defaultFaithTimerMp;
    public float faithTimerMp1;
    public float faithTimerMp2;
    public float faithTimerMp3;

    public float[] devotionIncreaseRatios = new float[10];
    public float[] devotionIncreaseMultipliers = new float[10];

    public float numberOfMonksAndGardens;

    private void Awake()
    {
        GetResourceObjects();
    }

    void Start()
    {
        structure = gameObject.GetComponent<Structure>();
        resourceTracker = GameObject.Find("MonkText");
        minDevotionAmountCollecting = devotionChunkDecreaseAmount;
    }

    void Update()
    {
        if (devotionDecrease == true)
        {
            DevotionDecrease(devotionDecreaseMp);
        }

        if (devotionIncrease == true)
        {
            DevotionIncrease(devotionIncreaseMp);
        }

        monks.RemoveAll(list_item => list_item == null);


    }

    //Use collected faith for constructing and leveling up buildings
    public void UseResources(float faithUseAmount, float devotionUseAmount, float woodUseAmount, float stoneUseAmount)
    {
        faith -= faithUseAmount;
        faithResourceTracker.UpdateFaith();

        if (faith <= 0)
        {
            faith = 0;
        }

        devotion -= devotionUseAmount;
        //devotionResourceTracker.UpdateDevotion();

        if (devotion <= 0)
        {
            devotion = 0;
        }

        wood -= woodUseAmount;
        woodResourceTracker.UpdateWood();

        if (wood <= 0)
        {
            wood = 0;
        }

        stone -= stoneUseAmount;
        stoneResourceTracker.UpdateStone();

        if (stone <= 0)
        {
            stone = 0;
        }
    }

    //Devotion decreases slowly
    public void DevotionDecrease(float decreaseMp)
    {
        devotion -= Time.deltaTime * decreaseMp;
        structure.changedValue = false;

        if (devotion <= 0)
        {
            devotion = 0;
            KillSomeMonks();
        }
    }

    //Devotion decreases in chunks
    public void DevotionDecreaseChunk()
    {
        devotion -= devotionChunkDecreaseAmount;

        if (devotion <= 0)
        {
            devotion = 0;
            KillSomeMonks();
        }
    }

    //Devotion increases slowly
    void DevotionIncrease(float increaseAmount)
    {
        devotion += Time.deltaTime * increaseAmount;

        /*
        if (devotionIncrease1 == true)
        {
            devotion += Time.deltaTime * devotionIncreaseMp1;
        }
        */

        if (devotion >= maxDevotionAmount)
        {
            devotion = maxDevotionAmount;
        }
    }

    //Devotion increases in chunks
    public void DevotionIncreaseChunk(float chunkAmount)
    {
        devotion += chunkAmount;

        if (devotion >= maxDevotionAmount)
        {
            devotion = maxDevotionAmount;
        }
    }

    //Player gets sanctity points
    public void GiveSanctityPoints(float amount)
    {
        sanctityPoints += amount;

        if (sanctityPoints >= requiredAmountForLvlUp)
        {
            ChangePlayerLvl();

            requiredAmountForLvlUp += requiredAmountIncrease;
        }

        if (sanctityPoints >= maxSanctityPoints)
        {
            sanctityPoints = maxSanctityPoints;
        }
    }

    //Changes players level
    private void ChangePlayerLvl()
    {
        sanctity++;
    }

    //Goes through a list of monks and destroys all of the excess ones, in Update() function destroys null objects from the list
    void KillSomeMonks()
    {
        numberOfMonksToKill = monks.Count - (farms.Count * goodMonkAndFarmRatio);

        float f = 0;

        foreach (GameObject monksToKill in monks)
        {
            if (f == numberOfMonksToKill) break;
            {
                Destroy(monksToKill.gameObject);

                monkResourceTracker.UpdateMonkCount();

                f++;
            }
        }
        DevotionIncreaseChunk(devotionChunkIncreaseAfterKilledMonks);
    }

    void GetResourceObjects()
    {
        monkResourceTracker = GameObject.Find("MonkText").GetComponent<MonkText>();
        faithResourceTracker = GameObject.Find("FaithText").GetComponent<FaithText>();
        woodResourceTracker = GameObject.Find("WoodText").GetComponent<WoodText>();
        stoneResourceTracker = GameObject.Find("StoneText").GetComponent<StoneText>();
    }

    //Checks if there's enough farms per monk on the map
    public void CheckFarmCount()
    {
        if (farms.Count == 0)
        {
            devotionDecrease = true;

            if (monks.Count == 0)
            {
                devotionDecrease = false;
            }
        }

        if (monks.Count > 0 && farms.Count > 0)
        {
            if (monks.Count / farms.Count <= goodMonkAndFarmRatio)
            {
                devotionIncreaseMp = defaultDevotionIncreaseMp;
                constructingTimerMp = defaultConstructingTimerMp;
                faithTimerMp = defaultFaithTimerMp;
                UpdateFaithMultiplierForProductionBar();
                devotionDecrease = false;
                devotionIncrease = true;

                if (gardens.Count > 0 || meditationRooms.Count > 0)
                {
                    numberOfMonksAndGardens = monks.Count / gardens.Count;

                    for (int i = 0; i < devotionIncreaseRatios.Length; i++)
                    {
                        if (devotionIncreaseRatios[i] <= numberOfMonksAndGardens)
                        {
                            devotionIncreaseMp = devotionIncreaseMultipliers[i];
                        }
                    }
                }
            }
        }

        if (monks.Count / farms.Count > goodMonkAndFarmRatio)
        {
            devotionDecreaseMp = defaultDevotionDecreaseMp;
            constructingTimerMp = defaultConstructingTimerMp;
            faithTimerMp = defaultFaithTimerMp;

            devotionIncrease = false;
            devotionDecrease = true;
            
        }

        if (monks.Count / farms.Count >= badMonkAndFarmRatio75)
        {
            devotionDecreaseMp = devotionDecreaseMp1;
            constructingTimerMp = constructingTimerMp1;
            faithTimerMp = faithTimerMp1;
            UpdateFaithMultiplierForProductionBar();
        }

        if (monks.Count / farms.Count >= badMonkAndFarmRatio50)
        {
            devotionDecreaseMp = devotionDecreaseMp2;
            constructingTimerMp = constructingTimerMp2;
            faithTimerMp = faithTimerMp2;
            UpdateFaithMultiplierForProductionBar();
        }

        if (monks.Count / farms.Count >= badMonkAndFarmRatio25)
        {
            devotionDecreaseMp = devotionDecreaseMp3;
            constructingTimerMp = constructingTimerMp3;
            faithTimerMp = faithTimerMp3;
            UpdateFaithMultiplierForProductionBar();
        }
    }
    void UpdateFaithMultiplierForProductionBar()
    {
        foreach (ProductionBar gameObject in ProductionBar.productionBars)
        {
            gameObject.faithMultiplier = faithTimerMp;
        }
    }
}

