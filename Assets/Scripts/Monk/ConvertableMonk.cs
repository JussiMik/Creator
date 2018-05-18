using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertableMonk : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject targetObject;

    public GameManager gameManager;
    public Structure structure;

    public bool checkForNewDestination;
    bool startNewPathTimer;
    bool reachedDestination;
    public float speed;
    public float movementCheckDistance;
    Vector2[] path;
    int targetIndex;

    [Space(10)]
    public float goodMonkAndFarmRatio;
    public float badMonkAndFarmRatio75;
    public float badMonkAndFarmRatio50;
    public float badMonkAndFarmRatio25;
    [Space(10)]
    public float defaultDevotionDecreaseMp;
    public float devotionDecreaseMp1;
    public float devotionDecreaseMp2;
    public float devotionDecreaseMp3;
    [Space(10)]
    public float defaultConstructingTimerMp;
    public float constructingTimerMp1;
    public float constructingTimerMp2;
    public float constructingTimerMp3;
    [Space(10)]
    public float defaultFaithTimerMp;
    public float faithTimerMp1;
    public float faithTimerMp2;
    public float faithTimerMp3;

    public bool isConverted;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        structure = GameObject.Find("Game Manager").GetComponent<Structure>();
        gameManager.convertableMonks.Add(gameObject);
        InvokeRepeating("CheckForNewDestination", 0.5f, 1.5f);
        InvokeRepeating("CheckDistanceFromTarget", 1f, 2.5f);
        startNewPathTimer = targetObject.GetComponent<PathfindingTargetLocation>().startNewTargetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkForNewDestination == true)
        {
            StartCoroutine("RefreshPath");
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
    void CheckDistanceFromTarget()
    {
        if (Vector2.Distance(transform.position, targetTransform.position) < movementCheckDistance)
        {
            targetObject.GetComponent<PathfindingTargetLocation>().startNewTargetTimer = true;
        }
    }
}
