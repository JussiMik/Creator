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

    public float monkFaithBaseCost;
    public float monkFaithCost;
    public int monksNeededForMultiplierIncrease1;
    public int monksNeededForMultiplierIncrease2;
    public int monksNeededForMultiplierIncrease3;
    public float monkFaithCostMultiplier1;
    public float monkFaithCostMultiplier2;
    public float monkFaithCostMultiplier3;

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

        monkFaithCost = monkFaithBaseCost;
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
        gameManager.gardens.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
    public void SpawnNewMonk()
    {
        clickedBuilding = gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().clickedBuilding;

        if (gameManager.faith >= monkFaithCost && gameManager.monks.Count < gameManager.monkSlots)
        {
            gameManager.UseResources(monkFaithCost,0, 0, 0);
            monksPurchased++;
            if (monksPurchased <= monksNeededForMultiplierIncrease1)
            {
                monkFaithCost *= monkFaithCostMultiplier1;
            }
            else if (monksPurchased <= monksNeededForMultiplierIncrease2)
            {
                monkFaithCost *= monkFaithCostMultiplier2;
            }
            else if (monksPurchased >= monksNeededForMultiplierIncrease3)
            {
                monkFaithCost *= monkFaithCostMultiplier3;
            }

            randomDistanceHorizontal = Random.Range(-1.5f, 1.5f);
            randomDistanceVertical = Random.Range(-1.5f, 1.5f);
            GameObject spawnedMonk = Instantiate(monk, new Vector2(clickedBuilding.transform.position.x + randomDistanceHorizontal, clickedBuilding.transform.position.y + randomDistanceVertical), clickedBuilding.transform.rotation);
            gameManager.monks.Add(spawnedMonk);
            resourceTracker.GetComponent<MonkText>().UpdateMonkCount();
            gameManager.CheckFarmCount();
        }
        if (gameManager.faith < monkFaithCost)
        {
            Debug.Log("Not enough faith");
        }
    }
    public override void ChangeLevel()
    {
        base.ChangeLevel();
        objectiveManager.CheckForCompletedObjectives();
    }
}
