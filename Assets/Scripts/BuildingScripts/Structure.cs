using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private bool constructingTimer;

    [SerializeField]
    private float constructingTime;
    public float constructingTimeSlow1;
    public float constructingTimeSlow2;
    public float constructingTimeSlow3;

    public bool normalSpeedConstructing;
    public bool changedValue;
    public bool lowerSpeedConstructing1;
    public bool lowerSpeedConstructing2;
    public bool lowerSpeedConstructing3;

    public bool constructingDone;

    [Space(10)]
    public int faithAmount;
    public int maxFaithAmount;

    public double faithMultiplier;
    public double generatedFaith;

    public bool collectedFaith;

    public bool faithTimer;
    public float faithTargetTime;
    public float originalFaithTargetTime;

    [Space(10)]
    public bool defaultFaithGeneration;
    public bool slowerFaithGeneration1;
    public bool slowerFaithGeneration2;
    public bool slowerFaithGeneration3;

    public bool lvlChange;
    public int level;
    public int maxLevelAmount;

    public virtual void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = faithTargetTime;

        level = 1;
        lvlChange = false;

        constructingDone = false;

        normalSpeedConstructing = true;

        collectedFaith = true;

        ConstructingStructures();
    }

    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            ConstructingTimer();
        }

        if (gameManager.devotionDecreaseMp1 == true)
        {   
            lowerSpeedConstructing1 = true;
        }
        if (gameManager.devotionDecreaseMp2 == true)
        {
            lowerSpeedConstructing2 = true;
        }
        if (gameManager.devotionDecreaseMp3 == true)
        {
            lowerSpeedConstructing3 = true;
        }

        if (lvlChange == true)
        {
            ChangeLevel();
        }

        if (faithTimer == true && collectedFaith == true)
        {
            FaithTimer();
        }

        if (Input.GetMouseButtonDown(0) && generatedFaith > 0 && gameManager.devotion >= 10)
        {
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);

            if (hit.transform.tag == "FaithBuilding")
            {
                CollectFaith();
            }
        }
    }

    public void ConstructingStructures()
    {
        //gameManager.UseFaith();
        gameManager.DevotionDecreaseChunk();

        constructingTimer = true;
    }

    public void ConstructingTimer()
    {
        if (normalSpeedConstructing == true)
        {
            constructingTime -= Time.deltaTime * 4;
        }

        if (lowerSpeedConstructing1 == true)
        {
            normalSpeedConstructing = false;
            constructingTime -= Time.deltaTime * 3;
        }

        if (lowerSpeedConstructing2 == true)
        {
            normalSpeedConstructing = false;
            lowerSpeedConstructing1 = false;
            constructingTime -= Time.deltaTime * 2;
        }

        if (lowerSpeedConstructing3 == true)
        {
            normalSpeedConstructing = false;
            lowerSpeedConstructing2 = false;
            constructingTime -= Time.deltaTime;
        }

        if (constructingTime <= 0)
        {
            constructingTime = 0f;
            constructingTimer = false;
            constructingDone = true;
        }

    }

    //Faithtimer before faithgeneration starts
    public virtual void FaithTimer()
    {
        faithTargetTime -= Time.deltaTime;

        if (faithTargetTime <= 0)
        {
            faithTargetTime = 0;
            TimerEnd();
        }
    }

    //Timer ends and starts faith generation
    public void TimerEnd()
    {
        faithTimer = false;

        GenerateFaith();

        if (faithTimer == false)
        {
            faithTargetTime = originalFaithTargetTime;
            faithTimer = true;
            collectedFaith = false;
        }
    }

    public void GenerateFaith()
    {
        generatedFaith += faithAmount;

        if (defaultFaithGeneration == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplier);
        }

        if (slowerFaithGeneration1 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow1);
        }

        if (slowerFaithGeneration2 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow2);
        }

        if (slowerFaithGeneration3 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow3);
        }
    }

    //Player can collect generated faith for later use
    public void CollectFaith()
    {
        gameManager.DevotionDecreaseChunk();

        gameManager.faith += generatedFaith;
        generatedFaith = 0;

        collectedFaith = true;
    }

    public void ChangeLevel()
    {
        //gameManager.UseFaith();

        if (level >= 1)
        {
            faithAmount += 1;
            level += 1;

            if (faithAmount >= maxFaithAmount && level >= maxLevelAmount)
            {
                faithAmount = maxFaithAmount;
                level = maxLevelAmount;
            }
        }

        lvlChange = false;
    }
}
