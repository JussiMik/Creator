using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineCS : Structure
{
    private bool addedToList;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        ConstructingStructures();

        addedToList = false;
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
        }
    }

    public override void FaithTimer()
    {
        base.FaithTimer();
    }

    public override void TimerEnd()
    {
        base.TimerEnd();
    }

    public override void GenerateFaith()
    {
        base.GenerateFaith();
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
}
