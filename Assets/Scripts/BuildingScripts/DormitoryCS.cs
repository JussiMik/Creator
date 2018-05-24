using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormitoryCS : Structure
{
    private SpriteRenderer spriteRenderer;
    private bool addedToList;
    [Space(10)]
    public int monkSlotsPerDormitory;

    private void Awake()
    {
        playAudio = true;
    }

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = underConstructionSprite;
        normalSpeedConstructing = true;
        addedToList = false;

        constructingTimer = true;

        name = "Dormitory";
        type = "Dormitory";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            spriteRenderer.sprite = finishedBuildingSprite;
            //AddToList();
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
            gameManager.monkSlots += monkSlotsPerDormitory;
            addedToList = true;
        }


    }

}
