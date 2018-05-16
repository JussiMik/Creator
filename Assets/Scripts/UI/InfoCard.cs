using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCard : MonoBehaviour {

    public Menu currentMenu;

    public GameObject infoText;

    // Use this for initialization
    public void CloseInfo()
    {
        gameObject.SetActive(false);
	}

    public void Action()
    {
        if(currentMenu.canTakeAction)
        {
            currentMenu.SelectToDrag();
            gameObject.SetActive(false);
        }
    }
}
