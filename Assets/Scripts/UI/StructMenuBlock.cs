using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenuBlock : MonoBehaviour {

    
    public BuildingMenu buildingMenu;
    public int blockNo = 666;
         
	// Use this for initialization
	void Start () {

        buildingMenu = GameObject.Find("BuildingMenu").GetComponent<BuildingMenu>();
        Button btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);

    }
	
	void TaskOnClick()
    {
        if ((buildingMenu.proActive && buildingMenu.buildingsPro.Length -1 >= blockNo) || (!buildingMenu.proActive && buildingMenu.buildingsUti.Length -1 >= blockNo))
        {
            buildingMenu.ShowInfo(blockNo);
        }
    }
}
