using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float faithMaximumTime;
    public float faithTimer;
    public float woodMaximumTime;
    public float woodTimer;
    Image progressBarForegroundImage;
    Image progressBarBackgroundImage;
    public float percent;
    public GameObject canvas;
    public GameObject clickedObject;
    GameObject FaithBarBackground;
    bool buildingDone;
    string buildingType;


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
        

    }
    void Update()
    {
        if(buildingType == "Production")
        {
            // Buildings are not ready yet
            progressBarForegroundImage.enabled = true;
            progressBarBackgroundImage.enabled = true;
            woodTimer = clickedObject.GetComponent<Structure>().faithTargetTime; // Sue me.
            buildingDone = clickedObject.GetComponent<Structure>().constructingDone; // Times two.

            percent = faithTimer / faithMaximumTime;
            progressBarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);
            if (faithTimer == 10 && buildingDone == true)
            {
                progressBarForegroundImage.fillAmount = 1;
            }
        }

        if (buildingType == "Faith")
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

        if (clickedObject.GetComponent<Structure>().type != "Faith")
        {
            progressBarForegroundImage.enabled = false;
            progressBarBackgroundImage.enabled = false;
        }




    }
}
