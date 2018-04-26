using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarryRockCollider : MonoBehaviour
{
    public float totalRockAmount;
    public bool rockTimerCollision;
    QuarryCS parent;
    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<QuarryCS>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            totalRockAmount++;
            parent.SendMessage("UpdateValues", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            rockTimerCollision = true;
            parent.SendMessage("UpdateValeus", SendMessageOptions.DontRequireReceiver);
        }
    }
}
