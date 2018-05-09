using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    //public List<GameObject> moveRightOnLists;

    static List<GameObject> list01 = new List<GameObject>();
    static List<GameObject> list02 = new List<GameObject>();
    static List<GameObject> list03 = new List<GameObject>();

    public List<List<GameObject>> moveRightOnLists = new List<List<GameObject>>() {list01, list02, list03};
    
    public float[] speedOnLines = new float[3];
    public float[] cloudsPerLine = new float[3];

    public GameObject gamammaa;

    [Header("LineMarks")]
    public Transform[] lines = new Transform[3];

    [Header("Cloud Sprites")]
    
    public Sprite[] flatClouds = new Sprite[3];
    public Sprite[] fluffyClouds = new Sprite[3];


    // Use this for initialization
    void Start()
    {
        NewCloudsToLine(0);
        NewCloudsToLine(1);
        NewCloudsToLine(2);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < moveRightOnLists.Count; i++)
        {
            for (int i2 = 0; i2 < moveRightOnLists[i].Count; i2++)
            {
                moveRightOnLists[i][i2].transform.Translate(Vector2.right * Time.deltaTime * speedOnLines[i]);
                if ((moveRightOnLists[i][i2].transform.position.x) >= (lines[i].position.x * -1))
                {
                    moveRightOnLists[i][i2].transform.position = lines[i].position;
                }
            }
        }
        
        //for (int i = 0; i < list02.Count; i++)
        //{
        //    list02[i].transform.Translate(Vector2.right * Time.deltaTime * speedOnLines[i]);
        //    if ((list02[i].transform.position.x) >= (lines[0].position.x * -1))
        //    {
        //        list01[i].transform.position = lines[0].position;
        //    }
        //}

        //for (int i = 0; i < list01.Count; i++)
        //{
        //    list01[i].transform.Translate(Vector2.right * Time.deltaTime * speedOnLines[i]);
        //    if ((list01[i].transform.position.x) >= (lines[0].position.x * -1))
        //    {
        //        list01[i].transform.position = lines[0].position;
        //    }
        //}
    }

    void NewCloudsToLine(int line)
    {
        bool flatCloud = true;
        float linePartLength = (((-1) * lines[line].position.x * 2)/cloudsPerLine[line]);
       
        for (int i = 0; i < cloudsPerLine[line]; i++)
        {
            moveRightOnLists[line].Add(new GameObject("Cloud " +i+ " On Line " + (line + 1)));
            var newCloud = moveRightOnLists[line][moveRightOnLists[line].Count - 1];

            Vector3 newPos = new Vector3(lines[line].position.x + (linePartLength * i), lines[line].position.y, lines[line].position.z);

            newCloud.transform.position = newPos;
            newCloud.AddComponent<SpriteRenderer>();

            if(flatCloud)
            {
                newCloud.GetComponent<SpriteRenderer>().sprite = flatClouds[line];
                flatCloud = false;
            }
            else
            {
                newCloud.GetComponent<SpriteRenderer>().sprite = fluffyClouds[line];
                flatCloud = true;
            }

            newCloud.GetComponent<SpriteRenderer>().sortingOrder = 2 + line * 2;
        }
        
    }
}
