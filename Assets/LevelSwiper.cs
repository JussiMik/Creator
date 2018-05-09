using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwiper : MonoBehaviour {

    Vector3 startDragPos;
    public float xOffset = 0;
    public MountainManager mountainManager;

    public void OnTouchDown()
    {
        Debug.Log("Touch down!");
        startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnTouchStay()
    {
        Debug.Log("Ont  Touch!");
        Vector3 curPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(startDragPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        xOffset =   Camera.main.ScreenToWorldPoint(Input.mousePosition).x - startDragPos.x;
    }

    public void OnTouchUp()
    {
        xOffset = 0;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //mountainManager.SetOffSet(xOffset);
	}
}
