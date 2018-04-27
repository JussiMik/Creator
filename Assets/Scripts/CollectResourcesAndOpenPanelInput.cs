using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResourcesAndOpenPanelInput : MonoBehaviour
{
    public PopupMenu popupMenu;
    public GameManager gameManager;
    public GameObject clickedBuilding;
    public bool showPanel;
    public LayerMask collisionMask;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        popupMenu = GameObject.FindGameObjectWithTag("PopupMenuCanvas").GetComponent<PopupMenu>();
        showPanel = true;
        // Physics2D.IgnoreLayerCollision(0, 13);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            {
                Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

                if (hitInfo == true)
                {
                    Debug.Log(hitInfo.collider.name);
                    clickedBuilding = hitInfo.transform.gameObject;
                    if (clickedBuilding.layer == LayerMask.NameToLayer("Building"))
                    {
                        GameObject.FindGameObjectWithTag("PopupMenuCanvas").GetComponent<PopupMenu>().clickedObject = hitInfo.transform.gameObject;
                        if ((hitInfo.transform.tag == "FaithBuilding" || hitInfo.transform.tag == "Shrine") && gameManager.devotion >= gameManager.minDevotionAmountCollecting)
                        {
                            clickedBuilding = hitInfo.transform.gameObject;

                            if (clickedBuilding.GetComponent<Structure>().generatedFaith > 0)
                            {
                                clickedBuilding.GetComponent<Structure>().CollectFaith();
                            }
                        }

                        if (hitInfo.transform.tag == "WoodWorkshop" && gameManager.devotion >= gameManager.minDevotionAmountCollecting)
                        {
                            clickedBuilding = hitInfo.transform.gameObject;

                            if (clickedBuilding.GetComponent<WoodWorkshopCS>().gatheredWood > 0)
                            {
                                clickedBuilding.GetComponent<WoodWorkshopCS>().CollectWood();
                            }
                        }

                        if (hitInfo.transform.tag == "Quarry" && gameManager.devotion >= gameManager.minDevotionAmountCollecting)
                        {
                            clickedBuilding = hitInfo.transform.gameObject;

                            if (clickedBuilding.GetComponent<QuarryCS>().gatheredStone > 0)
                            {
                                clickedBuilding.GetComponent<QuarryCS>().CollectStone();
                            }
                        }
                        if (hitInfo.transform.name == "MysticPlace")
                        {
                            clickedBuilding = hitInfo.transform.gameObject;

                            if (clickedBuilding.GetComponent<Structure>().generatedFaith > 0)
                            {
                                clickedBuilding.GetComponent<Structure>().CollectFaith();
                            }
                        }
                        if (showPanel == true)
                        {
                            popupMenu.PanelStuff();
                        }
                        showPanel = true;
                    }

                }
            }
        }
    }
}
