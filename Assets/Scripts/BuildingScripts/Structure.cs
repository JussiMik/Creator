using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField]
    private bool constructingTimer;

    [SerializeField]
    private float constructingTime;
    public bool lowerSpeedConstructing;

    public bool constructingDone;

    public bool lvlChange;
    public int level;
    public int maxLevelAmount;

    public int faithAmount;
    public int maxFaithAmount;

    public double faithMultiplier;
    public double generatedFaith;
    public double maxGeneratedFaith;

    public GameManager gameManager;

    public bool faithTimer;
    public float faithTargetTime;
    public float originalFaithTargetTime;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        level = 1;
        lvlChange = false;

        constructingDone = false;
    }

    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            constructingTime -= Time.deltaTime;

            if (lowerSpeedConstructing == true)
            {
                constructingTime = (constructingTime * 2) - Time.deltaTime;
            }

            if (constructingTime <= 0)
            {
                constructingTime = 0f;
                constructingTimer = false;
                constructingDone = true;
            }
        }

        if (lvlChange == true)
        {
            ChangeLevel();
        }

        if (faithTimer == true)
        {
            FaithTimer();
        }
    }

    public void ConstructingStructures()
    {
        //gameManager.UseFaith();
        gameManager.DevotionDecreaseChunk();

        constructingTimer = true;
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
        }
    }

    public void GenerateFaith()
    {
        for (int i = 0; i < gameManager.faithMultipliers.Count; i++)
        {
            generatedFaith += (faithAmount * gameManager.faithMultipliers[i]);
        }

        generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplier);

        if (gameManager.slowerFaithGeneration1 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow1);
        }
        if (gameManager.slowerFaithGeneration2 == true)
        {
            generatedFaith += (gameManager.monks.Count * gameManager.monkFaithMultiplierSlow2);
        }
        if (gameManager.slowerFaithGeneration3 == true)
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
