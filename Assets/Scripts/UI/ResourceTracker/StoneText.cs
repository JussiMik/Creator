using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneText : MonoBehaviour
{
    float stone;
    Text stoneText;
    GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        stoneText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateWood", 0.1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateWood()
    {
        stone = gameManager.stone;
        stoneText.text = stone.ToString();
    }
}
