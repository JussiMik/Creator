using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour {

    public bool dragging = false;
    public GameObject toDrag = null;

    // Use this for initialization
    void Start()
    {
        toDrag = Instantiate(new GameObject(), Input.mousePosition, transform.transform.rotation);
        toDrag.name = "To Drag";
        toDrag.SetActive(false);
        toDrag.AddComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toDrag.active == false && dragging == true)
        {
            toDrag.SetActive(true);
        }
       
        if (Vector2.Distance(Input.mousePosition, toDrag.transform.position) > 0.01f && dragging)
        {
            toDrag.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
    public void StartDragging(GameObject structure)
    {
        toDrag.GetComponent<SpriteRenderer>().sprite = structure.GetComponent<SpriteRenderer>().sprite;
        dragging = true;
    }
}
