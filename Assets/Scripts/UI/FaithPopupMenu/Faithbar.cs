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
    float percent;
    public GameObject canvas;
    public GameObject clickedObject;
    GameObject FaithBarBackground;
    

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

    }
    void Update()
    {
        if(clickedObject.GetComponent<Structure>().type != "Faith")
        {
            Debug.Log(clickedObject.GetComponent<Structure>().type);
            faithbarForegroundImage.enabled = false;
            faithbarBackgroundImage.enabled = false;
        }
        
        if (clickedObject.GetComponent<Structure>().type == "Faith")
        {
            Debug.Log(clickedObject.GetComponent<Structure>().type);
            faithbarForegroundImage.enabled = true;
            faithbarBackgroundImage.enabled = true;
            faithTimer = clickedObject.GetComponent<ShrineCS>().faithTargetTime; // Sue me.
        }
        percent = faithTimer / maximumTime;
        faithbarForegroundImage.fillAmount = Mathf.Lerp(1, 0, percent);

    }
}
