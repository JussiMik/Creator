using System.Collections;
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

<<<<<<< HEAD
=======

>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
    private void Update()
    {
        if (infoCard.active == true)
        {
<<<<<<< HEAD
            CheckResources(miracles);
        }
    }
=======
                CheckResources(miracles);
        }
    }
    
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed

    public void ShowInfo(int blockNo)
    {
        infoCard.SetActive(true);

        curBlockNo = blockNo;
<<<<<<< HEAD

        //if (curBlockNo == 0 || curBlockNo == 1)
        {
            infoBuildingSprite.GetComponent<Image>().sprite = miracles[blockNo].GetComponent<SpriteRenderer>().sprite;
            infoBuildingName.GetComponent<Text>().text = "Miracle " + (blockNo + 1);
            infoBuildingText.GetComponent<Text>().text = "Info of Miracle " + (blockNo + 1);

            infoBuildingRes_Wood.text = miracles[blockNo].GetComponent<Structure>().woodConstructingCost.ToString();
            infoBuildingRes_Faith.text = miracles[blockNo].GetComponent<Structure>().faithConstructingCost.ToString();
            infoBuildingRes_Stone.text = miracles[blockNo].GetComponent<Structure>().stoneConstructingCost.ToString();
            infoBuildingRes_Dev.text = miracles[blockNo].GetComponent<Structure>().devotionConstructingCost.ToString();
            
        }
        //else
        //{
        //    infoBuildingSprite.GetComponent<Image>().sprite = miracles[blockNo].GetComponent<SpriteRenderer>().sprite;
        //    infoBuildingName.GetComponent<Text>().text = "Miracle " + (blockNo + 1);
        //    infoBuildingText.GetComponent<Text>().text = "Info of Miracle " + (blockNo + 1);

        //    infoBuildingRes_Wood.text = miracles[blockNo].GetComponent<Miracle03>().woodConstructingCost.ToString();
        //    infoBuildingRes_Faith.text = miracles[blockNo].GetComponent<Miracle03>().faithConstructingCost.ToString();
        //    infoBuildingRes_Stone.text = miracles[blockNo].GetComponent<Miracle03>().stoneConstructingCost.ToString();
        //    infoBuildingRes_Dev.text = miracles[blockNo].GetComponent<Miracle03>().devotionConstructingCost.ToString();
            
        //}
=======
        infoBuildingSprite.GetComponent<Image>().sprite = miracles[blockNo].GetComponent<SpriteRenderer>().sprite;
        infoBuildingName.GetComponent<Text>().text = "Miracle " + (blockNo + 1);
        infoBuildingText.GetComponent<Text>().text = "Info of Miracle " + (blockNo + 1);

      
            infoBuildingRes_Wood.text = "0";
            infoBuildingRes_Faith.text = "100";
            infoBuildingRes_Stone.text = "0";
            infoBuildingRes_Dev.text = "0";

>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
        infoCard.GetComponent<InfoCard>().currentMenu = gameObject.GetComponent<Menu>();
    }
  
    

    public override void SelectToDrag()
    {
        if(curBlockNo == 0 || curBlockNo == 1)
        {
            dragNDrop.ShowToDrag(miracles[curBlockNo]);
            HideMenu();
        }
        if(curBlockNo == 2)
        {
            HideMenu();
            dragNDrop.layoutManager.BorderMiracle();
        }
        
    }
}
