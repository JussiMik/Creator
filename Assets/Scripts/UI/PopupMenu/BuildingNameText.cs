using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingNameText : MonoBehaviour
{
    GameObject clickedObject;
    GameObject canvas;
    public string name;
    public int level;
    Text nameText;

    void Awake()
    {
        canvas = GameObject.Find("PopupMenuCanvas");
        nameText = gameObject.GetComponent<Text>();
    }
    private void OnEnable()
    {
        FindInfo();
    }

    void Update()
    {
       ShowNameAndLevel();
    }
    public void ShowNameAndLevel()
    {
        if (clickedObject != null)
        {
            level = clickedObject.GetComponent<Structure>().level;
            nameText.text = name + " LVL. " + level;
        }
    }
    public void FindInfo()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        name = clickedObject.GetComponent<Structure>().name;
        level = clickedObject.GetComponent<Structure>().level;
    }
    public void RefreshInfo()
    {

    }
}
