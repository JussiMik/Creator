using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevotionText : MonoBehaviour
{
    float devotion;
    Text devotionText;
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        devotionText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateDevotion", 0f, 0.5f);
    }

    void Update()
    {

    }

    public void UpdateDevotion()
    {
        devotion = gameManager.GetComponent<GameManager>().devotion;
        devotionText.text = devotion.ToString();
    }
}
