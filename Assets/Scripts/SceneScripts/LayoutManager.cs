using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{

    public Vector3[,] positions;
    public GameObject[,] testGrid;
    [SerializeField]
    bool gridDone = false;
    public bool roundCorners = false;
    public int roundCornerBy = 1;

    public bool showGrid;

    private Vector3 nullVector3 = new Vector3(666, 666, 666);

    public Color cantBuild;

    public List<GameObject> lakes;
    public int rndLakes = 1;

    public int gridWidth;
    public int gridHeigth;
    public int currentWidth = 0;

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
        if (roundCorners)
        {
            for (int i1 = 0; i1 < roundCornerBy; i1++)
            {
                for (int i2 = 0; i2 < roundCornerBy; i2++)
                {
                    if(!(i1 == i2 && i1 == roundCornerBy -1) || (i1 == 0 && i2 == 0))
                    {
                        positions[i1, i2] = nullVector3;
                        positions[i1, positions.GetLength(1) - 1 - i2] = nullVector3;
                        positions[positions.GetLength(0) - 1 - i1, i2] = nullVector3;
                        positions[positions.GetLength(0) - 1 - i1, positions.GetLength(1) - 1 -i2] = nullVector3;
                    }
                }
                
            }
        }

        //CREATE TEST GRID
        GameObject testGridFolder = Instantiate(new GameObject(), Vector3.zero, transform.rotation);
        testGridFolder.name = "Test Grid";

        for (int x = 0; x < gridWidth; x++)
        {

            for (int y = 0; y < gridHeigth; y++)
            {
                if (positions[x, y] == nullVector3)
                {
                }
                else
                {
                    Vector3 newPos = new Vector3(positions[x, y].x, positions[x, y].y, transform.position.z);
                    testGrid[x, y] = Instantiate(emptyGo, newPos, Quaternion.identity);
                    testGrid[x, y].transform.parent = testGridFolder.transform;
                    testGrid[x, y].name = x + " , " + y;
                    testGrid[x, y].transform.localScale = new Vector3(tileWidth * 0.35f, (tileHeight*2) * 0.35f, testGrid[x, y].transform.localScale.z);
                }
            }
        }
        SpawnStructure(centerGo, gridWidth / 2, gridHeigth / 2, new Vector2(2, 2));
        RandomGen();
        gridDone = true;
    }

    void Update()
    {
        if(gridDone)
        {
            //if (showGrid)
            //{
            //    for (int x = 0; x < testGrid.GetLength(0); x++)
            //    {
            //        for (int y = 0; y < testGrid.GetLength(1); y++)
            //        {
            //            testGrid[x, y].GetComponent<SpriteRenderer>().enabled = true;
            //        }
            //    }
            //}
            //else
            //{
            //    for (int x = 0; x < testGrid.GetLength(0); x++)
            //    {
            //        for (int y = 0; y < testGrid.GetLength(1); y++)
            //        {
            //            testGrid[x, y].GetComponent<SpriteRenderer>().enabled = false;
            //        }
            //    }
            //}
        }

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
            Application.LoadLevel("testscene");

        }
        //TEST GRID UPDATE
        for (int x = 0; x < positions.GetLength(0); x++)
        {
            for (int y = 0; y < positions.GetLength(1); y++)
            {
                if (positions[x, y].z == 1 && !(positions[x, y] == nullVector3))
                {
                    testGrid[x, y].GetComponent<SpriteRenderer>().color = cantBuild;
                }
            }
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

            while (positions[rnd1, rnd2].z == 1 || (positions[rnd1, rnd2] == nullVector3))
            {
                rnd1 = Random.Range(0, positions.GetLength(0));
                rnd2 = Random.Range(0, positions.GetLength(1));
            }

            GameObject newLake = Instantiate(lakeGo, positions[rnd1, rnd2], Quaternion.identity);
            lakes.Add(newLake);
            newLake.transform.parent = lakesFolder.transform;
            positions[rnd1, rnd2].z = 1;
        }
    }



    private void SpawnStructure(GameObject structure, int posX, int posY, Vector2 size)
    {
        //SPAWN THE OBJECT
        
        //GET THE SIZE THAT STRUCTURE NEEDS

        //SET TILES TO TAKEN
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                int freeToTaken1 = Mathf.RoundToInt(posX - (size.x / 2) + x);
                int freeToTaken2 = Mathf.RoundToInt(posY - (size.y / 2) + y);
                positions[freeToTaken1, freeToTaken2] = new Vector3(positions[freeToTaken1, freeToTaken2].x, positions[freeToTaken1, freeToTaken2].y, 1);
            }
        }
        //CALCULATE HOUSE POSITION
        Vector3 newPosition = new Vector3(positions[posX, posY].x - size.x/2, positions[posX, posY].y, transform.position.z);
        GameObject obj = Instantiate(centerGo, newPosition, Quaternion.identity) as GameObject;
    }
}