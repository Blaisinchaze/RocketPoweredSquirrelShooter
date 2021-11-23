using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridNode
{
    public Vector3 worldPosition;
    public Vector2Int gridPosition;

    public float gCost;
    public float hCost;
    public float fCost { get { return gCost + hCost; } }
    public bool pathable = true;

    public GridNode parent;

    public GridNode(Vector3 _worldPos, Vector2Int _gridPosition, bool _pathable)
    {
        worldPosition = _worldPos;
        gridPosition = _gridPosition;
        pathable = _pathable;
    }

}

public class NavGrid : MonoBehaviour
{
    public float unitNodeRatio;
    [Space]
    public bool displayGrid = false;
    [Space]
    public Tilemap tilemap;

    internal Vector2 gridWorldSize;
    internal Vector2 nodeSize;
    internal GridNode[,] grid = null;

    internal Vector2Int gridSize;

    internal void Start()
    {
        nodeSize = tilemap.cellSize;

        if (nodeSize.x % 0.25f != 0 || nodeSize.y % 0.25f != 0)
        {
            Debug.LogError("TILEMAP NOT A VALID SIZE - MUST BE A MULTIPLE OF 0.25");
        }

        gridSize = new Vector2Int(tilemap.size.x, tilemap.size.y);

        gridWorldSize.x = gridSize.x * (1/nodeSize.x);
        gridWorldSize.y = gridSize.y * (1/nodeSize.y);

        //Debug.Log(nodeSize + " " + gridSize + " " + gridWorldSize);


        CreateGrid();
    }

    public GridNode NodeFromWorld(Vector3 worldPos)
    {
        float shortest = 999;
        GridNode returningNode = null;
        foreach (GridNode node in grid)
        {
            float distance = Vector3.Distance(worldPos, node.worldPosition);
            if (distance < shortest)
            {
                returningNode = node;
                shortest = distance;
            }
        }

        if (returningNode != null)
        {
            return returningNode;
        }

        return null;
    }

    void CreateGrid()
    {
        grid = new GridNode[gridSize.x, gridSize.y];

        Vector3 worldBottomLeft = tilemap.CellToWorld(tilemap.cellBounds.min);
        Vector3 worldTopRight = tilemap.CellToWorld(tilemap.cellBounds.max);

        Debug.Log(worldBottomLeft + " " + worldTopRight);

        int i = 0;
        int gridX = (int)worldBottomLeft.x;
        int j = 0;
        int gridY = (int)worldBottomLeft.y;

        for (float worldX = worldBottomLeft.x; worldX < worldTopRight.x; worldX += nodeSize.x)
        {
            for (float worldY = worldBottomLeft.y; worldY < worldTopRight.y; worldY += nodeSize.y)
            {
                Vector3Int gridV3 = new Vector3Int(gridX, gridY, 0);
                Vector3 worldPoint = tilemap.CellToWorld(gridV3) + new Vector3(nodeSize.x * 0.5f, nodeSize.y * 0.5f, 0);
                grid[i, j] = new GridNode(worldPoint, new Vector2Int(i, j), tilemap.HasTile(gridV3));

                if (grid[i,j] == null)
                {
                    Debug.Log(i + " " + j + " " + gridV3 + " " + worldPoint);
                }

                if (grid[i,j].pathable)
                {
                    //Debug.Log(i + " " + j + " " + gridV3 + " " + worldPoint);
                    if (Physics2D.BoxCast(worldPoint, nodeSize * unitNodeRatio, 0, Vector2.up, 0.01f, 1 << LayerMask.NameToLayer("Walls")))
                    {
                        grid[i, j].pathable = false;
                    }
                }
                gridY++;
                j++;
            }

            gridX++;
            i++;

            gridY = (int)worldBottomLeft.y;
            j = 0;
        }
    }

    internal GridNode NodeFromGridSpace(Vector2Int gridSpace)
    {
        foreach (GridNode node in grid)
        {
            if (node.gridPosition == gridSpace)
            {
                return node;
            }
        }
        return grid[0, 0];
    }

    private void OnDrawGizmos()
    {
        if (grid != null && displayGrid)
        {
            foreach (GridNode item in grid)
            {
                if (item == null)
                {
                    Debug.Log("broke");
                }
                else if (item.pathable)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(item.worldPosition, nodeSize * 0.8f);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(item.worldPosition, nodeSize * 0.8f);
                }
            }
        }
    }
}
