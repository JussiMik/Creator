using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenu : MonoBehaviour {

    //Miracle
    //Production

    public GameObject[] buildingsPro;
    public GameObject[] buildingsUti;
    public GameObject[] buildingButtons;
    public GameObject buildingMenuBlockPre;
    public GameObject buildingMenu;

    public Sprite gridSprite;

    public int gridSizeX = 2;
    public int gridSizeY = 2;

    public GameObject buildGrid;

    public DragNDrop dragNDrop;

    public bool menuVisible = false;

    private int curBlockNo = 0;

    public GameObject worldNavigation;

    void Awake()
    {
        worldNavigation = GameObject.Find("WorldNavigation");
        dragNDrop = GameObject.Find("LevelManager").GetComponent<DragNDrop>();



        //INSTANTIATE STRUCTURE MENU
        buildingButtons = buildGrid.FindGameObjectsWithTag("MenuButton");
        //float blockWidth = buildingMenuBlockPre.GetComponent<RectTransform>().rect.width;
        //float openerWidth = gameObject.GetComponent<RectTransform>().rect.width;

        //INSTATIATE BUILDGRID
        //buildGrid = Instantiate(new GameObject(), new Vector3(0,0,0), transform.rotation);
        //buildGrid.active = false;
        //buildGrid.name = "Building Grid";
        //buildGrid.transform.parent = gameObject.transform;
        //buildGrid.AddComponent<Image>();
        //buildGrid.GetComponent<Image>().sprite = gridSprite;
        //buildGrid.GetComponent<RectTransform>().anchoredPosition = new Vector2(-256, 260);
        //buildGrid.GetComponent<RectTransform>().localScale = new Vector3(7f, 4f, 1f);

        //Building buttons spawn
        //for (int x = 0; x < gridSizeX; x++)
        //{
        //    for (int y = 0; y < gridSizeX; y++)
        //    {
        //        Vector3 pos = new Vector3(gameObject.transform.position.x - 500 + (100 * x), gameObject.transform.position.y + 200 - (100 * y), gameObject.transform.position.z);
        //        structButtons[x,y] = Instantiate(buildingMenuBlockPre, pos, transform.rotation);
        //        structButtons[x,y].transform.SetParent(buildGrid.transform);
        //        structButtons[x, y].GetComponent<StructMenuBlock>().blockNo = curBlockNo;
        //        if (buildingsPro.Length > curBlockNo)
        //        {
        //            structButtons[x, y].GetComponent<Image>().sprite = buildingsPro[curBlockNo].GetComponent<SpriteRenderer>().sprite;
        //        }
        //        structButtons[x,y].name = "StructButton " + x + ", "+ y;
        //        structButtons[x,y].GetComponent<StructMenuBlock>().structMenu = this;
        //        curBlockNo += 1;
        //    }

        //}

        

    }

    public void ProducalButton()
    {

    }

    public void UnitialButton()
    {

    }

    public void PressStructureMenu()
    {
        if(menuVisible)
        {
            HideStructMenu();
            worldNavigation.SetActive(true);
        }
        else
        {
            ShowStructMenu();
            //worldNavigation;
            worldNavigation.SetActive(false);
        }
        
    }
    public void SelectToDrag(int blockNo)
    {
        dragNDrop.ShowToDrag(buildingsPro[blockNo]);
        HideStructMenu();
    }
    public void HideStructMenu()
    {
        buildGrid.active = false;
        menuVisible = false;
       
    }
    public void ShowStructMenu()
    {
        buildGrid.active = true;
        menuVisible = true;
       
    }
}
