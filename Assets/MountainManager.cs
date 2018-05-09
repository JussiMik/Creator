using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainManager : MonoBehaviour {

    public GameObject[] currentMountains = new GameObject[3];
    public Vector3[] startXPos = new Vector3[3];
    bool changePhase = false;

    public float borderOffset;

    public float smoothTime;

    private Vector3[] velocity = new Vector3[3] {Vector3.zero, Vector3.zero, Vector3.zero};

    private Vector3[] curPos = new Vector3[3];
    private Vector3[] prePos = new Vector3[3];

    // Update is called once per frame
    private void Start()
    {


        for (int i = 0; i < 3; i++)
        {
            startXPos[i] = currentMountains[i].transform.position;
        }
    }

    //void Update ()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        curPos[i] = currentMountains[i].transform.position;
    //        velocity[i] = (curPos[i] - prePos[i]) / Time.deltaTime;
    //        prePos[i] = curPos[i];
    //    }
    //}

    public void ButtonNext()
    {
        changePhase = true;
    }

    //public void SetOffSet(float xOffset)
    //{
    //    {
    //        for (int i = 0; i < currentMountains.Length; i++)
    //        {
    //            Vector3 newPos = new Vector3(startXPos[i].x + xOffset, startXPos[i].y, startXPos[i].z);
    //            currentMountains[i].transform.position = Vector3.SmoothDamp(currentMountains[i].transform.position, newPos, ref velocity[i], Time.deltaTime * smoothTime);
    //        }
    //    }
    //}

}
