using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanctityText : MonoBehaviour
{
    float sanctity;
    Text sanctityText;
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        sanctityText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateSanctityAmount", 0f, 0.2f);
    }
    void UpdateSanctityAmount()
    {
        sanctity = gameManager.GetComponent<GameManager>().sanctity;
        sanctityText.text = sanctity.ToString();
    }
}
