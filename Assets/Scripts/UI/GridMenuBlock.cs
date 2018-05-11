using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMenuBlock : MonoBehaviour {

    
    public GameObject upperMenu;
    public int blockNo = 666;
         
	// Use this for initialization
	void Start () {

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
	
	void TaskOnClick()
    {
        //if ((upperMenu.proActive && upperMenu.buildingsPro.Length -1 >= blockNo) || (!upperMenu.proActive && upperMenu.buildingsUti.Length -1 >= blockNo))
        {
            upperMenu.SendMessage("ShowInfo", (blockNo));
        }
    }
}
