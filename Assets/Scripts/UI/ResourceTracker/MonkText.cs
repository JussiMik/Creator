using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonkText : MonoBehaviour
{
    float monkCount;
    GameObject gameManager;
    Text monkCountText;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        monkCountText = gameObject.GetComponent<Text>();
        UpdateMonkCount();
    }

    // Update is called once per frame
    void Update()
    {
        monkCount = gameManager.GetComponent<GameManager>().monks.Count;
        monkCountText.text = monkCount.ToString();
    }
    public void UpdateMonkCount()
    {
        monkCount = gameManager.GetComponent<GameManager>().monks.Count;
        monkCountText.text = monkCount.ToString();
    }
}
