using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenuPanel : MonoBehaviour
{
    RectTransform rt;
    bool overX, overY;
    float rtX;
    float rtY;
    // Use this for initialization
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    void Update()
    {

    }
    public void CheckPosition()
    {
        // OffsetMax value is inversed in code. Top value 100 in editor is -100 in script while using offsetMax (so same as Bottom value). Left value 100 in editor is -100 in script while using offsetMax(so same as Right value).
        // Check if panel's Rect Transform top value is over the screen. 
        if (rt.offsetMax.y >= 65f)
        {
            overY = true;
            rtY = 65f;
        }
        // Check if panel's Rect Transform top value is under the screen.
        if (rt.offsetMax.y <= -151f)
        {
            overY = true;
            rtY = -150f;
        }
        // Check if panel's Rect Transform left value is too much right.
        if (rt.offsetMax.x <= -150f)
        {
            overX = true;
            rtX = -200f;
        }
        // Check if panel's Rect Transform left value is too much left.
        if (rt.offsetMax.x >= 200f)
        {
            overX = true;
            rtX = 200f;
        }
        SetPosition();
    }
    void SetPosition()
    {
        // If horizontal and vertical positions are over the screen set x & y values from CheckPosition() and change position
        if (overY == true && overX == true)
        {
            Vector2 newPos = new Vector2(rtX, rtY);
            transform.localPosition = newPos;
            overX = false;
            overY = false;
        }
        // If vertical position is over the screen only change y value
        if (overY == true)
        {
            Vector2 newPos = new Vector2(transform.localPosition.x, rtY);
            transform.localPosition = newPos;
            overY = false;
        }
        // If horizontal position is over the screen only change x value
        if (overX == true)
        {
            Vector2 newPos = new Vector2(rtX, transform.localPosition.y);
            transform.localPosition = newPos;
            overX = false;
        }

    }
}
