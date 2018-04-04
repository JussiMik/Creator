using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField]
    private bool constructingTimer;

    [SerializeField]
    private float constructingTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (constructingTimer == true)
        {
            constructingTime -= Time.deltaTime;

            if (constructingTime <= 0)
            {
                constructingTime = 0f;
            }
        }
    }

    public void ConstructingStructures()
    {
        constructingTimer = true;
    }
}
