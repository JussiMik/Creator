using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public Vector3 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeZ;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSizeX / nodeDiameter);
        gridSizeZ = Mathf.RoundToInt(gridSizeZ / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeZ];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2; //Vector3.up??
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int x = 0; x < gridSizeZ; x++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter * nodeRadius) + Vector3.forward * (x * nodeDiameter + nodeRadius);
                bool walkable = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,gridWorldSize.z, 1 ));
    }
}
