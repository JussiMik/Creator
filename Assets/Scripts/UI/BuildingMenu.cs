using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : Menu {

    [Header("Building Lists")]
    public GameObject[] buildingsPro = new GameObject[6];
    public GameObject[] buildingsUti = new GameObject[3];

    public List<GameObject> buildingButtons;
    public GameObject buildingMenuBlockPre;
    public GameObject buildingMenu;

    public Sprite gridSprite;

    //public int gridSizeX = 2;
    //public int gridSizeY = 2;

    public GameObject buildGrid;

    public DragNDrop dragNDrop;

    public bool proActive;

    [Header("Menu Background Sprites")]
    public Sprite menuSpriteUti;
    public Sprite menuSpritePro;

    void Awake()
    {
        //infoBuildingRes_Wood = GameObject.Find("Info_Resource_Wood").transform.Find("Text").GetComponent<Text>();
        //infoBuildingRes_Stone = GameObject.Find("Info_Resource_Stone").transform.Find("Text").GetComponent<Text>();
        //infoBuildingRes_Dev = GameObject.Find("Info_Resource_Devotion").transform.Find("Text").GetComponent<Text>();
        //infoBuildingRes_Faith = GameObject.Find("Info_Resource_Faith").transform.Find("Text").GetComponent<Text>();



        worldNavigation = GameObject.Find("WorldNavigation");
        dragNDrop = GameObject.Find("LevelManager").GetComponent<DragNDrop>();



        //INSTANTIATE STRUCTURE MENU

        foreach (Transform child in buildGrid.transform) if (child.CompareTag("BuildingButton"))
            {
                buildingButtons.Add(child.gameObject);
            }
        for (int i = 0; i < buildingButtons.Count; i++)
        {
            buildingButtons[i].GetComponent<GridMenuBlock>().blockNo = i;
            buildingButtons[i].GetComponent<GridMenuBlock>().upperMenu = gameObject;
        }

        ProducalToActive();

    }
    private void Update()
    {
        if (infoCard.active == true)
        {
            if(proActive)
            {
                Debug.Log("proooooooooooooo");
                CheckResources(buildingsPro);
            }
            else
            {
                Debug.Log("utiiiiiiiiiiiii");
                CheckResources(buildingsUti);
            }
            
        } 
    }


    public void ProducalToActive()
    {
        proActive = true;
        buildGrid.GetComponent<Image>().sprite = menuSpritePro;

        for (int i = 0; i < buildingButtons.Count; i++)
        {
            if (buildingsPro.Length - 1  >= i )
            {
                buildingButtons[i].GetComponent<Image>().sprite = buildingsPro[i].GetComponent<SpriteRenderer>().sprite;
                buildingButtons[i].transform.Find("Text").gameObject.GetComponent<Text>().text = buildingsPro[i].name;
            }
            else
            {
                buildingButtons[i].GetComponent<Image>().sprite = gridSprite;
                buildingButtons[i].transform.Find("Text").gameObject.GetComponent<Text>().text = "Butt";
            }
            
        }
    }

    public void UtilityToActive()
    {
        proActive = false;
        buildGrid.GetComponent<Image>().sprite = menuSpriteUti;
        for (int i = 0; i < buildingButtons.Count; i++)
        {
            if (buildingsUti.Length - 1 >= i)
            {
                buildingButtons[i].GetComponent<Image>().sprite = buildingsUti[i].GetComponent<SpriteRenderer>().sprite;
                buildingButtons[i].transform.Find("Text").gameObject.GetComponent<Text>().text = buildingsUti[i].name;
            }
            else
            {
                buildingButtons[i].GetComponent<Image>().sprite = gridSprite;
                buildingButtons[i].transform.Find("Text").gameObject.GetComponent<Text>().text = "Butt";
            }

        }
    }



   
    public void ShowInfo(int blockNo)
    {
        infoCard.SetActive(true);
        if (proActive)
        {
            curBlockNo = blockNo;
            infoBuildingSprite.GetComponent<Image>().sprite = buildingsPro[blockNo].GetComponent<SpriteRenderer>().sprite;
            infoBuildingName.GetComponent<Text>().text = buildingsPro[blockNo].name;
            infoBuildingText.GetComponent<Text>().text = buildingsPro[blockNo].GetComponent<Structure>().info;

            infoBuildingRes_Wood.text = buildingsPro[blockNo].GetComponent<Structure>().woodConstructingCost.ToString();
            infoBuildingRes_Faith.text = buildingsPro[blockNo].GetComponent<Structure>().faithConstructingCost.ToString();
            infoBuildingRes_Stone.text = buildingsPro[blockNo].GetComponent<Structure>().stoneConstructingCost.ToString();
            infoBuildingRes_Dev.text = buildingsPro[blockNo].GetComponent<Structure>().devotionConstructingCost.ToString();
            infoCard.GetComponent<InfoCard>().currentMenu = gameObject.GetComponent<Menu>();
        }
        else
        {
            curBlockNo = blockNo;
            infoBuildingSprite.GetComponent<Image>().sprite = buildingsUti[blockNo].GetComponent<SpriteRenderer>().sprite;
            infoBuildingName.GetComponent<Text>().text = buildingsUti[blockNo].name;
            infoBuildingText.GetComponent<Text>().text = buildingsUti[blockNo].GetComponent<Structure>().info;

            infoBuildingRes_Wood.text = buildingsUti[blockNo].GetComponent<Structure>().woodConstructingCost.ToString();
            infoBuildingRes_Faith.text = buildingsUti[blockNo].GetComponent<Structure>().faithConstructingCost.ToString();
            infoBuildingRes_Stone.text = buildingsUti[blockNo].GetComponent<Structure>().stoneConstructingCost.ToString();
            infoBuildingRes_Dev.text = buildingsUti[blockNo].GetComponent<Structure>().devotionConstructingCost.ToString();
            infoCard.GetComponent<InfoCard>().currentMenu = gameObject.GetComponent<Menu>();
           
        }
    }

    public override void SelectToDrag()
    {
        base.SelectToDrag();
        if (proActive)
        {
            dragNDrop.ShowToDrag(buildingsPro[curBlockNo]);
        }
        else
        {
            dragNDrop.ShowToDrag(buildingsUti[curBlockNo]);
        }
        base.HideMenuNavigationOff();
    }

   
   
}
