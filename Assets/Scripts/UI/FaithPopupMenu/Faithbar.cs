using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Faithbar : MonoBehaviour
{
    public float maximumTime;
    public float faithTimer;
    Image faithbarForeground;
    float percent;
    GameObject canvas;
    public GameObject clickedObject;

    // Use this for initialization
    void Start()
    {
        faithbarForeground = gameObject.GetComponent<Image>();
        canvas = GameObject.Find("PopupMenuCanvas");
    }
    private void OnEnable()
    {
        // When panel is set active get clicked object from PopupMenuCanvas
        clickedObject = canvas.GetComponent<PopupMenu>().clickedObject;
        maximumTime = clickedObject.GetComponent<ShrineCS>().originalFaithTargetTime;
    }
    void Update()
    {
        faithTimer = clickedObject.GetComponent<ShrineCS>().faithTargetTime; // Sue me.
        percent = faithTimer / maximumTime;
        faithbarForeground.fillAmount = Mathf.Lerp(1, 0, percent);
    }
}
