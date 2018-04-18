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

    /*
    public GameObject temple;
    public GameObject farm;*/
    public GameObject garden;
    public GameObject meditationRoom;
    
    [Space (10)]
    public double faith;

    [Space(10)]
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

    [Space(10)]
    [SerializeField]
    public bool slowerFaithGeneration1;
    public bool slowerFaithGeneration2;
    public bool slowerFaithGeneration3;

    [Space(10)]
    public List<GameObject> faithBuildings = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> gardens = new List<GameObject>();
    public List<GameObject> meditationRooms = new List<GameObject>();
    public List<GameObject> monks = new List<GameObject>();
    public List<double> faithMultipliers = new List<double>();

    public double monkFaithMultiplier;
    public double monkFaithMultiplierSlow1;
    public double monkFaithMultiplierSlow2;
    public double monkFaithMultiplierSlow3;

    void Start()
    {
        structure = gameObject.GetComponent<Structure>();
    }

    void Update()
    {
        /*
        if (faithBuildings.Count > 0)
        {
            structure.FaithTimer();
        }
        */

        if (devotionDecrease == true)
        {
            DevotionDecrease();
        }

        if (devotionIncrease == true)
        {
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

        /*
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnTemple();
        }
        */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGarden();
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse1) && structure.generatedFaith > 0 && devotion >= 10)
        {
            structure.CollectFaith();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnNewMonk();
        }
    }

    public void SpawnNewMonk()
    {
        GameObject spawnedMonk = Instantiate(monk, new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), transform.rotation);
        monks.Add(spawnedMonk);
    }

    



    //Generates faith resource that player can later extract and use
    /*
    public void GenerateFaith()
    {
        for (int i = 0; i < faithMultipliers.Count; i++)
        {
            generatedFaith += (structure.faithAmount * faithMultipliers[i]);
        }

        generatedFaith += (monks.Count * monkFaithMultiplier);

        if (slowerFaithGeneration1 == true)
        {
            generatedFaith += (monks.Count * monkFaithMultiplierSlow1);
        }
        if (slowerFaithGeneration2 == true)
        {
            generatedFaith += (monks.Count * monkFaithMultiplierSlow2);
        }
        if (slowerFaithGeneration3 == true)
        {
            generatedFaith += (monks.Count * monkFaithMultiplierSlow3);
        }
    }*/

   

    //Use collected faith for constructing and leveling up buildings
    public void UseFaith(float faithUseAmount)
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

    
    public void SpawnShrine()
    {
        GameObject spawnedShrine = Instantiate(shrine, new Vector3(transform.position.x + 3, transform.position.y + 4, transform.position.z), transform.rotation);
    }
    
    public void SpawnStatue()
    {
        GameObject spawnedStatue = Instantiate(statue, new Vector3(transform.position.x + 2, transform.position.y - 2, transform.position.z), transform.rotation);
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
            //GameObject spawned = Instantiate(gameObject, new Vector3(transform.position.x + 1, transform.position.y - 4, transform.position.z), transform.rotation);
        }
    }

    
    public void SpawnGarden()
    {
        GameObject spawned = Instantiate(garden, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }
    
}
