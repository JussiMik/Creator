using UnityEngine;
using System.Collections;

public class _TempMonk : MonoBehaviour
{

    public Transform targetTransform;
    public GameObject targetObject;
    public bool checkForNewDestination;
    public float speed = 20;
    Vector2[] path;
    int targetIndex;

    void Start()
    {
        //  StartCoroutine(RefreshPath());
        InvokeRepeating("CheckForNewDestination", 0.5f, 1.5f);
    }

    void Update()
    {
        if (checkForNewDestination == true)
        {
            RefreshPath();
            checkForNewDestination = false;
        }
    }
    void CheckForNewDestination()
    {
        checkForNewDestination = targetObject.GetComponent<PathfindingTargetLocation>().moveToPosition;
    }

    private void RefreshPath()
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
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

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