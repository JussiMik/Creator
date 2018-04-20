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
    void Awake()
    {
        popupPanel = GameObject.Find("PopupPanel");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        structure = gameManager.GetComponent<Structure>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hitInfo)
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    clickedObject = hitInfo.transform.gameObject;
                    //GetClickedObjectInfo();
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
        }
    }

    // I'll make the UI better someday :(
    /*  void GetClickedObjectInfo()
     {
         name = clickedObject.GetComponent<Structure>().name;
         type = clickedObject.GetComponent<Structure>().type;
         level = clickedObject.GetComponent<Structure>().level;
         levelupCost = clickedObject.GetComponent<Structure>().levelUpCost;
     }
     */
}
