using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public GameObject popupMenu;
    GameObject panel;
    public float xOffset, yOffset;
    void Awake()
    {
        popupMenu = GameObject.Find("PopupMenuCanvas");
        panel = GameObject.Find("PopupPanel");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hitInfo)
            {
                Debug.Log(hitInfo.transform.gameObject.name);
                if (hitInfo.transform.gameObject.tag == "Building")
                {
                    popupMenu.SetActive(true);
                    Vector3 offset = new Vector3(xOffset, yOffset, 0);
                    panel.transform.position = Input.mousePosition + offset;
                }
            }

        }

    }
}
