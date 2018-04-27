using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenuBlock : MonoBehaviour {

    
    public BuildingMenu buildingMenu;
    public int blockNo;
         
	// Use this for initialization
	void Start () {

        buildingMenu = GameObject.Find("BuildingMenu").GetComponent<BuildingMenu>();
        Button btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);

    }
	
	void TaskOnClick()
    {
        buildingMenu.SelectToDrag(blockNo);
    }
}
