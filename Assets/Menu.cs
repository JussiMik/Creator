using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    [SerializeField] public bool canTakeAction = true;
    [SerializeField] public GameManager gameManager;
    public GameObject menusGrid;
    public bool menuVisible = false;
    public GameObject worldNavigation;

    [Header("Building Info Stuff")]

    public GameObject infoBuildingSprite;
    public GameObject infoBuildingName;
    public GameObject infoBuildingText;
    public GameObject infoBuildingBuild;


    [Header("Needed Resource Texts")]
    public Text infoBuildingRes_Wood;
    public Text infoBuildingRes_Dev;
    public Text infoBuildingRes_Faith;
    public Text infoBuildingRes_Stone;

    public GameObject infoCard;

    protected int curBlockNo = 0;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Use this for initialization

    public virtual void SelectToDrag()
    {

    }

    //PRESSING THE ACTUAL MENU BUTTON
    protected virtual void PressStructureMenu()
    {
        if (menuVisible)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }

    }

    //SHOW MENU PRESSING THE BUTTON
    public virtual void ShowMenu()
    {
        menusGrid.SetActive(true);
        menuVisible = true;
        worldNavigation.GetComponent<WorldNavigation>().SetUnActive();
    }

    //HIDE MENU PRESSING THE BUTTON
    public virtual void HideMenu()
    {
        menusGrid.SetActive(false);
        menuVisible = false;
        worldNavigation.SetActive(true);
    }

    public virtual void HideMenuNavigationOff()
    {
        menusGrid.SetActive(false);
        menuVisible = false;
        worldNavigation.SetActive(false);
    }



    public virtual void CheckResources(GameObject[] otherBuildingsList)
    {
        var usedScript = otherBuildingsList[curBlockNo].GetComponent<Structure>();

        canTakeAction = true;
        var canColor = Color.black;
        var cantColor = Color.gray;

        Debug.Log("jjjjjjjjjjjjjj"); 
         
        //TEST WOOD
        if (gameManager.wood >= usedScript.woodConstructingCost)
        {
            infoBuildingRes_Wood.color = canColor;
        }
        else
        {
            infoBuildingRes_Wood.color = cantColor;
            canTakeAction = false;
        }

        //TEST FAITH
        if (gameManager.faith >= usedScript.faithConstructingCost)
        {
            infoBuildingRes_Faith.color = canColor;
        }
        else
        {
            infoBuildingRes_Faith.color = cantColor;
            canTakeAction = false;
        }

        //TEST STONE
        if (gameManager.stone >= usedScript.stoneConstructingCost)
        {
            infoBuildingRes_Stone.color = canColor;
        }
        else
        {
            infoBuildingRes_Stone.color = cantColor;
            canTakeAction = false;
        }

        //TEST DEVOTION
        if (gameManager.devotion >= usedScript.devotionConstructingCost)
        {
            infoBuildingRes_Dev.color = canColor;
        }
        else
        {
            infoBuildingRes_Dev.color = cantColor;
            canTakeAction = false;
        }

        if (canTakeAction)
        {
            infoBuildingBuild.GetComponent<Image>().color = Color.white;
        }
        else
        {
            infoBuildingBuild.GetComponent<Image>().color = Color.gray;
        }
    }

}
