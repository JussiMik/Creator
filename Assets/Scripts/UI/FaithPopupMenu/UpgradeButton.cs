using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public GameObject clickedObject;
    GameObject canvas;
    Button upgradeButton;
    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.Find("PopupMenuCanvas");
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(UpgradeBuilding);
    }

    private void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
    }
   public void UpgradeBuilding()
    {
        clickedObject.GetComponent<Structure>().lvlChange = true;
    }
    private void OnDestroy()
    {
        upgradeButton.onClick.RemoveListener(UpgradeBuilding);
    }
}
