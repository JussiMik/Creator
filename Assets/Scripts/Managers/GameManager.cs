using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Structure structure;

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
    public float devotionChunkIncreaseAfterKilledMonks;
    [Space(10)]
    public float devotionIncreaseMp1;
    [Space(10)]
    public bool devotionIncrease;  
    public bool devotionIncrease1;
    public bool devotionIncrease2;
    public bool devotionIncrease3;

    [Space(10)]
    public float monkFaithMultiplier;
    /*
    public float monkFaithMultiplierSlow1;
    public float monkFaithMultiplierSlow2;
    public float monkFaithMultiplierSlow3;
    */

    [Space(10)]
    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> gardens = new List<GameObject>();
    public List<GameObject> meditationRooms = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<float> faithMultipliers = new List<float>();

    FaithText faithResourceTracker;
    DevotionText devotionResourceTracker;
    WoodText woodResourceTracker;
    StoneText stoneResourceTracker;
    MonkText monkResourceTracker;

    public int monkSlots;

    public float goodMonkAndFarmRatio;

    [SerializeField]
    private float numberOfMonksToKill;

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
    void DevotionIncrease()
    {
        devotion += Time.deltaTime;

        if (devotionIncrease1 == true)
        {
            devotion += Time.deltaTime * devotionIncreaseMp1;
        }

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

    //Goes through a list of monks and destroys all of the excess ones in Update() function
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
}
