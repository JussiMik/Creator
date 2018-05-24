using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversionTempleCS : Structure
{
    private SpriteRenderer spriteRenderer;
    public float convertedMonk;
    public float monkConversionTimeLength;
    public int totalMonksConverted;
    public Sprite monkSprite;
    public GameObject convertableMonk;
    [HideInInspector]
    public bool conversionTimer, monkCollected;
    [HideInInspector]
    public float originalMonkConversionTimeLength;

    private bool sanctityPointsGiven;


    public bool testConversion;

    private void Awake()
    {
        playAudio = true;
    }

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = underConstructionSprite;
        originalMonkConversionTimeLength = monkConversionTimeLength;
        normalSpeedConstructing = true;
        monkCollected = true;
        constructingTimer = true;

        name = "Conversion temple";
        type = "Food";

        PlayAudio();
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && sanctityPointsGiven == false)
        {
            spriteRenderer.sprite = finishedBuildingSprite;
            gameManager.GiveSanctityPoints(sanctityPointsOnConsturction);
            sanctityPointsGiven = true;
            conversionTimer = true;
        }

        if (conversionTimer == true && monkCollected == true)
        {
            MonkConvsersionTimer();
        }

        if (testConversion == true)
        {
            ConvertMonk();
            testConversion = false;
        }
    }

    void MonkConvsersionTimer()
    {
        monkConversionTimeLength -= Time.deltaTime;

        if (monkConversionTimeLength <= 0)
        {
            monkConversionTimeLength = 0;

            convertedMonk++;
            conversionTimer = false;
        }

        if (conversionTimer == false)
        {
            monkConversionTimeLength = originalMonkConversionTimeLength;
            conversionTimer = true;
            monkCollected = false;

            if (level != 1)
            {
                ConvertMonk();
            }    
        }
    }
    public void ConvertMonk()
    {
        if (gameManager.convertableMonks.Count != 0 && gameManager.monks.Count < gameManager.monkSlots)
        {
            convertedMonk = 0;
            convertableMonk = gameManager.convertableMonks[0];
            gameManager.convertableMonks.RemoveAt(0);
            gameManager.monks.Add(convertableMonk);
            gameManager.totalMonksConverted++;
            gameManager.CheckFarmCount();
            convertableMonk.GetComponent<ConvertableMonk>().isConverted = true;
            convertableMonk.GetComponent<SpriteRenderer>().sprite = monkSprite;
            objectiveManager.CheckForCompletedObjectives();
            gameManager.DevotionDecreaseChunk();
            gameManager.GetComponent<CollectResourcesAndOpenPanelInput>().showPanel = false;
            monkCollected = true;
        }
    }
}
