﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    [SerializeField] public bool canTakeAction = true;
    [SerializeField] public GameManager gameManager;

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
    public virtual void CheckResources(GameObject[] otherBuildingsList)
    {
        
    }
    public virtual void SelectToDrag()
    {

    }

}
