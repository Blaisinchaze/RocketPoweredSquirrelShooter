using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AiUnit:Combatant
{
    [Space]
    public Enemies enemyType;
    public GameObject deathExplosion;
    public Animator bodyAnimator;
    private Ai_Weapon weapon;

    #region Pathfinding Variables
    public enum Facing
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4
    }
    public enum State
    {
        NULL = -1,
        MOVE = 0,
        ACTION = 1
    }
    [Space]
    public float range;
    public float actionDelay;
    public float moveSpeed;
    [Space]
    public float minDistance = 0.05f;
    public float pathUpdateRate;
    public float colliderRadius;
    public float maxStraightLineDistance = 3;

    private Pathfinder pathfinder;

    internal Facing direction;
    internal bool moving;

    private Vector2Int currentGridPosition;
    private Vector2 moveToPos;
    private GameObject targetPlayer;
    private State state;
    private float actionTimer;
    float distanceToTarget;
    bool canSeeTarget;

    #endregion

    public void Start()
    {
        weapon = GetComponentInChildren<Ai_Weapon>();

        //Pathfinding initialistion
        targetPlayer = AiController.Instance.player;
        pathfinder = new Pathfinder(colliderRadius, AiController.Instance.navGrid);
    }

    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(transform.position, moveToPos, moveSpeed * Time.deltaTime);
        UpdateDirection();
    }

    //returns true if it runs pathfinding
    public bool UnitUpdate() 
    {
        UpdateState();
        return UpdateAction();
    }

    private void UpdateState() 
    {
        currentGridPosition = pathfinder.navGrid.NodeFromWorld(transform.position).gridPosition;
        canSeeTarget = CheckLineToTarget(targetPlayer.transform.position);
        distanceToTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);

        if (distanceToTarget <= range && canSeeTarget)
        {
            state = State.ACTION;
        }
        else
        {
            state = State.MOVE;
        }
    }

    //returns true if a delay is needed
    private bool UpdateAction()
    {
        switch (state)
        {
            case State.NULL:
                Debug.Log("WRONG STATE ON AI");
                break;

            case State.MOVE:
                moveToPos = targetPlayer.transform.position;

                if(!canSeeTarget)
                {
                    pathfinder.FindPath(pathfinder.navGrid.NodeFromWorld(targetPlayer.transform.position).gridPosition, currentGridPosition);
                    if (pathfinder.currentRoute.Count > 0)
                    {
                        moveToPos = pathfinder.currentRoute[0].worldPosition;
                        if (Vector3.Distance(transform.position, pathfinder.currentRoute[0].worldPosition) <= minDistance)
                        {
                            pathfinder.currentRoute.RemoveAt(0);
                        }
                    }

                    return true;
                }
                break;

            case State.ACTION:
                //This needs to be changed to whatever action needs to take place.
                if (actionTimer > 0)
                {
                    actionTimer -= Time.deltaTime;
                }
                else
                {
                    weapon.Fire();
                    //Debug.Log("Attack");
                    actionTimer = actionDelay;
                }

                break;
        }

        return false;
    }

    private void UpdateDirection()
    {
        Vector2 diff = moveToPos - (Vector2)transform.position;

        TiltBot(diff);

        if (diff == Vector2.zero) return;

        bodyAnimator.SetFloat("Horizontal", diff.x);
        bodyAnimator.SetFloat("Vertical", diff.y);
        bodyAnimator.SetFloat("Speed", diff.sqrMagnitude);
    }

    private bool CheckLineToTarget(Vector3 position)
    {
        int mask = 1 << LayerMask.NameToLayer("Walls");
        position = Vector3.MoveTowards(position, transform.position, colliderRadius);

        return !Physics2D.CircleCast(transform.position, colliderRadius, position - transform.position,
            Vector3.Distance(transform.position, position), mask);
    }

    // not pathfinding related
    public override void Die()
    {
        Destroy(Instantiate(deathExplosion, transform.position, Quaternion.identity), 3);
        FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Body/Explosion");

        if (AiController.Instance != null)
        {
            AiController.Instance.KillEnemy(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Temp")
        {
            Die();
        }
    }

    public void TiltBot(Vector2 tilt)
    {
        float tiltAmount = 0;
        if (tilt.x > 0)
        {
            tiltAmount = -15;
            if (tilt.y != 0)
                tiltAmount = -5;

        }
        if (tilt.x < 0)
        {
            tiltAmount = 15;
            if (tilt.y != 0)
                tiltAmount = 5;
        }
        Vector3 desiredTilt = new Vector3(0, 0, tiltAmount);
        Quaternion q = Quaternion.Euler(desiredTilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 5);
    }
}

public class Pathfinder 
{
    public List<GridNode> currentRoute = new List<GridNode>();
    public NavGrid navGrid;
    public Vector3 currentPos;
    private float colliderRadius;

    List<GridNode> open = new List<GridNode>();
    HashSet<GridNode> closed = new HashSet<GridNode>();

    GridNode targetNode;
    GridNode startNode;

    int layerMask;

    public Pathfinder(float colliderRadius, NavGrid navGrid) 
    {
        this.colliderRadius = colliderRadius;
        this.navGrid = navGrid;

        layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        layerMask = layerMask & ~(1 << LayerMask.NameToLayer("AI"));
    }

    public void FindPath(Vector2Int target, Vector2Int start)
    {
        if ((target - start).magnitude <= 1)
        {
            currentRoute.Clear();
            return;
        }

        //Debug.Log("A");
        open.Clear();
        closed.Clear();

        targetNode = navGrid.NodeFromGridSpace(target);
        startNode = navGrid.NodeFromGridSpace(start);
        
        open.Add(startNode);
        GridNode currentNode;
        //Debug.Log("B");

        while (open.Count > 0)
        {
            //Debug.Log("C");
            currentNode = open[0];

            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].fCost <= currentNode.fCost && open[i].hCost < currentNode.hCost)
                {
                    currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (GridNode neighbourNode in GetNeighbouringGridSpaces(currentNode))
            {
                if (closed.Contains(neighbourNode) || neighbourNode.pathable == false)
                {
                    if (neighbourNode != targetNode) continue;
                }

                float newMovementCostToNeighbour = currentNode.gCost + DistanceFrom(currentNode.gridPosition, neighbourNode.gridPosition);

                if (neighbourNode.gridPosition.x != currentNode.gridPosition.x && neighbourNode.gridPosition.y != currentNode.gridPosition.y)
                {
                    newMovementCostToNeighbour += 0.5f;
                }

                if (newMovementCostToNeighbour < neighbourNode.gCost || !open.Contains(neighbourNode))
                {
                    neighbourNode.gCost = newMovementCostToNeighbour;
                    neighbourNode.hCost = DistanceFrom(neighbourNode.gridPosition, target);
                    neighbourNode.parent = currentNode;

                    if (!open.Contains(neighbourNode))
                    {
                        open.Add(neighbourNode);
                    }

                    if (neighbourNode == targetNode)
                    {
                        RetracePath(startNode, targetNode);
                        return;
                    }
                }
            }
        }
    }

    private void RetracePath(GridNode startNode, GridNode targetNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        currentRoute = path;
        //SimplifyPath();
    }

    private void SimplifyPath()
    {
        int nodeCount = currentRoute.Count;

        if (nodeCount <= 1)
        {
            return;
        }

        for (int i = 0; i < nodeCount; i++)
        {
            bool cleanHit = false;
            int j = nodeCount - 1;

            while (cleanHit == false && j > i)
            {
                if (Physics2D.CircleCast(currentPos, colliderRadius, currentRoute[j].worldPosition - currentPos, Vector3.Distance(currentPos, currentRoute[j].worldPosition), layerMask))
                {
                    j--;
                }
                else
                {
                    cleanHit = true;
                }
            }

            currentRoute.RemoveRange(i, j - i);
            nodeCount = currentRoute.Count;
        }
    }

    private GridNode[] GetNeighbouringGridSpaces(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //The diagonals
                if (Mathf.Abs(x) == Mathf.Abs(y))
                {
                    //continue;
                }

                int checkX = node.gridPosition.x + x;
                int checkZ = node.gridPosition.y + y;

                // check if the grid space is on the grid
                if (checkX >= 0 && checkX <= navGrid.gridSize.x - 1 && checkZ >= 0 && checkZ <= navGrid.gridSize.y - 1)
                {
                    neighbours.Add(navGrid.grid[checkX, checkZ]);
                }
            }
        }

        return neighbours.ToArray();
    }

    private float DistanceFrom(Vector2Int start, Vector2Int end)
    {
        int dstX = Mathf.Abs(end.x - start.x);
        int dstY = Mathf.Abs(end.y - start.y);

        return Mathf.Sqrt((dstX * dstX) + (dstY * dstY));
    }
}