using UnityEngine;
using System.Collections;

public class Monk : MonoBehaviour
{

    Animator animator;

    public Transform targetTransform;
    public GameObject targetObject;

    public GameManager gameManager;
    public Structure structure;

    public bool checkForNewDestination;
    bool startNewPathTimer;
    bool reachedDestination;
    public float speed;
    public float movementCheckDistance;
    public Vector2[] path;
    int targetIndex;

    public bool movingUp;
    public bool movingDown;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        structure = GameObject.Find("Game Manager").GetComponent<Structure>();
        InvokeRepeating("CheckForNewDestination", 0.5f, 1.5f);
        InvokeRepeating("CheckDistanceFromTarget", 1f, 2.5f);
        startNewPathTimer = targetObject.GetComponent<PathfindingTargetLocation>().startNewTargetTimer;
        gameManager.monks.Add(gameObject);
        animator = GetComponent<Animator>();

        //gameManager.constructingTimerMp = defaultConstructingTimerMp;
    }

    void Update()
    {
        if (checkForNewDestination == true)
        {
            StartCoroutine("RefreshPath");
        }
        if (path.Length == 0)
        {
            movingUp = false;
            movingDown = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("movingUp", false);
            animator.SetBool("movingDown", false);
        }
    }


    // DELET THIS when dormitory is done & move UpdateMonkCount();
    /*  public void SpawnNewMonk()
      {
          GameObject spawnedMonk = Instantiate(gameObject, new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z), transform.rotation);
          gameManager.monks.Add(spawnedMonk);

      }
      */

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
            animator.SetBool("isMoving", true);

            //uppuu
            if(path[0].y > gameObject.transform.position.y)
            {
                movingUp = true;
                animator.SetBool("movingUp", true);
            }

            //downuu
            if(path[0].y < gameObject.transform.position.y)
            {
                movingDown = true;
                animator.SetBool("movingDown", true);
            }

            //rightuu
            if(path[0].x > gameObject.transform.position.x && movingUp == true)
            {
                Debug.Log("Saatanansaatana");
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            //leftuu
           else if(path[0].x < gameObject.transform.position.x && movingUp == false)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

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