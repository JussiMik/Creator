using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonkCostText : MonoBehaviour
{
    float faithCost;
    Text faithCostText;
    public MysticPlaceCS mysticPlace;
    void Start()
    {
        mysticPlace = GameObject.FindGameObjectWithTag("MysticPlace").GetComponent<MysticPlaceCS>();
        faithCostText = gameObject.GetComponent<Text>();
        InvokeRepeating("UpdateFaithCost", 0.1f, 0.2f);
    }

    public void UpdateFaithCost()
    {
        faithCost = mysticPlace.monkFaithCost;
        faithCostText.text = faithCost.ToString("F0");
    }
}
