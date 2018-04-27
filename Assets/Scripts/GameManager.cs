using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Structure structure;
    public GameObject monk;
    public GameObject shrine;
    public GameObject statue;
    //public GameObject temple;
    public GameObject farm;
    public GameObject garden;
    public GameObject meditationRoom;

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

    public bool devotionDecrease;
    public float devotionChunkDecreaseAmount;
    public bool devotionDecreaseMp1;
    public bool devotionDecreaseMp2;
    public bool devotionDecreaseMp3;

    public bool devotionIncrease;
    public float devotionChunkIncreaseAmount;
    public bool devotionIncreaseMp1;

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

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnShrine();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnStatue();
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnGarden();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnGarden();
            SpawnFarm();
        }

        /* if (Input.GetKeyDown(KeyCode.W))
         {
             SpawnNewMonk();
         }
         
    }

       public void SpawnNewMonk()
       {
           clickedObject = PopupCanvas.GetComponent<PopupMenu>().clickedObject;
           GameObject spawnedMonk = Instantiate(monk, new Vector3(clickedObject.transform.position.x + 2 , clickedObject.transform.position.y + 2, clickedObject.transform.position.z), clickedObject.transform.rotation);
           monks.Add(spawnedMonk);
           monkResourceTracker.UpdateMonkCount();
       }
       */

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

        if (devotionDecreaseMp1 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * 2;
        }
        if (devotionDecreaseMp2 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * 3;
        }
        if (devotionDecreaseMp3 == true)
        {
            structure.changedValue = false;
            devotion -= Time.deltaTime * 4;
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

        if (devotionIncreaseMp1 == true)
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

    public void SpawnShrine()
    {
        GameObject spawnedShrine = Instantiate(shrine, new Vector3(transform.position.x - 3, transform.position.y + 4, transform.position.z), transform.rotation);
    }

    public void SpawnStatue()
    {
        GameObject spawnedStatue = Instantiate(statue, new Vector3(transform.position.x + 1, transform.position.y - 2, transform.position.z), transform.rotation);
    }

    /*
    public void SpawnTemple()
    {
        GameObject spawned = Instantiate(temple, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
    */

    public void SpawnFarm()
    {
        //if (faith >= faithUseAmount && devotion >= devotionChunkDecreaseAmount)
        {
            GameObject spawned = Instantiate(farm, new Vector3(transform.position.x + 5, transform.position.y - 2, transform.position.z), transform.rotation);
        }
    }

    public void SpawnGarden()
    {
        GameObject spawned = Instantiate(garden, new Vector3(transform.position.x - 6, transform.position.y - 3, transform.position.z), transform.rotation);
    }

    void GetResourceObjects()
    {
        monkResourceTracker = GameObject.Find("MonkText").GetComponent<MonkText>();
        faithResourceTracker = GameObject.Find("FaithText").GetComponent<FaithText>();
        woodResourceTracker = GameObject.Find("WoodText").GetComponent<WoodText>();
        stoneResourceTracker = GameObject.Find("StoneText").GetComponent<StoneText>();
    }
}
