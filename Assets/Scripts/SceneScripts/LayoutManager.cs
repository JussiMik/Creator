using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    public Vector3[,] positions;
    //FREE SPACE = 0 | TAKEN = 1 | Rock = 2

    GameObject testGridFolder;
    public GameObject[,] testGrid;
    public Vector3[,] gapPositions;
    [SerializeField]
    public Sprite[] grassTileSprites;
    [SerializeField]
    public Sprite[] rockTileSprites;

    bool gridDone = false;
    public bool roundCorners = false;
    public int roundCornerBy = 1;

    public bool showGrid;

    private GameObject rockFolder;

    //public Vector3 nullVector3 = new Vector3(666, 666, 666);


    public Color fullColor = new Color(255, 255, 255, 255);

    public Color cantBuild;

    public List<GameObject> lakes;
    public int rndLakes = 1;

    public int gridWidth;
    public int gridHeigth;
    public int currentWidth = 0;

    public int startWidth;
    public int startHeight;

    public bool renderGrid = true;

    public bool tileByAngle;
    public float tileHeight = 0.5f;
    public float tileWidth = 1;
    [SerializeField]
    private float gridAngle;

    public float tileCap;

    public GameObject emptyGo;
    public GameObject lakeGo;
    public GameObject treeGo;
    public GameObject centerGo;

    //https://docs.unity3d.com/ScriptReference/GL.html

    public int lineCount = 10;
    public float radius = 3.0f;

    static Material lineMaterial;



    // Use this for initialization
    private void Start()
    {
        gridAngle = 45 * (tileHeight / tileWidth);
        CreateGrid();
    }

    void CreateGrid()
    {
        positions = new Vector3[gridWidth, gridHeigth];
        testGrid = new GameObject[gridWidth, gridHeigth];

        //CALCULATE CENTER OFFSET
        //float centerOffsetX = ((gridWidth * gridHeigth)/4 * tileWidth )/ 2;
        float centerOffsetX = 10;
        float centerOffsetY = 0;

        //CREATE GRID ITSELF
        for (int x = 0; x < gridWidth; x++)
        {

            for (int y = 0; y < gridHeigth; y++)
            {
                positions[x, y] = new Vector3(transform.position.x - centerOffsetX + (tileWidth * x) + (tileWidth * y), transform.position.y - centerOffsetY + (tileHeight * x) - (tileHeight * y), 0);
            }
        }

        //"REMOVE" CORNER POSITIONS
        rockFolder = Instantiate(new GameObject(), transform.position, transform.rotation);
        rockFolder.name = "Rock Folder";
        if (roundCorners)
        {
            for (int i1 = 0; i1 < roundCornerBy; i1++)
            {
                for (int i2 = 0; i2 < roundCornerBy; i2++)
                {
                    if (!(i1 == i2 && i1 == roundCornerBy - 1) || (i1 == 0 && i2 == 0))
                    {
                        positions[i1, i2] = new Vector3(positions[i1, i2].x, positions[i1, i2].y, 2);
                        PlaceRockToPlace(positions[i1, i2], new Vector2(i1, i2));

                        positions[i1, positions.GetLength(1) - 1 - i2] = new Vector3(positions[i1, positions.GetLength(1) - 1 - i2].x, positions[i1, positions.GetLength(1) - 1 - i2].y, 2);
                        PlaceRockToPlace(positions[i1, positions.GetLength(1) - 1 - i2], new Vector2 (i1, positions.GetLength(1) - 1 - i2));

                        positions[positions.GetLength(0) - 1 - i1, i2] = new Vector3(positions[positions.GetLength(0) - 1 - i1, i2].x, positions[positions.GetLength(0) - 1 - i1, i2].y, 2);
                        PlaceRockToPlace(positions[positions.GetLength(0) - 1 - i1, i2], new Vector2(positions.GetLength(0) - 1 - i1, i2));

                        positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2] = new Vector3(positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2].x, positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2].y, 2);
                        PlaceRockToPlace(positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2], new Vector2(positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2));
                    }
                }

            }
        }

        //CREATE TEST GRID
        testGridFolder = Instantiate(new GameObject(), Vector3.zero, transform.rotation);
        testGridFolder.name = "Test Grid";

        for (int x = 0; x < gridWidth; x++)
        {

            for (int y = 0; y < gridHeigth; y++)
            {
                if (positions[x, y].z == 3)
                {
                    testGrid[x, y] = null;
                }
                else
                {
                    Vector3 newPos = new Vector3(positions[x, y].x, positions[x, y].y, transform.position.z);
                    testGrid[x, y] = Instantiate(emptyGo, newPos, Quaternion.identity);
                    testGrid[x, y].transform.parent = testGridFolder.transform;
                    testGrid[x, y].name = x + " , " + y;
                    testGrid[x, y].transform.localScale = new Vector3(tileWidth * 0.35f, (tileHeight * 2) * 0.35f, testGrid[x, y].transform.localScale.z);
                }
            }
        }
        SetTestGridActive(false);
        RandomGen();
        PlaceGrass();
        gridDone = true;
    }

    void Update()
    {
        TestGridUpdate();

        //CALCULATE CAP BETWEEN TILES(UNUSED)
        if (tileCap >= (float)tileWidth / 4 + 0.01f)
        {
            tileCap = (float)tileWidth / 4;
        }
        if (tileCap <= 0.01f)
        {
            tileCap = 0;
        }

        //NEW GRID SEED
        if (Input.GetKeyDown("space"))
        {
            //for (int i = 0; i < lakes.Count; i++)
            //{
            //    Destroy(lakes[i]);
            //}
            //RandomGen();
            Application.LoadLevel("FirstPlayable");

        }

    }

    void TestGridUpdate()
    {
        //TEST GRID UPDATE
        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                if (positions[x, y].z == 1 && !(positions[x, y].z == 3))
                {
                    testGrid[x, y].GetComponent<SpriteRenderer>().color = cantBuild;
                }
            }
        }
    }

    private void PlaceGrass()
    {
        GameObject grassFolder = Instantiate(new GameObject(), transform.position, transform.rotation);
        grassFolder.name = "Grass Folder";
        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                if (!(positions[x, y].z == 1 || positions[x, y].z == 3))
                {
                    GameObject newgrass = Instantiate(emptyGo, positions[x, y], transform.rotation);
                    newgrass.transform.parent = grassFolder.transform;
                    newgrass.name = "GrassTile";
                    newgrass.GetComponent<SpriteRenderer>().sprite = grassTileSprites[Random.Range(0, grassTileSprites.Length)];
                    newgrass.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(x, y)) + 1;
                    newgrass.GetComponent<SpriteRenderer>().color = fullColor;
                    newgrass.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
                }
            }
        }
    }

    private void PlaceRockToPlace(Vector3 position, Vector2 gridPos)
    {
        {
            GameObject newgrass = Instantiate(emptyGo, new Vector3(position.x, position.y, transform.position.z), transform.rotation);
            newgrass.transform.parent = rockFolder.transform;
            newgrass.name = "RockTile";
            newgrass.GetComponent<SpriteRenderer>().sprite = rockTileSprites[Random.Range(0, grassTileSprites.Length)];
            newgrass.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(gridPos) + 1;
            newgrass.GetComponent<SpriteRenderer>().color = fullColor;
            newgrass.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
        }
    }

    private void RandomGen()
    {
        //RANDOMIZE LAKES
        GameObject lakesFolder = Instantiate(new GameObject(), Vector3.zero, transform.rotation);
        lakesFolder.name = "Lakes";

        for (int i = 0; i < rndLakes; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));

            while (positions[rnd1, rnd2].z == 1 || (positions[rnd1, rnd2].z == 2))
            {
                rnd1 = Random.Range(0, positions.GetLength(0));
                rnd2 = Random.Range(0, positions.GetLength(1));
            }

            GameObject newLake = Instantiate(lakeGo, positions[rnd1, rnd2], Quaternion.identity);
            newLake.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(rnd1, rnd2));
            newLake.GetComponent<SpriteRenderer>().sortingLayerName = ("Buildings");
            lakes.Add(newLake);
            newLake.transform.parent = lakesFolder.transform;

            positions[rnd1, rnd2].z = 1;

        }
        TestGridUpdate();
    }



    public void SpawnStructure(GameObject structure, List<Vector2> tiles, Vector2 size)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            positions[(int)tiles[i].x, (int)tiles[i].y] = new Vector3(positions[(int)tiles[i].x, (int)tiles[i].y].x, positions[(int)tiles[i].x, (int)tiles[i].y].y, 1);
        }

        //CALCULATE HOUSE POSITION
        //Calculate center of the first and last tile
        Vector2 firstPos = new Vector2(positions[(int)tiles[0].x, (int)tiles[0].y].x, positions[(int)tiles[0].x, (int)tiles[0].y].y);
        Vector2 lastPos = new Vector2(positions[(int)tiles[tiles.Count - 1].x, (int)tiles[tiles.Count - 1].y].x, positions[(int)tiles[tiles.Count - 1].x, (int)tiles[tiles.Count - 1].y].y);
        Vector3 cenPos = new Vector3(firstPos.x + ((lastPos.x - firstPos.x) / 2), firstPos.y + ((lastPos.y - firstPos.y) / 2), transform.position.z);

        //INSTANTIATE HOUSE
        GameObject obj = Instantiate(structure, new Vector3(cenPos.x, cenPos.y, transform.position.z), Quaternion.identity) as GameObject;
        obj.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(tiles);
        TestGridUpdate();
    }
    public int CalculateSortingLayer(List<Vector2> tiles)
    {
        //FIND NEAREST CORNER TILE
        //When X is smallest and Y is largest

        int toReturn = 0;
        Vector2 bestMatch = new Vector2(666, -666);

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].x < bestMatch.x && tiles[i].y > bestMatch.y)
            {
                bestMatch = tiles[i];
            }
        }
        toReturn = (int)(bestMatch.y - bestMatch.x);
        return toReturn;
    }

    public int CalculateSortingLayer(Vector2 tile)
    {

        int toReturn = 0;
        toReturn = (int)(tile.y - tile.x);
        return toReturn;
    }
    public void SetTestGridActive(bool active)
    {
        testGridFolder.SetActive(active);
    }

}