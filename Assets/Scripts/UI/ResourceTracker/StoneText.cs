using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneText : MonoBehaviour
{
    float stone;
    Text stoneText;
    GameObject gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        stoneText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
