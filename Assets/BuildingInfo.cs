using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfo : MonoBehaviour {

    [Header("Producal Infos")]
    public string woodWorkshopInfo;
    public string quarryInfo;
    public string farmInfo;
    public string shrineInfo;
    public string statueInfo;
    public string templeInfo;

    [Header("Utility Infos")]
    public string gardenInfo;
    public string meditationRoomInfo;
    public string dormitoryInfo;

    public BuildingMenu buildingMenu;

    public GameObject infoText;
    

    // Use this for initialization
    public void CloseInfo (int no)
    {
        gameObject.SetActive(false);  
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
