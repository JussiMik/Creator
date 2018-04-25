using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public GameObject clickedObject;
    public GameObject popupPanel;
    GameManager gameManager;
    Structure structure;
    public float xOffset, yOffset;
    public string name;
    public string type;
    public int level;
    public int levelupCost;
    public bool showPanel;
    void Awake()
    {
        popupPanel = GameObject.Find("PopupPanel");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        structure = gameManager.GetComponent<Structure>();
        showPanel = true;
    }

    void Update()
    {
        /*  if (Input.GetMouseButtonDown(0))
          {
              Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
              RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

              if (hitInfo)
              {
                  if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Building") && showPanel == true)
                  {
                      clickedObject = hitInfo.transform.gameObject;
                      //GetClickedObjectInfo(); */

    }



    public void PanelStuff()
    {
        if (popupPanel.activeSelf == true)
        {
            popupPanel.SetActive(false);
        }
        popupPanel.SetActive(true);
        Vector3 offset = new Vector3(xOffset, yOffset, 0);
        popupPanel.transform.position = Input.mousePosition + offset;
        popupPanel.GetComponent<PopupMenuPanel>().CheckPosition();
    }
}
