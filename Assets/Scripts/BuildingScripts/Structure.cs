using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField]
    private bool constructingTimer;

    [SerializeField]
    private float constructingTime;

    public bool constructingDone;

    public int faithAmount;
    //public int faithMultiplier;

    // Use this for initialization
    void Start()
    {
        constructingDone = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (constructingTimer == true)
        {
            constructingTime -= Time.deltaTime;

            if (constructingTime <= 0)
            {
                constructingTime = 0f;
                constructingTimer = false;
                constructingDone = true;
            }
        }
    }

    public void ConstructingStructures()
    {
        constructingTimer = true;
    }
}
