using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Structure structure;

    [Space(10)]
    public float sanctity;
    [Space(10)]
    [SerializeField]
    private float sanctityPoints;
    [Space(10)]
    public float maxSanctityPoints;
    [Space(10)]
    public float requiredAmountForLvlUp;
    public float requiredAmountIncrease;

    [Space(10)]
    public float faith;

    [Space(10)]
    public float wood;

    [Space(10)]
    public float stone;

    [Space(10)]
    public float devotion;
    [Space(10)]
    public float maxDevotionAmount;
    public float minDevotionAmountCollecting;

    [Space(10)]
    public float devotionChunkDecreaseAmount;
    [Space(10)]
    public float devotionDecreaseMp1;
    public float devotionDecreaseMp2;
    public float devotionDecreaseMp3;
    [Space(10)]
    public bool devotionDecrease;
    public bool devotionDecrease1;
    public bool devotionDecrease2;
    public bool devotionDecrease3;

    [Space(10)]
    public float devotionChunkIncreaseAmount;
    [Space(10)]
    public bool devotionIncrease;  
    public bool devotionIncrease1;
    public bool devotionIncrease2;
    public bool devotionIncrease3;

    [Space(10)]
    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> gardens = new List<GameObject>();
    public List<GameObject> meditationRooms = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<float> faithMultipliers = new List<float>();

    public float monkFaithMultiplier;
    public float monkFaithMultiplierSlow1;
    public float monkFaithMultiplierSlow2;
    public float monkFaithMultiplierSlow3;

    FaithText faithResourceTracker;
    WoodText woodResourceTracker;
    StoneText stoneResourceTracker;
    MonkText monkResourceTracker;

    public int monkSlots;

    private void Awake()
    {
        GetResourceObjects();
    }

    void Start()
    {
        structure = gameObject.GetComponent<Structure>();
        minDevotionAmountCollecting = devotionChunkDecreaseAmount;
    }

    void Update()
    {
        if (devotionDecrease == true)
        {
            DevotionDecrease();
        }

        if (devotionIncrease == true)
        {
            DevotionIncrease();
        }
    }

    //Use collected faith for constructing and leveling up buildings
    public void UseResources(float faithUseAmount, float woodUseAmount, float stoneUseAmount)
    {
        faith -= faithUseAmount;
        faithResourceTracker.UpdateFaith();

        if (faith <= 0)
        {
            faith = 0;
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
    void DevotionDecrease()
    {
        devotion -= Time.deltaTime;

        if (devotionDecrease1 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * devotionDecreaseMp1;
        }
        if (devotionDecrease2 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * devotionDecreaseMp2;
        }
        if (devotionDecrease3 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * devotionDecreaseMp3;
        }

        if (devotion <= 0)
        {
            devotion = 0;
        }
    }

    //Devotion decreases in chunks
    public void DevotionDecreaseChunk()
    {
        devotion -= devotionChunkDecreaseAmount;

        if (devotion <= 0)
        {
            devotion = 0;
        }
    }

    //Devotion increases slowly
    void DevotionIncrease()
    {
        devotion += Time.deltaTime;

        if (devotionIncrease1 == true)
        {
            devotion += Time.deltaTime * 2;
        }

        if (devotion >= maxDevotionAmount)
        {
            devotion = maxDevotionAmount;
        }
    }

    //Devotion increases in chunks
    public void DevotionIncreaseChunk()
    {
        devotion += devotionChunkIncreaseAmount;

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

    private void ChangePlayerLvl()
    {
        sanctity++;
    }

    /*
    public void SpawnFarm()
    {
        //if (faith >= faithUseAmount && devotion >= devotionChunkDecreaseAmount)
        {
            GameObject spawned = Instantiate(farm, new Vector3(transform.position.x + 5, transform.position.y - 2, transform.position.z), transform.rotation);
        }
    }
    */

    void GetResourceObjects()
    {
        monkResourceTracker = GameObject.Find("MonkText").GetComponent<MonkText>();
        faithResourceTracker = GameObject.Find("FaithText").GetComponent<FaithText>();
        woodResourceTracker = GameObject.Find("WoodText").GetComponent<WoodText>();
        stoneResourceTracker = GameObject.Find("StoneText").GetComponent<StoneText>();
    }
}
