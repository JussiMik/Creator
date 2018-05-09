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
    public float monkTimer;
    public float monkMaximumTime;
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
            rockMaximumTime = clickedObject.GetComponent<QuarryCS>().originalstoneProductionTimeLength;
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            rockTimer = clickedObject.GetComponent<Structure>().GetComponent<QuarryCS>().stoneProductionTimeLength; // Sue me.
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.

            percent = rockTimer / rockMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (rockTimer == rockMaximumTime && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else if (buildingName == "Wood workshop")
        {
            woodMaximumTime = clickedObject.GetComponent<WoodWorkshopCS>().originalWoodTime;
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            woodTimer = clickedObject.GetComponent<Structure>().GetComponent<WoodWorkshopCS>().woodProductionTimeLength;
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone;

            percent = woodTimer / woodMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (woodTimer == woodMaximumTime && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else if (buildingType == "Faith")
        {
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            if (clickedObject != null)
            {
                faithTimer = clickedObject.GetComponent<Structure>().productionCycleLength; // Sue me.
                buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.
            }

            percent = faithTimer / faithMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (faithTimer == faithMaximumTime && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }
        else if (buildingName == "Conversion temple")
        {
            monkMaximumTime = clickedObject.GetComponent<ConversionTempleCS>().originalMonkConversionTimeLength;
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            monkTimer = clickedObject.GetComponent<Structure>().GetComponent<ConversionTempleCS>().monkConversionTimeLength;
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone;

            percent = monkTimer / monkMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (monkTimer == monkMaximumTime && buildingDone == true)
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
