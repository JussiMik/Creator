using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTargetLocation : MonoBehaviour
{
    public bool moveToPosition;
    public LayerMask unwalkable;
    int layer;
    float xPosition;
    float yPosition;
    Vector2 pos;
    [SerializeField]
    float newTargetLocationTimer;

    // Use this for initialization
    void Start()
    {
        unwalkable = LayerMask.GetMask("Unwalkable");
        newTargetLocationTimer = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        //  newTargetLocationTimer -= Time.deltaTime;

        if (newTargetLocationTimer <= 0)
        {
            MoveToNewLocation();
        }
    }

    void MoveToNewLocation()
    {
        xPosition = Random.Range(-5f, 5f);
        yPosition = Random.Range(-5f, 5f);
        pos = new Vector2(xPosition, yPosition);
        transform.position = pos;
        newTargetLocationTimer = Random.Range(5f, 15f);
        moveToPosition = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Building")
        {
            moveToPosition = false;
            Debug.Log("Pathfinding target on unwalkable area");
        }
        else
        {
            moveToPosition = true;
        }
    }
}
