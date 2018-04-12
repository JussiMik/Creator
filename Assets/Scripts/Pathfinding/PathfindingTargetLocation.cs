using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargetLocation : MonoBehaviour
{
    public bool moveToPosition;
    public bool startNewTargetTimer;
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
    [SerializeField]
    float MovementRange;
    float newPosition;

    [Space(10)]
    [Range(0, 10f)]
    [SerializeField]
    float newPathTimerMin;
    [Range(0, 30f)]
    [SerializeField]
    float newPathTimerMax;

    [Space(10)]
    float newTargetLocationTimer;
    [SerializeField]
    float boundaryTimer;
    float setBoundaryTimer;

    void Start()
    {
        setBoundaryTimer = boundaryTimer;
        unwalkable = LayerMask.GetMask("Unwalkable");
        newTargetLocationTimer = Random.Range(newPathTimerMin, newPathTimerMax);
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
        newPosition = Random.Range(-MovementRange, MovementRange);
        position = new Vector2(transform.position.x + newPosition, transform.position.y + newPosition);
        transform.position = position;
        newTargetLocationTimer = Random.Range(newPathTimerMin, newPathTimerMax);
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
        else if (other.gameObject.tag == "Boundary")
        {
            useBoundaryTimer = true;
            position = new Vector2(0,0);
            transform.position = position;
        }
        else
        {
            moveToPosition = true;
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
