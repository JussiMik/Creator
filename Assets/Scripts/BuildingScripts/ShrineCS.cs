using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineCS : Structure
{
    private SpriteRenderer spriteRenderer;
    private bool addedToList;

    [Space(10)]
    public float totalShrineAmount;
    [Space(10)]
    public bool allowTempleConstructing;

    private void Awake()
    {
        playAudio = true;
    }

    protected override void Start()
    {
        base.Start();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = underConstructionSprite;
        originalFaithTargetTime = productionCycleLength;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;

        constructingTimer = true;

        name = "Shrine";
        type = "Faith";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            spriteRenderer.sprite = finishedBuildingSprite;
            AddToList();
            AddToObjectivesList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
            faithTimer = true;
        }
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }

    private void AddToObjectivesList()
    {
      //  objectiveManager.shrineList.Add(gameObject);
      //  objectiveManager.CheckForCompletedObjectives();
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
