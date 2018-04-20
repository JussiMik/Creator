using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaithText : MonoBehaviour
{
    double faith;
    Text faithText;
    public GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        faithText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateFaith", 0.1f, 0.2f);
    }

    void Update()
    {

    }

    public void UpdateFaith()
    {
        faith = gameManager.faith;
        faithText.text = faith.ToString();
    }
}
