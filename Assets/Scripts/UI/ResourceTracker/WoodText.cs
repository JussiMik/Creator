using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodText : MonoBehaviour
{
    float wood;
    Text woodText;
    GameObject gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        woodText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
