using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingNameText : MonoBehaviour
{
    GameObject clickedObject;
    GameObject canvas;
    public string name;

    void Start()
    {
        canvas = GameObject.Find("PopupMenuCanvas");
        
    }
    private void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        name = clickedObject.GetComponent<Structure>().name;
    }

    void Update()
    {

    }
}
