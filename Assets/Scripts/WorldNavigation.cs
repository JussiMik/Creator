using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNavigation : MonoBehaviour
{

    public Vector3 startDragPos;
    public Vector3 dragOffset;
    public Vector3 startCamPos;


    private Vector3 velocity = Vector3.zero;
    private Vector3 curPos;
    private Vector3 prePos;

    public bool navOn = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        curPos = Camera.main.transform.position;
        velocity = (curPos - prePos) / Time.deltaTime;
        prePos = curPos;

    }

    public void OnTouchDown()
    {

        startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startCamPos = Camera.main.transform.position;


    }
    public void OnTouchUp()
    {
        //startDragPos = Input.mousePosition;
    }
    public void OnTouchStay()
    {

        Debug.DrawLine(startDragPos, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        dragOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDragPos;
        if (!(Camera.main.transform.position == startCamPos - dragOffset))
        {
            Vector3 newTarget = startCamPos - dragOffset;
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, newTarget, ref velocity, 0.1f);
        }

    }
    public void ResetValues()
    {
        startDragPos = Vector3.zero;
        dragOffset = Vector3.zero;
        //startCamPos = Vector3.zero;
    }
}
