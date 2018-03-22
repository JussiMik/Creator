using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{

    public GameObject[,] positions;
    public List<GameObject> lakes;

    public int gridWidth;
    public int gridHeigth;
    public int currentWidth = 0;

    public bool renderGrid = true;

    public int tileWidth;
    public float tileCap;

    public GameObject emptyGo;
    public GameObject lakeGo;

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
        positions = new GameObject[gridWidth, gridHeigth];

        float centerOffsetX = gridWidth * tileWidth/2 - tileWidth/2;
        float centerOffsetY = 0;

        for (int y = 0; y < gridHeigth; y++)
        {

            for (int x = 0; x < gridWidth; x++)
            {
                //positions[x, y] = Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + x * (tileWidth/2), transform.position.y - centerOffsetY + y * (tileWidth/2), 0), Quaternion.identity);

                positions[x, y] = Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f)), Quaternion.identity);

                if (currentWidth < gridWidth -1)
                {
                    currentWidth += 1;
                }
                else
                {
                    currentWidth = 0;
                }
            }
        }
        RandomGen();
    }

    // Update is called once per frame
    void Update()
    {
        if(tileCap >= (float) tileWidth/4 + 0.01f)
        {
            tileCap = (float) tileWidth/4;
        }
        if (tileCap <= 0.01f)
        {
            tileCap = 0;
        }

        //NEW GRID SEED
        if(Input.GetKeyDown("space"))
        {
            for (int i = 0; i < lakes.Count; i++)
            {
                Destroy(lakes[i]);
                RandomGen();
            }
        }
    }

    private void RandomGen()
    {
        for (int i = 0; i < 5; i++)
        {
            int rndfloat = Random.Range(0, gridWidth);
            int rndfloat2 = Random.Range(0, gridHeigth);
            lakes.Add(Instantiate(lakeGo, positions[rndfloat, rndfloat2].transform.position, Quaternion.identity));
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
            for (int i = 0; i < lineCount; ++i)
            {
                float a = i / (float)lineCount;
                float angle = a * Mathf.PI * 2;
                // Vertex colors change from red to green
                GL.Color(new Color(a, 1 - a, 0, 0.8F));
                // One vertex at transform position

                for (int x = 0; x < gridWidth; x++)
                {
                    for (int y = 0; y < gridHeigth; y++)
                    {
                        float gap = tileWidth / 2 - tileCap;

                        //Dot1
                        GL.Vertex3(positions[x, y].transform.position.x - gap, positions[x, y].transform.position.y, positions[x, y].transform.position.z);
                        //Dot2
                        GL.Vertex3(positions[x, y].transform.position.x, positions[x, y].transform.position.y - gap / 2, positions[x, y].transform.position.z);
                        GL.Vertex3(positions[x, y].transform.position.x, positions[x, y].transform.position.y - gap / 2, positions[x, y].transform.position.z);
                        //Dot3
                        GL.Vertex3(positions[x, y].transform.position.x + gap, positions[x, y].transform.position.y, positions[x, y].transform.position.z);
                        GL.Vertex3(positions[x, y].transform.position.x + gap, positions[x, y].transform.position.y, positions[x, y].transform.position.z);
                        //Dot4
                        GL.Vertex3(positions[x, y].transform.position.x, positions[x, y].transform.position.y + gap / 2, positions[x, y].transform.position.z);
                        GL.Vertex3(positions[x, y].transform.position.x, positions[x, y].transform.position.y + gap / 2, positions[x, y].transform.position.z);
                        //Back To Dot1
                        GL.Vertex3(positions[x, y].transform.position.x - gap, positions[x, y].transform.position.y, positions[x, y].transform.position.z);
                    }

                }
                //GL.Vertex3(positions[0,0].transform.position.x, positions[0, 0].transform.position.y, positions[0, 0].transform.position.z);
                // Another vertex at edge of circle
                //GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            }
            GL.End();
            GL.PopMatrix();
        }
    }
    IEnumerator DoSomething()
    { 
        yield return new WaitForSeconds(1);
    }
}
