using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesOrNoButton : MonoBehaviour
{

    //public StructMenu structMenu;
    public bool yesButton = true;
    public bool draggerButton = false;
    public DragNDrop dragNDrop;

    // Use this for initialization
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        if (draggerButton == false)
        {
            if (yesButton)
            {
                dragNDrop.YesButton();
            }
            if (!yesButton)
            {
                dragNDrop.NoButton();
            }
        }
        else
        {
            dragNDrop.StartOrPauseDragging();
        }
    }

    public void SetYesOrNo(bool yes, Sprite spr)
    {
        yesButton = yes;
        gameObject.GetComponent<Image>().sprite = spr;
    }

    public void SetAsDragger(bool yes, Sprite spr)
    {
        draggerButton = yes;
        gameObject.GetComponent<Image>().sprite = spr;
    }
}
