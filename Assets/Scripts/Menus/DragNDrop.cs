using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour
{
    public Vector3 v3;
    public bool dragging = false;
    public GameObject toDrag = null;

    public GameObject yesOrNoPre;
    public GameObject yesButton;
    public GameObject noButton;
    public Sprite yesButtonSpr;
    public Sprite noButtonSpr;
    public Sprite draggerButtonSpr;

    public LayoutManager layoutManager;
    public Grid grid;

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

    public Vector3 toDragPos;

    //
    private GameObject worldNavigation;

    public bool buildingMode = true;

    [SerializeField]
    private bool allow;

    // Use this for initialization
    void Start()
    {
        layoutManager = gameObject.GetComponent<LayoutManager>();
        emptyColor = layoutManager.emptyPre.GetComponent<SpriteRenderer>().color;
        worldNavigation = GameObject.Find("WorldNavigation");


        //INSTANTIATE OBJECT TO DRAG
        //This shows the structure that you're dragging around
        toDrag = Instantiate(new GameObject(), Input.mousePosition, transform.transform.rotation);
        toDrag.transform.parent = GameObject.Find("DragCanvas").transform;
        toDrag.AddComponent<Image>();
        toDrag.GetComponent<Image>().color = new Color(1, 1, 1, 0.85f);
        toDrag.name = "To Drag";
        toDrag.SetActive(false);

        //Here we spawn a "Yes" button that follows ToDrag object around and will be pressed 
        //if we want to build to that selected spot.
        Vector2 yesPos = new Vector3(toDrag.GetComponent<RectTransform>().position.x - 40, toDrag.GetComponent<RectTransform>().position.y - 50);
        yesButton = Instantiate(yesOrNoPre, yesPos, transform.rotation);
        yesButton.transform.SetParent(toDrag.transform);
        yesButton.GetComponent<YesOrNoButton>().SetYesOrNo(true, yesButtonSpr);
        yesButton.GetComponent<YesOrNoButton>().dragNDrop = this;

        //Here we spawn "No" button
        Vector2 noPos = new Vector3(toDrag.GetComponent<RectTransform>().position.x + 40, toDrag.GetComponent<RectTransform>().position.y - 50);
        noButton = Instantiate(yesOrNoPre, noPos, transform.rotation);
        noButton.transform.SetParent(toDrag.transform);
        noButton.GetComponent<YesOrNoButton>().SetYesOrNo(false, noButtonSpr);
        noButton.GetComponent<YesOrNoButton>().dragNDrop = this;

        //Here we spawn dragger button
        Vector2 dragPos = new Vector3(toDrag.GetComponent<RectTransform>().position.x, toDrag.GetComponent<RectTransform>().position.y - 100);
        noButton = Instantiate(yesOrNoPre, dragPos, transform.rotation);
        noButton.transform.SetParent(toDrag.transform);
        noButton.GetComponent<YesOrNoButton>().SetAsDragger(true, draggerButtonSpr);
        noButton.GetComponent<YesOrNoButton>().dragNDrop = this;
    }

    // Update is called once per frame
    void Update()
    {
        

        //if (Input.GetMouseButtonUp(1) && dragging)
        //{
        //    StopDragging();
        //}

        if (dragging)
        {
            toDragPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 100, Input.mousePosition.z);
            if (Vector2.Distance(toDragPos, toDrag.transform.position) > 0.01f)
            {
                toDrag.GetComponent<RectTransform>().position = toDragPos;
            }

            Dragging();
        }

        if (Input.GetMouseButtonUp(1) && dragging && firstRound == false && allow == true)
        {
            PlaceBuilding();
        }

    }

    public void YesButton()
    {
        if (dragging == false && firstRound == false && allow == true)
        {
            PlaceBuilding();
        }
    }

    public void NoButton()
    {
        StopDragging();
    }

    //USED WHILE PLACING A BUILDING
    public void ShowToDrag(GameObject structure)
    {
        worldNavigation.GetComponent<WorldNavigation>().SetUnActive();
        buildingSize = structure.GetComponent<Structure>().sizeOnGrid;
        layoutManager.SetTestGridActive(true);
        curDraBuilding = structure;
        toDrag.GetComponent<Image>().sprite = structure.GetComponent<SpriteRenderer>().sprite;
        toDrag.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        toDrag.SetActive(true);
    }

    //USED WHILE USING MIRACLE
    public void ShowToDrag()
    {
        worldNavigation.GetComponent<WorldNavigation>().SetUnActive();
        buildingSize = new Vector2(2,2);
        layoutManager.SetTestGridActive(true);
        toDrag.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        toDrag.SetActive(true);
    }



    public void StartDragging()
    {
        //bool checke1st = false;

        if (!dragging)
        {
            firstRound = true;
            dragging = true;
            Debug.Log("to true");
            worldNavigation.SetActive( false);
        }
    }

    public void PauseDragging()
    {
        dragging = false;
        //worldNavigation.navOn = true;
        Debug.Log("to false");
    }

    public void StopDragging()
    {
        dragging = false;
        toDrag.SetActive(false);
        layoutManager.SetTestGridActive(false);
        worldNavigation.SetActive(true);

    }
    public void Dragging()
    {
        //GET MOUSE WORLD POSITION
        v3 = Input.mousePosition;
        v3.z = 10.0f;
        //Add dragger between
        v3 = new Vector3(v3.x, v3.y + 100, v3.z);
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
            allow = true;

            //TEST IF SPACES ARE TAKEN
            for (int i = 0; i < toBeColorized.Count; i++)
            {
                if (layoutManager.positions[(int)toBeColorized[i].x, (int)toBeColorized[i].y].z == 1 || layoutManager.positions[(int)toBeColorized[i].x, (int)toBeColorized[i].y].z == 2)
                {
                    allow = false;
                }
            }

            //IF SPACES ARE NOT TAKEN SET THEM GREEN
            if (allow)
            {
                for (int i = 0; i < toBeColorized.Count; i++)
                {
                    if (!(layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y] == null))
                    {
                        layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y].GetComponent<SpriteRenderer>().color = draCanBuildColor;
                    }     
                }
            }

            //IF EVEN ONE OF SPACES ARE TAKEN SET THEM RED
            else
            {
                for (int i = 0; i < toBeColorized.Count; i++)
                {
                    if(!(layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y] == null))
                    {
                        layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y].GetComponent<SpriteRenderer>().color = draCantBuildColor;
                    }
                }
            }

            currentColored = bestTarget;
            firstRound = false;
        }
    }
    private void PlaceBuilding()
    {
        if(buildingMode)
        {
            layoutManager.SpawnStructure(curDraBuilding, toBeColorized, new Vector2(2, 2));
            grid.CreateGrid();
            StopDragging();
            SwipeToEmpty();
            worldNavigation.SetActive(true);
        }

       
    }

    /// SWIPES COLORIZED
    private void SwipeToEmpty()
    {

        for (int i = 0; i < toBeColorized.Count; i++)
        {
            
            {
                if (!(layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y] == null))
                {
                    layoutManager.testGrid[(int)toBeColorized[i].x, (int)toBeColorized[i].y].GetComponent<SpriteRenderer>().color = emptyColor;
                }
            }   
        }
        toBeColorized.Clear();
    }
}
