using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesBlessing : MonoBehaviour
{
    LayoutManager layoutManager;
    DragNDrop dragNDrop;

    void Start()
    {
        layoutManager = GameObject.FindGameObjectWithTag("").GetComponent<LayoutManager>();
        dragNDrop = GameObject.FindGameObjectWithTag("").GetComponent<DragNDrop>();
    }

    void Update()
    {

    }

    public void SpawnTree()
    {
        layoutManager.SpawnStructure(dragNDrop.curDraBuilding, dragNDrop.toBeColorized, new Vector2(2, 2));
    }
}
