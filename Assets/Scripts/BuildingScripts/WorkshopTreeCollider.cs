using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopTreeCollider : MonoBehaviour
{
    public float totalTreeAmount;
    public bool woodTimerCollision;
    WoodWorkshopCS parent;

    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<WoodWorkshopCS>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            totalTreeAmount++;
            parent.SendMessage("UpdateValues", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            woodTimerCollision = true;
            parent.SendMessage("UpdateValues", SendMessageOptions.DontRequireReceiver);
        }
    }
}
