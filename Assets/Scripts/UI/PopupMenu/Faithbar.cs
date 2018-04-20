using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Faithbar : MonoBehaviour
{
    public float maximumTime;
    public float faithTimer;
    Image faithbarForegroundImage;
    Image faithbarBackgroundImage;
    public float percent;
    public GameObject canvas;
    public GameObject clickedObject;
    GameObject FaithBarBackground;
    bool buildingDone;
    string buildingType;


    // Use this for initialization
    void Awake()
    {
        faithbarForegroundImage = gameObject.GetComponent<Image>();
        FaithBarBackground = GameObject.Find("FaithbarBackground");
        faithbarBackgroundImage = FaithBarBackground.GetComponent<Image>();
        canvas = GameObject.FindGameObjectWithTag("PopupMenuCanvas");

    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        // When panel is set active get clicked object from PopupMenuCanvas
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        maximumTime = clickedObject.GetComponent<Structure>().originalFaithTargetTime;
        buildingType = clickedObject.GetComponent<Structure>().type;
        

    }
    void Update()
    {
        if(buildingType == "Production")
        {
            // Buildings are not ready yet
            faithbarForegroundImage.enabled = false;
            faithbarBackgroundImage.enabled = false;
        }

        if (buildingType == "Faith")
        {
            faithbarForegroundImage.enabled = true;
            faithbarBackgroundImage.enabled = true;
            faithTimer = clickedObject.GetComponent<Structure>().faithTargetTime; // Sue me.
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.

            percent = faithTimer / maximumTime;
            faithbarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (faithTimer == 10 && buildingDone == true)
            {
                faithbarForegroundImage.fillAmount = 1;
            }
        }

        if (clickedObject.GetComponent<Structure>().type != "Faith")
        {
            faithbarForegroundImage.enabled = false;
            faithbarBackgroundImage.enabled = false;
        }




    }
}
