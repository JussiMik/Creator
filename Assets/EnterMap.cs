using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMap : MonoBehaviour {

    public GameObject mapInfo;
    public string mapName;
    public string sceneName;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTouchUp()
    {
        Debug.Log("Enter to the " + mapName);
        mapInfo.GetComponent<MapInfo>().titleText.text = mapName;
        mapInfo.GetComponent<MapInfo>().sceneToBeLoaded = sceneName;
        mapInfo.SetActive(true);
    }
    
}
