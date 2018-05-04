using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundBar : MonoBehaviour
{
    public GameObject originalParent;
    GameObject background;

    public Structure structure;
    public Vector3 offset;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (originalParent == null)
        {
            Destroy(gameObject);
        }
        if (originalParent != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(originalParent.transform.position) + offset;
        }
    }
}