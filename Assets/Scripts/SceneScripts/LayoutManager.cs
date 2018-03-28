using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{

    public Vector3[,] positions;
    [SerializeField]
    bool emptyGridDone = false;

    public List<GameObject> lakes;
    public int rndLakes = 1;

    public int gridWidth;
    public int gridHeigth;
    public int currentWidth = 0;

    public bool renderGrid = true;

    public int tileWidth;
    public float tileCap;

    public GameObject emptyGo;
    public GameObject lakeGo;
    public GameObject centerGo;

    //https://docs.unity3d.com/ScriptReference/GL.html

    public int lineCount = 10;
    public float radius = 3.0f;

    static Material lineMaterial;

    // Use this for initialization
    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        positions = new Vector3[gridWidth, gridHeigth];

        float centerOffsetX = gridWidth * tileWidth / 2 - tileWidth / 2;
        float centerOffsetY = 0;

        for (int x = 0; x < gridWidth; x++)
        {

            for (int y = 0; y < gridHeigth; y++)
            {

                positions[x, y] = new Vector3 (transform.position.x - centerOffsetX + (1f * x) + (1f * y), transform.position.y - centerOffsetY + (0.5f * x) - (0.5f * y), 0);

                //positions.Add(Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f)), Quaternion.identity));
                //positions[x, y] = new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f), 0);
                Vector3 newPos = new Vector3(positions[x, y].x, positions[x, y].y, transform.position.z);
                GameObject newGo = Instantiate(emptyGo, newPos, Quaternion.identity);
                newGo.name = x + " , " + y;


                //positionSit.Add(0);


                //if (currentWidth < gridHeigth)
                //{
                //    currentWidth += 1;
                //}
                //else
                //{
                //    currentWidth = 0;
                //}
            }
        }
        SpawnStructure(centerGo, gridWidth / 2, gridHeigth / 2, new Vector2(2,2));
        RandomGen();
    }

    // Update is called once per frame
    void Update()
    {
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
            for (int i = 0; i < lakes.Count; i++)
            {
                Destroy(lakes[i]);
            }
            RandomGen();
        }
    }

    private void RandomGen()
    {
        //KAATUU TÄHÄN FOR LOOPPIIN----------------------------------------------------------------
        for (int i = 0; i < rndLakes; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));

            StartCoroutine(RndLakes(rnd1, rnd2));

            //while(positions[rnd1, rnd2].z == 1)
            //{
            //    rnd1 = Random.Range(0, positions.GetLength(0));
            //    rnd2 = Random.Range(0, positions.GetLength(1));
            //}
        }
    }

    IEnumerator RndLakes(int rnd1, int rnd2)
    {
        for (; positions[rnd1, rnd2].z == 1; )
        {
            rnd1 = Random.Range(0, positions.GetLength(0));
            rnd2 = Random.Range(0, positions.GetLength(1));
        }

        lakes.Add(Instantiate(lakeGo, positions[rnd1, rnd2], Quaternion.identity));
        positions[rnd1, rnd2].z = 1;

        yield return null;
    }

    private void SpawnStructure(GameObject structure, int posX, int posY, Vector2 size)
    {
        //SPAWN THE OBJECT
        Vector3 newPosition = new Vector3(positions[posX, posY].x, positions[posX, posY].y - size.y/4, transform.position.z);
        GameObject obj = Instantiate(centerGo, newPosition, Quaternion.identity) as GameObject;
        //GET THE SIZE THAT STRUCTURE NEEDS
   
        //Vector2 size = obj.GetComponent<HouseScript>().sizeOnGrid;

        //SET TILES TO TAKE
        for (int x = 0; x < size.x; x++)

        {
            for (int y = 0; y < size.y; y++)
            {
                int freeToTaken1 = Mathf.RoundToInt(posX - (size.x / 2) + x);
                int freeToTaken2 = Mathf.RoundToInt(posY - (size.y / 2) + y);
                positions[freeToTaken1, freeToTaken2] = new Vector3(positions[freeToTaken1, freeToTaken2].x, positions[freeToTaken1, freeToTaken2].y, 1);
            }
        }
        UpdateTakens();
    }

    private void UpdateTakens()
    {
        for (int y = 0; y < gridHeigth; y++)
        {

            for (int x = 0; x < gridWidth; x++)
            {
                if (positions[x, y].z == 1)
                {
                    Vector3 newPos = new Vector3(positions[x, y].x, positions[x, y].y, transform.position.z);
                    //Instantiate(emptyGo, newPos, Quaternion.identity);
                }

            }
        }
    }
}


