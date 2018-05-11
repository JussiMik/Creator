using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTracker : MonoBehaviour
{
    ObjectiveManager objectiveManager;
   public GameObject panel;
    Vector2 step;
    public float stepAmount;
    public Transform verticalPosition;
    Text textField;
   public Text textFieldObject;

    // Use this for initialization
    void Start()
    {
        objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();

        panel = gameObject.transform.GetChild(0).gameObject;
        
        step = new Vector2(0, stepAmount);
        CheckSelectedObjectives();
    }

    void CheckSelectedObjectives()
    {

        foreach (bool item in objectiveManager.selectedObjectives)
        {
            if (item == true)
            {
                textField = Instantiate(textFieldObject, transform.position + new Vector3(0, stepAmount, 0), transform.rotation);
                textField.transform.SetParent(panel.transform, true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
