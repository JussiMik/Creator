using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Structure structure;

    public GameObject shrine;
    public GameObject statue;
    public GameObject temple;
    public GameObject farm;
    public GameObject garden;
    public GameObject meditationRoom;

    [SerializeField]
    private double generatedFaith;
    [SerializeField]
    private double faith;

    [SerializeField]
    private float devotion;
    public float maxDevotionAmount;
    public bool devotionDecrease;
    public bool allowDevotionDecreaseInChunks;
    public bool devotionDecreaseChunk;
    public float devotionChunkDecreaseAmount;
    public bool devotionIncrease;
    public bool devotionIncreaseMp1;

    public bool devotionDecreaseMp1;
    public bool devotionDecreaseMp2;
    public bool devotionDecreaseMp3;

    public bool sanctity;

    public bool faithTimerActive;
    [SerializeField]
    private float faithTargetTime;
    private float originalFaithTargetTime;

    //public Monk[] controllers;
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

        originalFaithTargetTime = faithTargetTime;

        devotionDecrease = false;
        devotionDecreaseMp1 = false;
        devotionDecreaseChunk = false;
        allowDevotionDecreaseInChunks = true;
    }

    void Update()
    {
        if (faithBuildings.Count > 0)
        {
            FaithTimer();
        }

        if (faithTimerActive == true)
        {
            faithTargetTime -= Time.deltaTime;

            if (faithTargetTime <= 0)
            {
                TimerEnd();
            }
        }

        if (generatedFaith > 0)
        {
            allowDevotionDecreaseInChunks = true;
        }

        if (devotionDecrease == true)
        {
            devotionIncrease = false;

            DevotionDecrease();
        }

        if (devotionDecreaseChunk == true && allowDevotionDecreaseInChunks == true)
        {
            DevotionDecreaseChunk();
        }

        if (devotion <= 0)
        {
            devotion = 0;
            //devotionDecreaseChunk = false;
            allowDevotionDecreaseInChunks = false;
        }

        if (devotion < 10 && devotion > 0)
        {
            allowDevotionDecreaseInChunks = false;
        }

        if (devotion >= 10)
        {
            allowDevotionDecreaseInChunks = true;
        }

        if (devotionIncrease == true)
        {
            devotionDecrease = false;
            //devotionDecreaseChunk = false;

            DevotionIncrease();
        }

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

        if (Input.GetKeyDown(KeyCode.Mouse1) && generatedFaith > 0 && devotion >= 10)
        {
            CollectFaith();
        }
    }

    //Faithtimer before faithgeneration starts
    public void FaithTimer()
    {
        faithTimerActive = true;
    }

    //Timer ends and starts faith generation
    public void TimerEnd()
    {
        faithTimerActive = false;

        GenerateFaith();

        if (faithTimerActive == false)
        {
            faithTimerActive = true;
            faithTargetTime = originalFaithTargetTime;
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
        devotionDecreaseChunk = true;

        faith += generatedFaith;
        generatedFaith = 0;
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
    }

    //Devotion decreases in chunks
    void DevotionDecreaseChunk()
    {
        devotion -= devotionChunkDecreaseAmount;
        devotionDecreaseChunk = false;
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

    public void SpawnFarm()
    {
        GameObject spawned = Instantiate(farm, new Vector3(transform.position.x + 1, transform.position.y - 4, transform.position.z), transform.rotation);
    }

    public void SpawnGarden()
    {
        GameObject spawned = Instantiate(garden, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
}
