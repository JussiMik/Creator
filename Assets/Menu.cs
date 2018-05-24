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

    protected int curBlockNo = 0;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Use this for initialization
<<<<<<< HEAD

=======
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
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

    public virtual void CheckResources(GameObject[] otherBuildingsList)
    {
<<<<<<< HEAD
        var usedScript = otherBuildingsList[curBlockNo].GetComponent<Structure>();

        canTakeAction = true;

        //TEST WOOD
        if (gameManager.wood >= usedScript.woodConstructingCost)
=======
        canTakeAction = true;

        //TEST WOOD
        if (gameManager.wood >= otherBuildingsList[curBlockNo].GetComponent<Structure>().woodConstructingCost)
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
        {
            infoBuildingRes_Wood.color = Color.green;
        }
        else
        {
            infoBuildingRes_Wood.color = Color.red;
            canTakeAction = false;
        }

        //TEST FAITH
<<<<<<< HEAD
        if (gameManager.faith >= usedScript.faithConstructingCost)
=======
        if (gameManager.faith >= otherBuildingsList[curBlockNo].GetComponent<Structure>().faithConstructingCost)
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
        {
            infoBuildingRes_Faith.color = Color.green;
        }
        else
        {
            infoBuildingRes_Faith.color = Color.red;
            canTakeAction = false;
        }

        //TEST STONE
<<<<<<< HEAD
        if (gameManager.stone >= usedScript.stoneConstructingCost)
=======
        if (gameManager.stone >= otherBuildingsList[curBlockNo].GetComponent<Structure>().stoneConstructingCost)
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
        {
            infoBuildingRes_Stone.color = Color.green;
        }
        else
        {
            infoBuildingRes_Stone.color = Color.red;
            canTakeAction = false;
        }

        //TEST DEVOTION
<<<<<<< HEAD
        if (gameManager.devotion >= usedScript.devotionConstructingCost)
=======
        if (gameManager.devotion >= otherBuildingsList[curBlockNo].GetComponent<Structure>().devotionConstructingCost)
>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
        {
            infoBuildingRes_Dev.color = Color.green;
        }
        else
        {
            infoBuildingRes_Dev.color = Color.red;
            canTakeAction = false;
        }

        if (canTakeAction)
        {
            infoBuildingBuild.GetComponent<Image>().color = Color.green;
        }
        else
        {
            infoBuildingBuild.GetComponent<Image>().color = Color.red;
        }
    }

<<<<<<< HEAD
=======

>>>>>>> 6fe9fb00423e5c7737e825b9fe907605e78b39ed
}
