using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public GameObject clickedObject;
    public GameObject popupPanel;
    public BuyMonkButton buyMonkButton;
    public MonkCostText monkCostText;
    GameObject gameManager;
    Structure structure;
    public float xOffset, yOffset;
    public string name;
    public string type;
    public string clickedObjectName;
    public int level;
    public int levelupCost;
    public bool showPanel;
    void Awake()
    {
        popupPanel = GameObject.Find("PopupPanel");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        structure = gameManager.GetComponent<Structure>();
        showPanel = true;
        clickedObjectName = structure.name;
        // buyMonkButton = gameObject.transform.Find("BuyMonkButton").GetComponent<BuyMonkButton>();
        // monkCostText = gameObject.transform.Find("MonkCostText").GetComponent<MonkCostText>();

    }

    public void PanelStuff()
    {
        if (popupPanel.activeSelf == true)
        {
            popupPanel.SetActive(false);
        }
        popupPanel.SetActive(true);
        Vector3 offset = new Vector3(xOffset, yOffset, 0);
        popupPanel.transform.position = Input.mousePosition + offset;
        popupPanel.GetComponent<PopupMenuPanel>().CheckPosition();
        if(clickedObjectName != "MysticPlace")
        {
            buyMonkButton.gameObject.SetActive(false);
            monkCostText.enabled = false;
        }
        if(clickedObjectName == "MysticPlace")
        {
            buyMonkButton.gameObject.SetActive(true);
            monkCostText.enabled = true;
        }
    }
}
