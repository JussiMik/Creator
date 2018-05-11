using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeBase : MonoBehaviour
{
    protected ObjectiveManager objectiveManager;
    protected GameManager gameManager;
    public bool useChallenge;
    protected bool challengeDone;

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
  /*  void CheckSelectedObjectives(bool useChallenge)
    {
        objectiveManager.selectedObjectives.Add(useChallenge);
    }
    */
}
