using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Structure structure;

    /*
    public GameObject shrine;
    public GameObject statue;
    public GameObject temple;
    public GameObject farm;
    public GameObject garden;
    public GameObject meditationRoom;
    */

    [SerializeField]
    private double generatedFaith;
    public double faith;
    public float faithUseAmount;

    [SerializeField]
    private float devotion;
    public float maxDevotionAmount;

    public bool devotionDecrease;
    public float devotionChunkDecreaseAmount;
    public bool devotionDecreaseMp1;
    public bool devotionDecreaseMp2;
    public bool devotionDecreaseMp3;

    public bool devotionIncrease;
    public float devotionChunkIncreaseAmount;
    public bool devotionIncreaseMp1;

    public bool faithTimer;
    [SerializeField]
    private float faithTargetTime;
    private float originalFaithTargetTime;

    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> gardens = new List<GameObject>();
    public List<GameObject> meditationRooms = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<double> faithMultipliers = new List<double>();

    public double monkFaithMultiplier;

    void Start()
    {
        structure = gameObject.GetComponent<Structure>();

        faithTimer = true;
        originalFaithTargetTime = faithTargetTime;
    }

    void Update()
    {
        if (faithBuildings.Count > 0 && faithTimer == true)
        {
            FaithTimer();
        }

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

        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnTemple();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFarm();
        }
        */

        if (Input.GetKeyDown(KeyCode.Mouse1) && generatedFaith > 0 && devotion >= 10)
        {
            CollectFaith();
        }
    }

    //Faithtimer before faithgeneration starts
    public void FaithTimer()
    {
        faithTargetTime -= Time.deltaTime;

        if (faithTargetTime <= 0)
        {
            faithTimer = false;
            TimerEnd();
        }
    }

    //Timer ends and starts faith generation
    public void TimerEnd()
    {
        GenerateFaith();

        if (faithTimer == false)
        {
            faithTargetTime = originalFaithTargetTime;
            faithTimer = true;
        }
    }

    //Generates faith resource that player can later extract and use
    public void GenerateFaith()
    {
        for (int i = 0; i < faithMultipliers.Count; i++)
        {
            generatedFaith += (structure.faithAmount * faithMultipliers[i]);
        }

        generatedFaith += (monks.Count * monkFaithMultiplier);
    }

    //Player can collect generated faith for later use
    public void CollectFaith()
    {
        DevotionDecreaseChunk();

        faith += generatedFaith;
        generatedFaith = 0;
    }

    public void UseFaith()
    {
        faith -= faithUseAmount;

        if (faith <= 0)
        {
            faith = 0;
        }
    }

    //Devotion decreases slowly
    void DevotionDecrease()
    {
        devotion -= Time.deltaTime;

        if (devotionDecreaseMp1 == true)
        {
            devotion -= Time.deltaTime * 2;
        }
        else if (devotionDecreaseMp2 == true)
        {
            devotion -= Time.deltaTime * 3;
        }
        else if (devotionDecreaseMp3 == true)
        {
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

    public void DevotionIncreaseChunk()
    {
        devotion += devotionChunkIncreaseAmount;

        if (devotion >= maxDevotionAmount)
        {
            devotion = maxDevotionAmount;
        }
    }

    /*
    public void SpawnShrine()
    {
        GameObject spawnedShrine = Instantiate(shrine, new Vector3(transform.position.x + 3, transform.position.y + 4, transform.position.z), transform.rotation);
    }

    public void SpawnStatue()
    {
        GameObject spawnedStatue = Instantiate(statue, new Vector3(transform.position.x + 2, transform.position.y - 2, transform.position.z), transform.rotation);
    }

    public void SpawnTemple()
    {
        GameObject spawned = Instantiate(temple, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
    */

    public void SpawnFarm()
    {
        if (faith >= faithUseAmount && devotion >= devotionChunkDecreaseAmount)
        {
            //GameObject spawned = Instantiate(gameObject, new Vector3(transform.position.x + 1, transform.position.y - 4, transform.position.z), transform.rotation);
        }
    }

    /*
    public void SpawnGarden()
    {
        GameObject spawned = Instantiate(garden, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
    */
}
