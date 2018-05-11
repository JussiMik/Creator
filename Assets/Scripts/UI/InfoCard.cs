using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCard : MonoBehaviour {

    public Menu upperMenu;

    public GameObject infoText;

    // Use this for initialization
    public void CloseInfo()
    {
        gameObject.SetActive(false);
	}

    public void Action()
    {
        if(upperMenu.canTakeAction)
        {
            upperMenu.SelectToDrag();
            gameObject.SetActive(false);
        }
    }
}
