using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    Button closeButton;
    GameObject popupMenuPanel;
    void Start()
    {
        popupMenuPanel = GameObject.Find("PopupPanel");
        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(CloseWindow); 
    }

    public void CloseWindow()
    {
        popupMenuPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(CloseWindow);
    }
}
