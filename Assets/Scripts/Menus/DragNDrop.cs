using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour
{
    public Vector3 v3;
    public bool dragging = false;
    public GameObject toDrag = null;
    public LayoutManager layoutManager;

    // Use this for initialization
    void Start()
    {
        layoutManager = gameObject.GetComponent<LayoutManager>();
        toDrag = Instantiate(new GameObject(), Input.mousePosition, transform.transform.rotation);
        toDrag.transform.parent = GameObject.Find("Canvas").transform;
        toDrag.AddComponent<Image>();
        toDrag.name = "To Drag";
        toDrag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Input.mousePosition, toDrag.transform.position) > 0.01f && dragging)
        {
            toDrag.GetComponent<RectTransform>().position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1) && dragging)
        {
            StopDragging();
        }
        if (dragging)
        {
            Dragging();
        }
    }
    public void StartDragging(GameObject structure)
    {
        toDrag.SetActive(true);
        toDrag.GetComponent<Image>().sprite = structure.GetComponent<SpriteRenderer>().sprite;
        dragging = true;
    }
    public void StopDragging()
    {
        dragging = false;
        toDrag.SetActive(false);
    }
    public void Dragging()
    {
        //GET MOUSE WORLD POSITION
        v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        //FIND CLOCEST TO CURSOR
        Vector3[,] positions = GameObject.Find("LevelManager").GetComponent<LayoutManager>().positions;
        Vector3 bestTarget = new Vector3(0, 0, 0);
        float closestDistanceSqr = Mathf.Infinity;
        foreach (Vector3 potentialTarget in positions)
        {
            Vector3 directionToTarget = potentialTarget - v3;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        //SET COLOR OFF THE CLOSET ON TEST GRID
        for (int x = 0; x < layoutManager.testGrid.GetLength(0); x++)
        {
            for (int y = 0; y < layoutManager.testGrid.GetLength(1); y++)
            {
                if (layoutManager.testGrid[x, y].transform.position.x == bestTarget.x)
                {
                    if (layoutManager.testGrid[x, y].transform.position.y == bestTarget.y)
                    {
                        layoutManager.testGrid[x, y].GetComponent<SpriteRenderer>().color = Color.cyan;
                    }
                }
            }
        }
    }
}
