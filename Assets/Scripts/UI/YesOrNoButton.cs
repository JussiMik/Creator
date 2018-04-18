using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesOrNoButton : MonoBehaviour {

    //public StructMenu structMenu;
    public bool yesButton = true;
    public DragNDrop dragNDrop;
         
	// Use this for initialization
	void Start () {


        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	public void TaskOnClick()
    {
        if(yesButton)
        {
            dragNDrop.YesButton();
        }
        if (!yesButton)
        {
            dragNDrop.NoButton();
        }
    }

    public void SetYesOrNo(bool yes, Sprite spr)
    {
        yesButton = yes;
        gameObject.GetComponent<Image>().sprite = spr; 
    }
}
