using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticPlaceCS : Structure
{
    private bool addedToList;
    public GameObject monk;
    public MonkText monkText;
    protected override void Start()
    {
        base.Start();
        originalFaithTargetTime = faithTargetTime;
        faithCollected = true;
        addedToList = false;
        constructingDone = true;

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
        gameManager.meditationRooms.Add(gameObject);
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
    public void SpawnNewMonk()
    {
        GameObject spawnedMonk = Instantiate(monk, new Vector3(clickedBuilding.transform.position.x + 2, clickedBuilding.transform.position.y + 2, clickedBuilding.transform.position.z), clickedBuilding.transform.rotation);
        gameManager.monks.Add(spawnedMonk);
        monkText.UpdateMonkCount();
    }
}
