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

    public bool firstRound;
    public Vector3 currentColored;
    public List<Vector2> currentColoredXY;

    private Color emptyColor;
    public Color draCanBuildColor;
    public Color draCantBuildColor;

    private Vector2 buildingSize = new Vector2(2, 2);

    private GameObject curDraBuilding;
    private Vector3 bestTarget;

    // Use this for initialization
    void Start()
    {
        layoutManager = gameObject.GetComponent<LayoutManager>();
        emptyColor = layoutManager.emptyGo.GetComponent<SpriteRenderer>().color;
        toDrag = Instantiate(new GameObject(), Input.mousePosition, transform.transform.rotation);
        toDrag.transform.parent = GameObject.Find("Canvas").transform;
        toDrag.AddComponent<Image>();
        toDrag.GetComponent<Image>().color = new Color(1, 1, 1, 0.85f);
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

        //if (Input.GetMouseButtonUp(1) && dragging)
        //{
        //    StopDragging();
        //}

        if (dragging)
        {
            Dragging();
        }

        if (Input.GetMouseButtonUp(1) && dragging && firstRound == false)
        {
            PlaceBuilding();
        }

    }
    public void StartDragging(GameObject structure)
    {
        curDraBuilding = structure;
        toDrag.SetActive(true);
        toDrag.GetComponent<Image>().sprite = structure.GetComponent<SpriteRenderer>().sprite;

        firstRound = true;
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
        //float closestDistanceSqr = Mathf.Infinity;
        foreach (Vector3 potentialTarget in positions)
        {
            if (Vector3.Distance(bestTarget, v3) > Vector3.Distance(potentialTarget, v3))
            {
                bestTarget = potentialTarget;
            }
        }

        //
        if (!(currentColored == bestTarget) && firstRound == false)
        {
            SwipeToEmpty();
        }

        if (!(currentColored == bestTarget))
        {
            for (int x = 0; x < layoutManager.testGrid.GetLength(0); x++)
            {
                for (int y = 0; y < layoutManager.testGrid.GetLength(1); y++)
                {
                    if (!(layoutManager.testGrid[x, y] == null))
                    {
                        if (layoutManager.testGrid[x, y].transform.position.x == bestTarget.x && layoutManager.testGrid[x, y].transform.position.y == bestTarget.y)
                        {
                            for (int x2 = 0; x2 < buildingSize.x; x2++)
                            {
                                for (int y2 = 0; y2 < buildingSize.y; y2++)
                                {
                                    layoutManager.testGrid[x + x2, y + y2].GetComponent<SpriteRenderer>().color = draCanBuildColor;
                                    currentColoredXY.Add(new Vector2(x + x2, y + y2));
                                }
                            }
                            currentColored = bestTarget;
                            firstRound = false;
                        }
                    }
                }
            }
        }
    }
    private void PlaceBuilding()
    {
        layoutManager.SpawnStructure(curDraBuilding, currentColoredXY, bestTarget, new Vector2(2, 2));
        StopDragging();
        SwipeToEmpty();
    }

    private void SwipeToEmpty()
    {

        for (int i = 0; i < currentColoredXY.Count; i++)
        {
            layoutManager.testGrid[(int)currentColoredXY[i].x, (int)currentColoredXY[i].y].GetComponent<SpriteRenderer>().color = emptyColor;
        }
        currentColoredXY.Clear();

    }
}
