using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineCS : Structure
{
    private bool addedToList;

    [Space(10)]
    public float totalShrineAmount;
    [Space(10)]
    public bool allowTempleConstructing;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = faithTargetTime;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;
        ConstructingStructures();
        
        gameManager.UseFaith(constructingCost);

        name = "Shrine";
        type = "Faith";
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointAmount);
            faithTimer = true;
        }
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }

    public void UpdateValues()
    {
        totalShrineAmount = transform.GetChild(0).GetComponent<ShrineCollider>().totalShrineAmount;
    }

    public void UpdateBool()
    {
        allowTempleConstructing = transform.GetChild(0).GetComponent<ShrineCollider>().allowTempleConstructing;
    }
}
