using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMonkButton : MonoBehaviour
{
    GameObject canvas;
    GameObject clickedObject;
    GameManager gameManager;
    Button buyMonkButton;
    string buildingName;
    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("PopupMenuCanvas");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        buyMonkButton = GetComponent<Button>();
        buyMonkButton.onClick.AddListener(BuyMonk);
    }
    private void OnEnable()
    {
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        buildingName = clickedObject.GetComponent<Structure>().name;
    }
    private void OnDisable()
    {
        buyMonkButton.onClick.RemoveListener(BuyMonk);
    }
    public void BuyMonk()
    {
        clickedObject.GetComponent<MysticPlaceCS>().SpawnNewMonk();
    }
}
