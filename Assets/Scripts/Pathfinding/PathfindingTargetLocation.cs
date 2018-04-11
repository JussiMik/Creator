using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargetLocation : MonoBehaviour
{
    public bool moveToPosition;
    public bool startNewTargetTimer;
    // public Transform monk;
    public LayerMask unwalkable;
    int layer;
    public int unwalkableLocationCount;
    float newHorizontalPosition;
    float newVerticalPosition;
    Vector2 position;
    Vector2 oldPosition;
    [SerializeField]
    float newTargetLocationTimer;

    // Use this for initialization
    void Start()
    {
        unwalkable = LayerMask.GetMask("Unwalkable");
        //  monk = transform.parent.Find("Sprite");
       // startNewTargetTimer = transform.parent.GetComponentInChildren<_TempMonk>().reachedDestination;
        newTargetLocationTimer = Random.Range(2.5f, 5f);
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startNewTargetTimer == true)
        {
            FindNewTargetTimer();
        }
    }
    void StartTimer()
    {

    }

    void MoveToNewLocation()
    {
        newHorizontalPosition = Random.Range(-15f, 15f);
        newVerticalPosition = Random.Range(-15f, 15f);
        oldPosition = transform.position;
        position = new Vector2(transform.position.x + newHorizontalPosition, transform.position.y + newVerticalPosition);
        transform.position = position;
        newTargetLocationTimer = Random.Range(2.5f, 5f);
        if(unwalkableLocationCount >= 5)
        {
            position = new Vector2(0,0);
            transform.position = position;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Building")
        {
            unwalkableLocationCount++;
            Debug.Log("Hit unwalkable terrain");
            moveToPosition = false;
            transform.position = oldPosition;
            MoveToNewLocation();
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
}
