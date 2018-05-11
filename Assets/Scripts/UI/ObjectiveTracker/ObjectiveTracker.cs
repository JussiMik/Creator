using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracker : MonoBehaviour
{
    ObjectiveManager objectiveManager;
    public GameObject panel;
    float step;
    public float stepAmount;
    public Transform verticalPosition;
    Text textField;
    public Text textFieldObject;

    bool monkTextUsed;

    // Use this for initialization
    void Start()
    {
        step = 0;
        objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
        panel = gameObject.transform.GetChild(0).gameObject;
        CheckSelectedObjectives();
    }

    void CheckSelectedObjectives()
    {
        foreach (bool selectedObjective in objectiveManager.selectedObjectives)
        {
            if (selectedObjective == true)
            {
                textFieldObject.text = objectiveManager.tutturuuVittu;
                textField = Instantiate(textFieldObject, panel.transform.position + new Vector3(0, step, 0), transform.rotation);
                textField.transform.SetParent(panel.transform, true);
                step += stepAmount;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
