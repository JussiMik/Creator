using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject building;
    public GameObject monk;

    public float faith;
    public float devotion = 100f;

    public bool buildingActive;

    public bool faithTimerActive;
    
    public float faithTimerTargetTime;
    private float originalFaithTargetTime;

    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> devotionBuildings = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();

    public int sizeOfMonkList;

    void Start()
    {
        originalFaithTargetTime = faithTimerTargetTime;

        for (int i = 0; i < 4; i++)
        {
            GameObject spawnedMonk = Instantiate(monk, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            monks.Add(spawnedMonk);
        }
    }

    void Update()
    {
        sizeOfMonkList = monks.Count;

        if (faithTimerActive == true)
        {
            faithTimerTargetTime -= Time.deltaTime;

            if (faithTimerTargetTime <= 0)
            {
                TimerEnd();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewObject();
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
            faithTimerTargetTime = originalFaithTargetTime;
        }   
    }

    public void GenerateFaith()
    {
        if (buildingActive == true)
        {
            if (gameObject )
            {
                faith += 2f * monks.Count;
            }
        }
    }

    public void SpawnNewObject()
    {
        GameObject spawned = Instantiate(building, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        faithBuildings.Add(spawned);
    }
}
