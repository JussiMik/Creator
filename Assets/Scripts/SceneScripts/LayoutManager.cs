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

        for (int y = 0; y < gridHeigth; y++)
        {

            for (int x = 0; x < gridHeigth; x++)
            {

                //positions[x, y] = Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + x * (tileWidth/2), transform.position.y - centerOffsetY + y * (tileWidth/2), 0), Quaternion.identity);

                //positions.Add(Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f)), Quaternion.identity));
                positions[x, y] = new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f), 0);
                //positionSit.Add(0);


                if (currentWidth < gridHeigth)
                {
                    currentWidth += 1;
                }
                else
                {
                    currentWidth = 0;
                }
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
        for (int i = 0; i < rndLakes; i++)
        {

            int rnd1 = Random.Range(0, positions.GetLength(0));
            int rnd2 = Random.Range(0, positions.GetLength(1));
            lakes.Add(Instantiate(lakeGo, positions[rnd1, rnd2], Quaternion.identity));
        }
    }
    private void SpawnStructure(GameObject structure, int posX, int posY, Vector2 size)
    {
        //SPAWN THE OBJECT
        Vector3 newPosition = new Vector3(positions[posX, posY].x, positions[posX, posY].y - size.y/4, transform.position.z);
        GameObject obj = Instantiate(centerGo, newPosition, Quaternion.identity) as GameObject;
        //GET THE SIZE THAT STRUCTURE NEEDS
   
        //Vector2 size = obj.GetComponent<HouseScript>().sizeOnGrid;

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
                    Instantiate(emptyGo, newPos, Quaternion.identity);
                }

            }
        }
    }

    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }
    public void OnRenderObject()
    {
        if (renderGrid)
        {
            CreateLineMaterial();
            // Apply the line material
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            // Set transformation matrix for drawing to
            // match our transform

            GL.MultMatrix(transform.localToWorldMatrix);

            // Draw lines
            GL.Begin(GL.LINES);
            //float angle = a * Mathf.PI * 2;
            // Vertex colors change from red to green

            // One vertex at transform position

            //if (emptyGridDone)
            //{

            //    for (int i = 0; i < positions.Count; i++)
            //    {
            //        float gap = tileWidth / 2 - tileCap;
            //        if (positionSit[i] == 0)
            //        {
            //            GL.Color(new Color(1, 1, 0, 0.8F));
            //        }
            //        else if (positionSit[i] == 1)
            //        {
            //            GL.Color(new Color(1, 1, 0, 0.8F));
            //        }
            //        else
            //        {
            //            GL.Color(new Color(1, 1, 0, 0.8F));
            //        }

            //        //Dot1
            //        GL.Vertex3(positions[i].x - gap, positions[i].y, transform.position.z);
            //        //Dot2
            //        GL.Vertex3(positions[i].x, positions[i].y - gap / 2, transform.position.z);
            //        GL.Vertex3(positions[i].x, positions[i].y - gap / 2, transform.position.z);
            //        //Dot3
            //        GL.Vertex3(positions[i].x + gap, positions[i].y, transform.position.z);
            //        GL.Vertex3(positions[i].x + gap, positions[i].y, transform.position.z);
            //        //Dot4
            //        GL.Vertex3(positions[i].x, positions[i].y + gap / 2, transform.position.z);
            //        GL.Vertex3(positions[i].x, positions[i].y + gap / 2, transform.position.z);
            //        //Back To Dot1
            //        GL.Vertex3(positions[i].x - gap, positions[i].y, transform.position.z);
            //    }

            //}

            //GL.Vertex3(positions[0,0].transform.position.x, positions[0, 0].transform.position.y, positions[0, 0].transform.position.z);
            // Another vertex at edge of circle
            //GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        }
        GL.End();
        GL.PopMatrix();
    }
}


