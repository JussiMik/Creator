using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    //public List<GameObject> moveRightOnLists;

    static List<GameObject> list01 = new List<GameObject>();
    static List<GameObject> list02;
    static List<GameObject> list03;

    public List<GameObject> moveRightOnLists;

    public float[] speedOnLines = new float[3];


    public GameObject gamammaa;

    [Header("LineMarks")]
    public Transform[] lines = new Transform[3];

    [Header("Light Clouds")]
    public Sprite lightCloud;
    public Sprite lightCloudFluffy;

    [Header("Mid Clouds")]
    public Sprite midCloud;
    public Sprite midCloudFluffy;

    [Header("Dark Clouds")]
    public Sprite darkCloud;
    public Sprite darkCloudFluffy;

    // Use this for initialization
    void Start()
    {
        //List<List<GameObject>> moveRightOnLists = new List<List<GameObject>>(list01, list02, list03);

        NewCloud(lightCloud, 0);
        NewCloud(midCloud, 1);
        NewCloud(darkCloud, 2);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < list01.Count; i++)
        {
            list01[i].transform.Translate(Vector2.right * Time.deltaTime * speedOnLines[i]);
            if ((list01[i].transform.position.x) >= (lines[0].position.x * -1))
            {
                list01[i].transform.position = lines[0].position;
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

    void NewCloud(Sprite spr, int line)
    {
        list01.Add(new GameObject("Cloud On Line " + (line + 1)));
        var newCloud = list01[list01.Count - 1];
        newCloud.transform.position = lines[line].position;
        newCloud.AddComponent<SpriteRenderer>();
        newCloud.GetComponent<SpriteRenderer>().sprite = spr;
        newCloud.GetComponent<SpriteRenderer>().sortingOrder = 2 + line * 2;
    }
}
