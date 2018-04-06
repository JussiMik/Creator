using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Structure structure;

    public GameObject building;
    public GameObject shrine;
    public GameObject statue;
    public GameObject temple;

    [SerializeField]
    private double generatedFaith;
    [SerializeField]
    private double faith;

    [SerializeField]
    private float devotion;
    public bool devotionDecrease;

    public bool devotionDecreaseMp1;
    public bool devotionDecreaseMp2;
    public bool devotionDecreaseMp3;

    public bool sanctity;

    public bool faithTimerActive;
    [SerializeField]
    private float faithTargetTime;
    private float originalFaithTargetTime;

    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> devotionBuildings = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<double> faithMultipliers = new List<double>();

    public double monkFaithMultiplier;

    void Start()
    {
        structure = gameObject.GetComponent<Structure>();

        originalFaithTargetTime = faithTargetTime;

        devotionDecrease = true;
        devotionDecreaseMp1 = false;
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

        if (devotionDecrease == true)
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
            SpawnNewBuilding();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CollectFaith();
        }
    }

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

    public void GenerateFaith()
    {
        for (int i = 0; i < faithMultipliers.Count; i++)
        {
            generatedFaith += (structure.faithAmount * faithMultipliers[i]);
        }

        generatedFaith += (monks.Count * monkFaithMultiplier);
    }

    public void CollectFaith()
    {
        faith += generatedFaith;
        generatedFaith = 0;
    }

    public void SpawnShrine()
    {
        GameObject spawnedShrine = Instantiate(shrine, new Vector3(transform.position.x + 3, transform.position.y + 4, transform.position.z), transform.rotation);
        //faithBuildings.Add(spawnedShrine);
    }

    public void SpawnStatue()
    {
        GameObject spawnedStatue = Instantiate(statue, new Vector3(transform.position.x + 2, transform.position.y - 2, transform.position.z), transform.rotation);
        //faithBuildings.Add(spawnedStatue);
    }

    public void SpawnTemple()
    {
        GameObject spawned = Instantiate(temple, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        //faithBuildings.Add(spawned);
    }

    public void SpawnNewBuilding()
    {
        GameObject spawned = Instantiate(building, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        devotionBuildings.Add(spawned);


        if (devotionBuildings.Count >= monks.Count)
        {
            devotionDecreaseMp1 = false;
        }
    }
}
