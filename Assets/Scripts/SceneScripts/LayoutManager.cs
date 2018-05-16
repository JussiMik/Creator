﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LayoutManager : MonoBehaviour
{
    public Vector3[,] positions;
    //FREE SPACE = 0 | TAKEN = 1 | Rock = 2

    public GameManager gameManager;

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

    private GameObject solidRockFolder;

    //public Vector3 nullVector3 = new Vector3(666, 666, 666);


    public Color fullColor = new Color(255, 255, 255, 255);

    public Color cantBuild;

    public List<GameObject> lakes;
    public int rndLakes = 1;

    public List<GameObject> trees;
    public int rndTrees = 1;

    public List<GameObject> rocks;
    public int rndRocks = 1;

    public int gridWidth;
    public int gridHeigth;
    public int currentWidth = 0;

    public int borderWidth;
    public int borderHeigth;

    public bool renderGrid = true;

    public bool tileByAngle;
    public float tileHeight = 0.5f;
    public float tileWidth = 1;
    [SerializeField]
    private float gridAngle;

    public float tileCap;

    private GameObject allTiles;

    public GameObject emptyPre;
    public GameObject lakePre;
    public GameObject treePre;
    public GameObject rockPre;
    public GameObject mysticPlace;

    //https://docs.unity3d.com/ScriptReference/GL.html

    public int lineCount = 10;
    public float radius = 3.0f;

    static Material lineMaterial;



    // Use this for initialization
    private void Start()
    {
        gridAngle = 45 * (tileHeight / tileWidth);
        allTiles = new GameObject("AllTiles");
        //allTiles.hideFlags = HideFlags.HideInHierarchy;
        CreateGrid();
    }

    void CreateGrid()
    {
        positions = new Vector3[gridWidth, gridHeigth];
        testGrid = new GameObject[gridWidth, gridHeigth];

        //CALCULATE CENTER OFFSET
        //float centerOffsetX = ((gridWidth * gridHeigth)/4 * tileWidth )/ 2;
        float centerOffsetX = gridWidth;
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

        solidRockFolder = new GameObject("Rock Folder");
        solidRockFolder.transform.position = Vector3.zero;
        solidRockFolder.transform.SetParent(allTiles.transform);

        //OLD GRID ROUNDING

        //if (roundCorners)
        //{
        //    for (int i1 = 0; i1 < roundCornerBy; i1++)
        //    {
        //        for (int i2 = 0; i2 < roundCornerBy; i2++)
        //        {
        //            if (!(i1 == i2 && i1 == roundCornerBy - 1) || (i1 == 0 && i2 == 0))
        //            {
        //                positions[i1, i2] = new Vector3(positions[i1, i2].x, positions[i1, i2].y, 2);
        //                PlaceRockToPlace(positions[i1, i2], new Vector2(i1, i2));

        //                positions[i1, positions.GetLength(1) - 1 - i2] = new Vector3(positions[i1, positions.GetLength(1) - 1 - i2].x, positions[i1, positions.GetLength(1) - 1 - i2].y, 2);
        //                PlaceRockToPlace(positions[i1, positions.GetLength(1) - 1 - i2], new Vector2 (i1, positions.GetLength(1) - 1 - i2));

        //                positions[positions.GetLength(0) - 1 - i1, i2] = new Vector3(positions[positions.GetLength(0) - 1 - i1, i2].x, positions[positions.GetLength(0) - 1 - i1, i2].y, 2);
        //                PlaceRockToPlace(positions[positions.GetLength(0) - 1 - i1, i2], new Vector2(positions.GetLength(0) - 1 - i1, i2));

        //                positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2] = new Vector3(positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2].x, positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2].y, 2);
        //                PlaceRockToPlace(positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2], new Vector2(positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 - i2));
        //            }
        //        }

        //    }
        //}

        // NEW ROUNDING 
        //EAST
        PlaceRockToPlace(new Vector2(borderWidth, borderHeigth));

        //SOUTH
        PlaceRockToPlace(new Vector2(borderWidth, gridHeigth - borderHeigth - 1));

        //NORTH
        PlaceRockToPlace(new Vector2(gridWidth - borderWidth -1 , borderHeigth));

        //
        PlaceRockToPlace(new Vector2(gridWidth - borderWidth -1, gridHeigth - borderHeigth -1));

        //CREATE TEST GRID
        testGridFolder = new GameObject("Test Grid");
        testGridFolder.transform.position = Vector3.zero;
        testGridFolder.transform.SetParent(allTiles.transform);

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
                    testGrid[x, y] = Instantiate(emptyPre, newPos, Quaternion.identity);
                    testGrid[x, y].transform.SetParent(testGridFolder.transform);
                    testGrid[x, y].name = x + " , " + y;
                    //testGrid[x, y].transform.localScale = new Vector3(tileWidth * 0.35f, (tileHeight * 2) * 0.35f, testGrid[x, y].transform.localScale.z);
                }
            }
        }

        

        //PLACE SOLID ROCKS
        //Place on top width alinged lines
        for (int rowNo = 0; rowNo < borderHeigth; rowNo++)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                if (!(positions[i, rowNo].z == 2))
                {
                    PlaceRockToPlace(new Vector2(i, rowNo));
                    
                }
            }
        }

        //Place on right height alinged lines
        for (int rowNo = 0; rowNo < borderWidth; rowNo++)
        {
            for (int i = 0; i < gridHeigth; i++)
            {
                if (!(positions[rowNo, i].z == 2))
                {
                    PlaceRockToPlace( new Vector2(rowNo, i));
                }
            }
        }

        //Place on left height alinged lines
        for (int rowNo = gridWidth -1; rowNo > (gridWidth - borderWidth -1); rowNo--)
        {
            for (int i = 0; i < gridHeigth; i++)
            {
                if (!(positions[rowNo, i].z == 2))
                {
                    PlaceRockToPlace(new Vector2(rowNo, i));
                }
            }
        }

        //Place on bottom width alinged lines
        for (int rowNo = gridHeigth -1; rowNo > (gridHeigth - borderHeigth -1); rowNo--)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                if (!(positions[i, rowNo].z == 2))
                {
                    PlaceRockToPlace(new Vector2(i, rowNo));
                }
            }
        }
        List<Vector2> temp = new List<Vector2>() { new Vector2(gridWidth / 2, gridHeigth / 2), new Vector2(gridWidth / 2 + 1, gridHeigth / 2), new Vector2(gridWidth / 2, gridHeigth / 2 + 1), new Vector2(gridWidth / 2 + 1, gridHeigth / 2 + 1) };
        SpawnStructure(mysticPlace, temp, new Vector2(2,2)); 

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
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

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
        GameObject grassFolder = new GameObject("Grass Folder");
        grassFolder.transform.position = Vector3.zero;
        grassFolder.transform.SetParent(allTiles.transform);

        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                if (!(positions[x, y].z == 1 || positions[x, y].z == 3))
                {
                    GameObject newgrass = Instantiate(emptyPre, positions[x, y], transform.rotation);
                    newgrass.transform.SetParent(grassFolder.transform);
                    newgrass.name = "GrassTile";
                    newgrass.GetComponent<SpriteRenderer>().sprite = grassTileSprites[Random.Range(0, grassTileSprites.Length)];
                    newgrass.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(x, y)) + 1;
                    newgrass.GetComponent<SpriteRenderer>().color = fullColor;
                    newgrass.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
                }
            }
        }
    }

    private void PlaceRockToPlace(Vector2 gridPos)
    {
        {
            Vector3 newPos = new Vector3(positions[(int)gridPos.x, (int)gridPos.y].x, positions[(int)gridPos.x, (int)gridPos.y].y, transform.position.z);
            GameObject newRock = Instantiate(emptyPre, newPos, transform.rotation);
            newRock.name = "RockTile";
            newRock.layer = LayerMask.NameToLayer("Border");
            newRock.transform.SetParent(solidRockFolder.transform);

            var rend = newRock.GetComponent<SpriteRenderer>();
            rend.sprite = rockTileSprites[Random.Range(0, rockTileSprites.Length)];
            rend.sortingOrder = CalculateSortingLayer(gridPos) + 1;
            rend.color = fullColor;
            rend.sortingLayerName = "Ground";
            newRock.tag = "Boundary";
            positions[(int)gridPos.x, (int)gridPos.y].z = 2;
            Destroy(testGrid[(int)gridPos.x, (int)gridPos.y]);
            testGrid[(int)gridPos.x, (int)gridPos.y] = newRock;
        }
    }

    private void RandomGen()
    {
        //RANDOMIZE LAKES
        GameObject lakesFolder =new GameObject("Lakes Folder");
        lakesFolder.transform.position = Vector3.zero;
        lakesFolder.transform.SetParent(allTiles.transform);

        for (int i = 0; i < rndLakes; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));

            while (positions[rnd1, rnd2].z == 1 || (positions[rnd1, rnd2].z == 2))
            {
                rnd1 = Random.Range(0, positions.GetLength(0));
                rnd2 = Random.Range(0, positions.GetLength(1));
            }

            GameObject newLake = Instantiate(lakePre, positions[rnd1, rnd2], transform.rotation);
            
            newLake.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(rnd1, rnd2));
            newLake.GetComponent<SpriteRenderer>().sortingLayerName = ("Buildings");
            lakes.Add(newLake);
            newLake.transform.SetParent(lakesFolder.transform);

            positions[rnd1, rnd2].z = 1;

        }
        TestGridUpdate();

        //RANDOMIZE TREES
        GameObject treeFolder = new GameObject("Tree Folder");
        treeFolder.transform.position = Vector3.zero;
        treeFolder.transform.SetParent(allTiles.transform);

        for (int i = 0; i < rndTrees; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));

            while (positions[rnd1, rnd2].z == 1 || (positions[rnd1, rnd2].z == 2))
            {
                rnd1 = Random.Range(0, positions.GetLength(0));
                rnd2 = Random.Range(0, positions.GetLength(1));
            }

            GameObject newTree = Instantiate(treePre, positions[rnd1, rnd2], transform.rotation);
            newTree.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(rnd1, rnd2));
            newTree.GetComponent<SpriteRenderer>().sortingLayerName = ("Buildings");
            trees.Add(newTree);
            newTree.transform.SetParent(treeFolder.transform);

            positions[rnd1, rnd2].z = 1;

        }
        TestGridUpdate();

        //RANDOMIZE STONE
        GameObject stonesFolder = new GameObject("Stone Folder");
        stonesFolder.transform.position = Vector3.zero;
        stonesFolder.transform.SetParent(allTiles.transform);

        for (int i = 0; i < rndRocks; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));

            while (positions[rnd1, rnd2].z == 1 || (positions[rnd1, rnd2].z == 2))
            {
                rnd1 = Random.Range(0, positions.GetLength(0));
                rnd2 = Random.Range(0, positions.GetLength(1));
            }

            GameObject newRock = Instantiate(rockPre, positions[rnd1, rnd2], Quaternion.identity);
            newRock.GetComponent<SpriteRenderer>().sortingOrder = CalculateSortingLayer(new Vector2(rnd1, rnd2));
            newRock.GetComponent<SpriteRenderer>().sortingLayerName = ("Buildings");
            rocks.Add(newRock);
            newRock.transform.SetParent(stonesFolder.transform);

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