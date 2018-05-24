using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Structure
{
    private SpriteRenderer spriteRenderer;
    private bool addedToList;

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
        normalSpeedConstructing = true;
        addedToList = false;

        constructingTimer = true;

        name = "Farm";
        type = "Food";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            spriteRenderer.sprite = finishedBuildingSprite;
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
        }
    }

    public void AddToList()
    {
        gameManager.farms.Add(gameObject);
        gameManager.CheckFarmCount();
        addedToList = true;
    }
}
