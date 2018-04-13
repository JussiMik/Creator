using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField]
    private bool constructingTimer;

    [SerializeField]
    private float constructingTime;

    public bool constructingDone;

    public bool lvlChange;
    public int level;
    public int maxLevelAmount;

    public int faithAmount;
    public int maxFaithAmount;
    public double faithMultiplier;

    public GameManager gameManager;

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
    }

    public void ConstructingStructures()
    {
        gameManager.UseFaith();
        gameManager.DevotionDecreaseChunk();

        constructingTimer = true;
    }

    public void ChangeLevel()
    {
        gameManager.UseFaith();

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
