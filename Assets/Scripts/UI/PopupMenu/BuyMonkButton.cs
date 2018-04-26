using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMonkButton : MonoBehaviour
{
    GameObject canvas;
    GameObject clickedObject;
    GameManager gameManager;
    string buildingName;
    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("PopupMenuCanvas");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        buildingName = clickedObject.GetComponent<Structure>().name;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
