using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructMenuBlock : MonoBehaviour {

    
    public StructMenu structMenu;
    public int blockNo;
         
	// Use this for initialization
	void Start () {
        
        Button btn = gameObject.GetComponent<Button>();

        btn.onClick.AddListener(TaskOnClick);
    }
	
	void TaskOnClick()
    {
        structMenu.SelectToDrag(blockNo);
    }
}
