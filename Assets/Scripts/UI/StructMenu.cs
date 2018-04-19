using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenu : MonoBehaviour {

    public GameObject[] structures;
    public GameObject[,] structButtons;
    public GameObject buildingMenuBlockPre;
    public GameObject buildingMenu;

    public Sprite gridSprite;

    public int gridSizeX = 2;
    public int gridSizeY = 2;

    GameObject buildGrid;

    public DragNDrop dragNDrop;

    public bool menuVisible = false;

    private int curBlockNo = 0;

    void Awake()
    {
        dragNDrop = GameObject.Find("LevelManager").GetComponent<DragNDrop>();

        //INSTANTIATE STRUCTURE MENU
        structButtons = new GameObject[gridSizeX, gridSizeY];
        float blockWidth = buildingMenuBlockPre.GetComponent<RectTransform>().rect.width;
        float openerWidth = gameObject.GetComponent<RectTransform>().rect.width;
        buildGrid = Instantiate(new GameObject(), new Vector3(0,0,0), transform.rotation);
        buildGrid.active = false;
        buildGrid.name = "Building Grid";
        buildGrid.transform.parent = gameObject.transform;
        buildGrid.AddComponent<Image>();
        buildGrid.GetComponent<Image>().sprite = gridSprite;
        buildGrid.GetComponent<RectTransform>().anchoredPosition = new Vector2(-256, 260);
        buildGrid.GetComponent<RectTransform>().localScale = new Vector3(7f, 4f, 1f);


        //for (int i = 0; i < structures.Length; i++)
        //{
        //    Vector3 pos = new Vector3 (buildingMenu.transform.position.x - (openerWidth + blockWidth * i), buildingMenu.transform.position.y , buildingMenu.transform.position.z);
        //    structButtons[i] = Instantiate(buildingMenuBlockPre, pos, transform.rotation);
        //    structButtons[i].transform.parent = buildGrid.transform;
        //    structButtons[i].GetComponent<Image>().sprite = structures[i].GetComponent<SpriteRenderer>().sprite;
        //    structButtons[i].name = "StructButton " + i;
        //    structButtons[i].GetComponent<StructMenuBlock>().blockNo = i;
        //    structButtons[i].GetComponent<StructMenuBlock>().structMenu = this;
        //}

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                Vector3 pos = new Vector3(gameObject.transform.position.x - 100 + (100 * x), gameObject.transform.position.y + 100 + (100 * y), gameObject.transform.position.z);
                structButtons[x,y] = Instantiate(buildingMenuBlockPre, pos, transform.rotation);
                structButtons[x,y].transform.parent = buildGrid.transform;
                structButtons[x, y].GetComponent<StructMenuBlock>().blockNo = curBlockNo;
                if (structures.Length > curBlockNo)
                {
                    structButtons[x, y].GetComponent<Image>().sprite = structures[curBlockNo].GetComponent<SpriteRenderer>().sprite;
                }
                structButtons[x,y].name = "StructButton " + x + ", "+ y;
                structButtons[x,y].GetComponent<StructMenuBlock>().structMenu = this;
                curBlockNo += 1;
            }

        }

        

    }
    public void PressStructureMenu()
    {
        if(menuVisible)
        {
            HideStructMenu();
        }
        else
        {
            ShowStructMenu();
        }
        
    }
    public void SelectToDrag(int blockNo)
    {
        dragNDrop.ShowToDrag(structures[blockNo]);
        HideStructMenu();
    }
    public void HideStructMenu()
    {
        buildGrid.active = false;
        //for (int i = 0; i < structButtons.Length; i++)
        //{
        //    structButtons[i].active = false;
        //}
        menuVisible = false;
    }
    public void ShowStructMenu()
    {
        buildGrid.active = true;
        //for (int i = 0; i < structButtons.Length; i++)
        //{
        //    structButtons[i].active = true;
        //}
        menuVisible = true;
    }
}
