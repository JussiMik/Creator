using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    Text text;
    bool[] objectives;
    int numberOfObjectives;


    void Start()
    {
        objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();

        for (int i = 0; i < numberOfObjectives; i++)
        {

        }
    }


    void Update()
    {

    }
}
