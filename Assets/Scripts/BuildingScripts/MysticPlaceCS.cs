using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticPlaceCS : Structure
{
    private bool addedToList;
    bool increaseMonkCost;
    [Space(10)]
    public int addedMonkSlots;
    public int monksPurchased;
    public float[] monkFaithCosts;
    [HideInInspector]
    public GameObject monk, resourceTracker;
    float randomDistanceHorizontal;
    float randomDistanceVertical;

    protected override void Start()
    {
        base.Start();
        resourceTracker = GameObject.Find("MonkText");
        gameManager.monkSlots += addedMonkSlots; 
        originalFaithTargetTime = productionCycleLength;
        faithCollected = true;
        addedToList = false;
        constructingDone = true;
        increaseMonkCost = false;
        name = "Mystic place";
        type = "Faith";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            faithTimer = true;
        }
    }
    public void AddToList()
    {
        gameManager.farms.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
    public void SpawnNewMonk()
    {
        clickedBuilding = gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().clickedBuilding;
        if (gameManager.faith >= monkFaithCosts[monksPurchased] && gameManager.monks.Count < gameManager.monkSlots)
        {

            gameManager.UseResources(monkFaithCosts[monksPurchased], 0, 0, 0);
            randomDistanceHorizontal = Random.Range(-1.5f, 1.5f);
            randomDistanceVertical = Random.Range(-1.5f, 1.5f);
            GameObject spawnedMonk = Instantiate(monk, new Vector2(clickedBuilding.transform.position.x + randomDistanceHorizontal, clickedBuilding.transform.position.y + randomDistanceVertical), clickedBuilding.transform.rotation);
            gameManager.monks.Add(spawnedMonk);
            resourceTracker.GetComponent<MonkText>().UpdateMonkCount();
            gameManager.CheckFarmCount();

            if (monksPurchased < monkFaithCosts.Length - 1)
            {
                increaseMonkCost = true;
            }
        }
        if (gameManager.faith < monkFaithCosts[monksPurchased])
        {
            Debug.Log("Not enough faith");
        }
        if (increaseMonkCost == true)
        {
            monksPurchased++;
            increaseMonkCost = false;

        }
    }
}
