using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodText : MonoBehaviour
{
    float wood;
    Text woodText;
    GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        woodText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateWood", 0.1f, 0.2f);
    }

    public void UpdateWood()
    {
        wood = gameManager.wood;
        woodText.text = wood.ToString();
    }
}
