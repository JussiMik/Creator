using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanctityFillBarScript : MonoBehaviour
{
    Image image;
    GameManager gameManager;
   public float requiredSanctity;
   public float currentSanctity;
   public float percent;
    // Use this for initialization
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        InvokeRepeating("CheckSanctity", 0.2f, 0.2f);
        requiredSanctity = gameManager.requiredAmountForLvlUp;
        currentSanctity = gameManager.sanctityPoints;

    }

    // Update is called once per frame
    void Update()
    {

    }
    void CheckSanctity()
    {
        requiredSanctity = gameManager.requiredAmountForLvlUp;
        currentSanctity = gameManager.sanctityPoints;
        percent = currentSanctity / requiredSanctity;
        image.fillAmount = percent;
        if(percent >= 1)
        {
            image.fillAmount = 0;
        }
    }
}
