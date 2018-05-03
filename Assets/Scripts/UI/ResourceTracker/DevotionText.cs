using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevotionText : MonoBehaviour
{
    float devotion;
    Text devotionText;
    public GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        devotionText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateDevotion", 0.1f, 0.1f);
    }

    public void UpdateDevotion()
    {
        devotion = gameManager.devotion;
        devotionText.text = devotion.ToString("F0");
    }
}
