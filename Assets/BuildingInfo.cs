using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour {

    public BuildingMenu buildingMenu;

    public GameObject infoText;

    private void Start()
    {
        buildingMenu = GameObject.Find("BuildingMenu").GetComponent<BuildingMenu>();
    }

    // Use this for initialization
    public void CloseInfo()
    {
        gameObject.SetActive(false);  
	}

    public void Select()
    {
        if(buildingMenu.canBuild)
        {
            buildingMenu.SelectToDrag();
            gameObject.SetActive(false);
        }
        else
        {

        }
    }
}
