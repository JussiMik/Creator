using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public GameObject clickedObject;
    public GameObject DragNDrop;
    public GameObject temple;
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
        Debug.Log("Saatanansaatana");
        clickedObject.GetComponent<Structure>().lvlChange = true;
        if (clickedObject.tag == "Shrine" && clickedObject.GetComponent<Structure>().level == 2 && clickedObject.GetComponent<ShrineCS>().allowTempleConstructing == true)
        {
            Instantiate(temple, clickedObject.transform.position, clickedObject.transform.rotation);
        }
        buildingNameTextObject.GetComponent<BuildingNameText>().FindInfo();

    }
    private void OnDestroy()
    {
        upgradeButton.onClick.RemoveListener(UpgradeBuilding);
    }
}
