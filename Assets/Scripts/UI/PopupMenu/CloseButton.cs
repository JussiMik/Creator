using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    Button closeButton;
    GameObject popupMenuCanvas;
    void Start()
    {
        popupMenuCanvas = GameObject.Find("PopupMenuCanvas");
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(CloseWindow); 
    }

    public void CloseWindow()
    {
        popupMenuCanvas.SetActive(false);
    }
    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(CloseWindow);
    }
}
