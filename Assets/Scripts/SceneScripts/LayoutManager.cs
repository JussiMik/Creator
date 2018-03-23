using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{

    public List<Vector2> positions;
    public List<int> positionSit = new List<int>();
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

        float centerOffsetX = gridWidth * tileWidth / 2 - tileWidth / 2;
        float centerOffsetY = 0;

        for (int y = 0; y < gridHeigth; y++)
        {

            for (int x = 0; x < gridWidth; x++)
            {
                //positions[x, y] = Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + x * (tileWidth/2), transform.position.y - centerOffsetY + y * (tileWidth/2), 0), Quaternion.identity);

                //positions.Add(Instantiate(emptyGo, new Vector3(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f)), Quaternion.identity));
                positions.Add(new Vector2(transform.position.x - centerOffsetX + (1f * y) + (currentWidth * 1f), transform.position.y - centerOffsetY + (0.5f * y) - (currentWidth * 0.5f)));
                positionSit.Add(0);

                if (currentWidth < gridWidth - 1)
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
            int rndInt = Random.Range(0, positions.Count);
            
            lakes.Add(Instantiate(lakeGo, positions[rndInt], Quaternion.identity));
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

            if (emptyGridDone)
            {

                for (int i = 0; i < positions.Count; i++)
                {
                    float gap = tileWidth / 2 - tileCap;
                    if (positionSit[i] == 0)
                    {
                        GL.Color(new Color(1, 1, 0, 0.8F));
                    }
                    else if (positionSit[i] == 1)
                    {
                        GL.Color(new Color(1, 1, 0, 0.8F));
                    }
                    else
                    {
                        GL.Color(new Color(1, 1, 0, 0.8F));
                    }

                    //Dot1
                    GL.Vertex3(positions[i].x - gap, positions[i].y, transform.position.z);
                    //Dot2
                    GL.Vertex3(positions[i].x, positions[i].y - gap / 2, transform.position.z);
                    GL.Vertex3(positions[i].x, positions[i].y - gap / 2, transform.position.z);
                    //Dot3
                    GL.Vertex3(positions[i].x + gap, positions[i].y, transform.position.z);
                    GL.Vertex3(positions[i].x + gap, positions[i].y, transform.position.z);
                    //Dot4
                    GL.Vertex3(positions[i].x, positions[i].y + gap / 2, transform.position.z);
                    GL.Vertex3(positions[i].x, positions[i].y + gap / 2, transform.position.z);
                    //Back To Dot1
                    GL.Vertex3(positions[i].x - gap, positions[i].y, transform.position.z);
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


