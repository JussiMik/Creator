using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float faithMaximumTime;
    public float faithTimer;
    public float woodTimer;
    public float woodMaximumTime;
    public float rockTimer;
    public float rockMaximumTime;
    Image progressBarForegroundImage;
    Image progressBarBackgroundImage;
    public float percent;
    public GameObject canvas;
    public GameObject clickedObject;
    GameObject FaithBarBackground;
    bool buildingDone;
    string buildingType;
    string buildingName;


    // Use this for initialization
    void Awake()
    {
        progressBarForegroundImage = gameObject.GetComponent<Image>();
        FaithBarBackground = GameObject.Find("ProgressBarBackground");
        progressBarBackgroundImage = FaithBarBackground.GetComponent<Image>();
        canvas = GameObject.FindGameObjectWithTag("PopupMenuCanvas");

    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        // When panel is set active get clicked object from PopupMenuCanvas
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        faithMaximumTime = clickedObject.GetComponent<Structure>().originalFaithTargetTime;
        buildingType = clickedObject.GetComponent<Structure>().type;
        buildingName = clickedObject.GetComponent<Structure>().name;


    }
    void Update()
    {
        if (buildingName == "Quarry")
        {
            rockMaximumTime = clickedObject.GetComponent<QuarryCS>().originalRockTime;
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            rockTimer = clickedObject.GetComponent<Structure>().GetComponent<QuarryCS>().rockTime; // Sue me.
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.

            percent = rockTimer / rockMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (rockTimer == 10 && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else if (buildingName == "Wood workshop")
        {
            woodMaximumTime = clickedObject.GetComponent<WoodWorkshopCS>().originalWoodTime;
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            woodTimer = clickedObject.GetComponent<Structure>().GetComponent<WoodWorkshopCS>().woodTime;
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone;

            percent = woodTimer / woodMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (woodTimer == 10 && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else if (buildingType == "Faith")
        {
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            faithTimer = clickedObject.GetComponent<Structure>().faithTargetTime; // Sue me.
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.

            percent = faithTimer / faithMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (faithTimer == 10 && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else
        {
            progressBarForegroundImage.enabled = false;
            progressBarBackgroundImage.enabled = false;
        }





    }
}
