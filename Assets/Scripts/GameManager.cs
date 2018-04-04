using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject building;
    public GameObject monk;

    [SerializeField]
    private float generatedFaith;
    [SerializeField]
    private float faith;

    [SerializeField]
    private float devotion;
    public bool devotionDecrease;

    public bool devotionDecreaseMp1;
    public bool devotionDecreaseMp2;
    public bool devotionDecreaseMp3;

    public bool sanctity;

    public bool activeBuildings;

    public bool faithTimerActive;    
    [SerializeField]
    private float faithTargetTime;
    private float originalFaithTargetTime;

    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> devotionBuildings = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();

    public int sizeOfMonkList;

    void Start()
    {
        originalFaithTargetTime = faithTargetTime;

        devotionDecrease = true;
        devotionDecreaseMp1 = false;
    }

    void Update()
    {
        sizeOfMonkList = monks.Count;

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
        if (activeBuildings == true)
        {
            if (gameObject )
            {
                generatedFaith += 2f * monks.Count;
            }
        }
    }

    public void CollectFaith()
    {
        faith += generatedFaith;
        generatedFaith = 0f;
    }

    public void SpawnNewBuilding()
    {
        GameObject spawned = Instantiate(building, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        devotionBuildings.Add(spawned);


        if (devotionBuildings.Count >= monks.Count)
        {
            devotionDecrease = false;
        }
    }
}
