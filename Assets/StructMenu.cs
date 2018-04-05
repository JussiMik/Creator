using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenu : MonoBehaviour {

    public GameObject[] structures;
    public GameObject[] structButtons;
    public GameObject buildingMenuBlockPre;
    public GameObject buildingMenu;

    public bool menuVisible = false;

    void Awake()
    {
        //INSTANTIATE STRUCTURE MENU
        structButtons = new GameObject[structures.Length];
        float blockWidth = buildingMenuBlockPre.GetComponent<RectTransform>().rect.width;
        float openerWidth = buildingMenu.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < structures.Length; i++)
        {
            Vector3 pos = new Vector3 (buildingMenu.transform.position.x - (openerWidth + blockWidth * i), buildingMenu.transform.position.y , buildingMenu.transform.position.z);
            structButtons[i] = Instantiate(buildingMenuBlockPre, pos, transform.rotation);
            structButtons[i].transform.parent = buildingMenu.transform;
            structButtons[i].SetActive(false);
            structButtons[i].GetComponent<Image>().sprite = structures[i].GetComponent<SpriteRenderer>().sprite;
        }
        
    }
    public void PressStructureMenu()
    {
        if(menuVisible)
        {
            for (int i = 0; i < structButtons.Length; i++)
            {
                structButtons[i].active = false;
            }
            menuVisible = false;
        }
        else
        {
            for (int i = 0; i < structButtons.Length; i++)
            {
                structButtons[i].active = true;
            }
            menuVisible = true;
        }
    }
}
