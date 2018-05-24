using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapInfo : MonoBehaviour {

    public Text titleText;
    public Text infoText;
    public string sceneToBeLoaded;
    public TouchInput touchInput;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ExitButton()
    {
        gameObject.SetActive(false);
        touchInput.enabled = true;
    }
    public void StartButton()
    {
        Debug.Log("Loaded " + sceneToBeLoaded);
        SceneManager.LoadScene(sceneToBeLoaded);
    }
    public void OnEnable()
    {
        touchInput.enabled = false;
    }
}
