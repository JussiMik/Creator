using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneText : MonoBehaviour
{
    float stone;
    Text stoneText;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        stoneText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateStone", 0.1f, 0.2f);
    }

    public void UpdateStone()
    {
        stone = gameManager.stone;
        stoneText.text = stone.ToString();
    }
}
