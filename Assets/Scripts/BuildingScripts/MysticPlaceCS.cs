using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticPlaceCS : Structure
{
    private bool addedToList;
    bool increaseMonkCost;
    public int monksPurchased;
    public GameObject monk;
    public MonkText monkText;
    public float[] monkFaithCosts;
    float randomDistanceHorizontal;
    float randomDistanceVertical;
    protected override void Start()
    {
        base.Start();
        originalFaithTargetTime = faithTargetTime;
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
        Debug.Log(monkFaithCosts.Length);
        clickedBuilding = gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().clickedBuilding;
        if (gameManager.faith >= monkFaithCosts[monksPurchased])
        {

            gameManager.UseResources(monkFaithCosts[monksPurchased], 0, 0);
            randomDistanceHorizontal = Random.Range(-1.5f, 1.5f);
            randomDistanceVertical = Random.Range(-1.5f, 1.5f);
            GameObject spawnedMonk = Instantiate(monk, new Vector2(clickedBuilding.transform.position.x + randomDistanceHorizontal, clickedBuilding.transform.position.y + randomDistanceVertical), clickedBuilding.transform.rotation);
            gameManager.monks.Add(spawnedMonk);
            monkText.UpdateMonkCount();
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
