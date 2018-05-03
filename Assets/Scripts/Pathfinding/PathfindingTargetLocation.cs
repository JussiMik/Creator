using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargetLocation : MonoBehaviour
{
    public bool moveToPosition;
    public bool startNewTargetTimer;
    public bool drawLocationCircle;
    bool useBoundaryTimer;
    int layer;
    Vector2 position;
    public LayerMask unwalkable;

    /* [Space(10)]
     [Range(-15, 0f)]
     [SerializeField]
     float MinMovementRange;
     */

    [Range(0, 15f)]
    float movementRangeHorizontal;
    float movementRangeVertical;
    float newPosition;

    [Space(10)]
    [Range(0, 10f)]
    [SerializeField]
    float newPathTimerMin;
    [Range(0, 30f)]
    [SerializeField]
    float newPathTimerMax;

    [Space(10)]
    public float newTargetLocationTimer;
    [SerializeField]
    float boundaryTimer;
    float setBoundaryTimer;
    public float pathfindingOverlapCircle;
    Collider2D checkLocation;

    void Start()
    {
        setBoundaryTimer = boundaryTimer;
        unwalkable = LayerMask.GetMask("Unwalkable");
        newTargetLocationTimer = Random.Range(newPathTimerMin, newPathTimerMax);
        checkLocation = Physics2D.OverlapCircle(transform.position, pathfindingOverlapCircle);
    }

    void Update()
    {
        if (startNewTargetTimer == true)
        {
            FindNewTargetTimer();
        }
        if (useBoundaryTimer == true)
        {
            BoundaryTimer();
        }
    }
    // Randomize position and timer
    void MoveToNewLocation()
    {
        movementRangeHorizontal = Random.Range(-4f, 4f);
        movementRangeVertical = Random.Range(-4f, 4f);
        position = new Vector2(transform.position.x + movementRangeHorizontal, transform.position.y + movementRangeVertical);
        transform.position = position;
        newTargetLocationTimer = Random.Range(newPathTimerMin, newPathTimerMax);
        checkLocation = Physics2D.OverlapCircle(transform.position, pathfindingOverlapCircle);
        if (checkLocation == null)
        {
            moveToPosition = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // If target moves on building randomize new position
        if (other.gameObject.tag == "Building")
        {
            moveToPosition = false;
            MoveToNewLocation();
        }
        // If target moves on boundary set position to 0,0 and start faster timer for new position
        if (other.gameObject.tag == "Boundary")
        {
            useBoundaryTimer = true;
            position = new Vector2(0, 0);
            transform.position = position;
        }

    }
    void OnDrawGizmos()
    {
        if (drawLocationCircle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, pathfindingOverlapCircle);
        }
    }
    void FindNewTargetTimer()
    {
        newTargetLocationTimer -= Time.deltaTime;
        if (newTargetLocationTimer <= 0)
        {
            MoveToNewLocation();
            startNewTargetTimer = false;
        }
    }
    void BoundaryTimer()
    {
        boundaryTimer -= Time.deltaTime;
        if (boundaryTimer <= 0)
        {
            MoveToNewLocation();
            startNewTargetTimer = false;
            useBoundaryTimer = false;
            boundaryTimer = setBoundaryTimer;
        }
    }
}
