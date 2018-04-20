using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public GameObject clickedObject;
    GameObject canvas;
    Button upgradeButton;
    GameObject buildingNameTextObject;
    public bool levelUp;
    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.Find("PopupMenuCanvas");
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(UpgradeBuilding);
    }

    private void Start()
    {
        buildingNameTextObject = GameObject.Find("Name & Level text");
    }
    private void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
    }
   public void UpgradeBuilding()
    {
        clickedObject.GetComponent<Structure>().lvlChange = true;
        buildingNameTextObject.GetComponent<BuildingNameText>().FindInfo();

    }
    private void OnDestroy()
    {
        upgradeButton.onClick.RemoveListener(UpgradeBuilding);
    }
}
