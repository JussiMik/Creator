﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiracleMenu : Menu
{

    [Header("Building Lists")]
    public GameObject[] miracles = new GameObject[3];
    public Sprite[] miracleSpr = new Sprite[3];

    public Sprite[] miracleSprites;

    public List<GameObject> buildingButtons;

    public GameObject miracleGrid;

    public GameObject infoCard;

    public DragNDrop dragNDrop;

    public bool menuVisible = false;

    public GameObject worldNavigation;

    [Header("Menu Background Sprites")]
    public Sprite menuSpriteUti;
    public Sprite menuSpritePro;


    void Awake()
    {
        worldNavigation = GameObject.Find("WorldNavigation");
        dragNDrop = GameObject.Find("LevelManager").GetComponent<DragNDrop>();



        //INSTANTIATE STRUCTURE MENU

        foreach (Transform child in miracleGrid.transform) if (child.CompareTag("BuildingButton"))
            {
                buildingButtons.Add(child.gameObject);
            }
        for (int i = 0; i < buildingButtons.Count; i++)
        {
            buildingButtons[i].GetComponent<GridMenuBlock>().blockNo = i;
            buildingButtons[i].GetComponent<GridMenuBlock>().upperMenu = gameObject;
        }

    }
    private void Update()
    {
        if (infoCard.active == true)
        {


        }
    }

    public void ShowInfo(int blockNo)
    {
        infoCard.SetActive(true);

        curBlockNo = blockNo;
        infoBuildingSprite.GetComponent<Image>().sprite = buildingButtons[blockNo].GetComponent<Image>().sprite;
        infoBuildingName.GetComponent<Text>().text = "miracle " + (blockNo + 1);
        infoBuildingText.GetComponent<Text>().text = "Info of Miracle " + (blockNo + 1);

        infoBuildingRes_Wood.text = miracles[blockNo].GetComponent<Miracle>().faithNeeded.ToString();
        infoBuildingRes_Faith.text = miracles[blockNo].GetComponent<Miracle>().faithNeeded.ToString();
        infoBuildingRes_Stone.text = miracles[blockNo].GetComponent<Miracle>().faithNeeded.ToString();
        infoBuildingRes_Dev.text = miracles[blockNo].GetComponent<Miracle>().faithNeeded.ToString();

    }

    public void PressStructureMenu()
    {
        if (menuVisible)
        {
            HideMiracleMenu();
            worldNavigation.SetActive(true);
        }
        else
        {
            ShowMiracleMenu();
            //worldNavigation;
            worldNavigation.SetActive(false);
        }

    }
    private void HideMiracleMenu()
    {
        miracleGrid.active = false;
        menuVisible = false;

    }
    private void ShowMiracleMenu()
    {
        miracleGrid.active = true;
        menuVisible = true;
    }

    public override void SelectToDrag()
    {
        dragNDrop.ShowToDrag();
        HideMiracleMenu();
    }
}
