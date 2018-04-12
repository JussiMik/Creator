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

    public Color emptyColor;
    public Color draCanBuildColor;
    public Color draCantBuildColor;

    public Vector2 buildingSize;

    private GameObject curDraBuilding;
    public Vector3 bestTarget;

    public List<Vector2> toBeColorized;

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

        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                Vector3 current = new Vector3(positions[x, y].x + layoutManager.tileWidth / 2, positions[x, y].y, transform.position.z);

                if (Vector3.Distance(bestTarget, v3) > Vector3.Distance(current, v3))
                {
                    bestTarget = current;
                    SwipeToEmpty();
                    for (int bx = 0; bx < buildingSize.x; bx++)
                    {
                        for (int by = 0; by < buildingSize.y; by++)
                        {
                            toBeColorized.Add(new Vector2(x + bx, y + by));
                        }
                    }
                }
            }
        }


        //COLORIZE CAN BUILD TILES
        if (!(currentColored == bestTarget))
        {
            for (int i = 0; i < toBeColorized.Count; i++)
            {
                layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y].GetComponent<SpriteRenderer>().color = draCanBuildColor;
            }
            currentColored = bestTarget;
            firstRound = false;
        }
    }
    private void PlaceBuilding()
    {
        layoutManager.SpawnStructure(curDraBuilding, toBeColorized, bestTarget, new Vector2(2, 2));
        StopDragging();
        SwipeToEmpty();
    }

    /// <summary>
    /// SWIPES COLORIZED
    /// </summary>
    private void SwipeToEmpty()
    {

        for (int i = 0; i < toBeColorized.Count; i++)
        {
            layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y].GetComponent<SpriteRenderer>().color = emptyColor;
        }
        toBeColorized.Clear();
    }
}
