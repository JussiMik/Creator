using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleCS : Structure
{
    private bool addedToList;

    //public float radius = 10f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        originalFaithTargetTime = faithTargetTime;

        normalSpeedConstructing = true;
        faithCollected = true;
        addedToList = false;
        ConstructingStructures();

        gameManager.UseFaith(constructingCost);

        name = "Temple";
        type = "Faith";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shrine")
        {
            Debug.Log("Koskettaa");
            Destroy(collision.gameObject);
        }

        /*
        if (collision.gameObject.CompareTag("ShrineCollider"))
        {
            Debug.Log("Koskettaa");

            Collider[] colliders = Physics.OverlapSphere(collision.gameObject.transform.position, radius);
            //Collider[] colliders = Physics2D.OverlapArea(collision.transform.position, collision.transform.position, radius);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.tag == "Shrine")
                {
                    Destroy(col.gameObject);
                }
            }
        }*/
    }

    protected override void Update()
    {
        base.Update();

        if (constructingDone == true && addedToList == false)
        {
            AddToList();
            gameManager.GiveSanctityPoints(sanctityPointAmount);
            faithTimer = true;
        }
    }

    private void AddToList()
    {
        gameManager.faithBuildings.Add(gameObject);
        gameManager.faithMultipliers.Add(faithMultiplier);
        addedToList = true;
    }
}
