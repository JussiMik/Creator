﻿using UnityEngine;
using System.Collections;

public class Monk : MonoBehaviour
{

    public Transform targetTransform;
    public GameObject targetObject;
    public GameManager gameManager;
   public bool checkForNewDestination;
    bool startNewPathTimer;
    bool reachedDestination;
    public float speed;
    public float movementCheckDistance;
    Vector2[] path;
    int targetIndex;

    //  public GameObject monk;

    [Space(10)]
    public float goodMonkAndFarmRatio;
    public float badMonkAndFarmRatio75;
    public float badMonkAndFarmRatio50;
    public float badMonkAndFarmRatio25;


    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("CheckForNewDestination", 0.5f, 1.5f);
        InvokeRepeating("CheckDistanceFromTarget", 1f, 2.5f);
        startNewPathTimer = targetObject.GetComponent<PathfindingTargetLocation>().startNewTargetTimer;
    }

    void Update()
    {
        if (checkForNewDestination == true)
        {
            StartCoroutine("RefreshPath");
        }

        CheckFarmCount();
    }

    
    // DELET THIS when dormitory is done & move UpdateMonkCount();
  /*  public void SpawnNewMonk()
    {
        GameObject spawnedMonk = Instantiate(gameObject, new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), transform.rotation);
        gameManager.monks.Add(spawnedMonk);

    }
    */

    void CheckFarmCount()
    {
       // int index;

        if (gameManager.farms.Count == 0)
        {
            gameManager.devotionDecrease = true;

            if (gameManager.monks.Count == 0)
            {
                gameManager.devotionDecrease = false;
            }
        }

        if (gameManager.monks.Count > 0 && gameManager.farms.Count > 0)
        {
            if (gameManager.monks.Count / gameManager.farms.Count <= goodMonkAndFarmRatio)
            {
                gameManager.devotionDecrease = false;
                gameManager.devotionDecrease1 = false;
                gameManager.devotionDecrease2 = false;
                gameManager.devotionDecrease3 = false;
                gameManager.devotionIncrease = true;

                if (gameManager.gardens.Count > 0 || gameManager.meditationRooms.Count > 0)
                {
                    gameManager.devotionIncrease1 = true;
                }
            }

            if (gameManager.monks.Count / gameManager.farms.Count > goodMonkAndFarmRatio)
            {
                gameManager.devotionIncrease = false;
                gameManager.devotionDecrease = true;
            }

            if (gameManager.monks.Count / gameManager.farms.Count >= badMonkAndFarmRatio75)
            {
                gameManager.devotionDecrease1 = true;
            }

            if (gameManager.monks.Count / gameManager.farms.Count >= badMonkAndFarmRatio50)
            {
                gameManager.devotionDecrease1 = false;
                gameManager.devotionDecrease2 = true;
            }

            if (gameManager.monks.Count / gameManager.farms.Count >= badMonkAndFarmRatio25)
            {
                gameManager.devotionDecrease2 = false;
                gameManager.devotionDecrease3 = true;
            }
        }

    }
    void CheckForNewDestination()
    {
        checkForNewDestination = targetObject.GetComponent<PathfindingTargetLocation>().moveToPosition;
    }
    IEnumerator RefreshPath()
    {
        Vector2 targetPositionOld = (Vector2)targetTransform.position + Vector2.up; // ensure != to target.position initially

        if (true)
        {
            if (targetPositionOld != (Vector2)targetTransform.position)
            {
                targetPositionOld = (Vector2)targetTransform.position;

                path = Pathfinding.RequestPath(transform.position, targetTransform.position);
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            StopCoroutine("RefreshPath");
            checkForNewDestination = false;
            yield return new WaitForSeconds(.25f);
        }
    }
    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    while (targetIndex >= path.Length)
                    {
                        yield return new WaitForSeconds(.25f);
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
        }
    }
    // Starts new pathfinding timer if distance between monk and movement target is small enough
    void CheckDistanceFromTarget()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) < movementCheckDistance)
        {
            targetObject.GetComponent<PathfindingTargetLocation>().startNewTargetTimer = true;
        }
    }
    // Draws movement path in editor
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                 Gizmos.color = Color.black;
                // Gizmos.DrawCube((Vector3)path[i], Vector3.one * .5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}