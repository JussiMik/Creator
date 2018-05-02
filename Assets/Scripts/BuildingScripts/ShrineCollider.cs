using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineCollider : MonoBehaviour
{
    ShrineCS shrine;
    GameManager gameManager;

    public float totalShrineAmount;

    public bool allowTempleConstructing;

    void Start()
    {
        shrine = transform.parent.gameObject.GetComponent<ShrineCS>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShrineCollider")
        {
            totalShrineAmount++;
            shrine.SendMessage("UpdateValues", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ShrineCollider")
        {
            if (totalShrineAmount >= 8)
            {
                allowTempleConstructing = true;
                shrine.SendMessage("UpdateBool", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
