using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeBase : MonoBehaviour
{
    protected ObjectiveManager objectiveManager;
    protected GameManager gameManager;
    public bool useChallenge;
    public bool challengeDone;

    public string objectiveText;

    // Use this for initialization
    protected virtual void Start()
    {
        objectiveManager = GameObject.Find("ObjectiveManager").GetComponent<ObjectiveManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        challengeDone = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void Objective()
    {

    }
}
