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

    internal NavGrid navGrid;
    internal Facing direction;
    internal bool moving;

    private bool findingPath = false;
    private Vector2Int currentGridPosition;
    private List<GridNode> currentRoute = new List<GridNode>();
    private GameObject targetPlayer;
    private State state;
    private float actionTimer;
    float distanceToTarget;
    #endregion

    public void Start()
    {
        weapon = GetComponentInChildren<Ai_Weapon>();

        //Pathfinding initialistion
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        navGrid = FindObjectOfType<NavGrid>();
        findingPath = false;
    }

    protected override void Update()
    {
        base.Update();

        //pathfinding updates
        currentGridPosition = navGrid.NodeFromWorld(transform.position).gridPosition;
        UpdateAction();
        UpdateDirection();
    }

    void UpdateAction()
    {
        bool canSeeTarget = CheckLineToTarget(targetPlayer.transform.position);
        distanceToTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);

        Vector3 moveToPos = Vector3.zero;

        if (distanceToTarget <= range && canSeeTarget)
        {
            state = State.ACTION;
        }
        else
        {
            state = State.MOVE;
        }

        switch (state)
        {
            case State.NULL:
                Debug.Log("WRONG STATE ON AI");
                break;

            case State.MOVE:
                if (canSeeTarget && distanceToTarget < maxStraightLineDistance)
                {
                    moveToPos = targetPlayer.transform.position;
                    currentRoute.Clear();
                }
                else if (!findingPath)
                {
                    StartCoroutine(FindPath(navGrid.NodeFromWorld(targetPlayer.transform.position).gridPosition));
                }

                if (currentRoute.Count > 0)
                {
                    currentGridPosition = navGrid.NodeFromWorld(transform.position).gridPosition;
                    if (Vector3.Distance(transform.position, currentRoute[0].worldPosition) <= minDistance)
                    {
                        currentRoute.RemoveAt(0);
                    }
                    moveToPos = currentRoute[0].worldPosition;
                }

                transform.position = Vector3.MoveTowards(transform.position, moveToPos, moveSpeed * Time.deltaTime);

                break;

            case State.ACTION:
                //This needs to be changed to whatever action needs to take place.
                if (actionTimer > 0)
                {
                    actionTimer -= Time.deltaTime;
                    break;
                }

                weapon.Fire();

                actionTimer = actionDelay;
                break;
        }

        
    }

    private void UpdateDirection()
    {
        moving = true;

        float x;
        float y;

        if (currentRoute.Count <= 0)
        {
            Vector3 diff = targetPlayer.transform.position - transform.position;
            x = diff.x;
            y = diff.y;
        }
        else
        {
            Vector2Int diff = currentRoute[0].gridPosition - currentGridPosition;
            x = diff.x;
            y = diff.y;
        }

        //if (ph.transform != this.transform)
        //{
        //    ph.transform.localRotation = Quaternion.LookRotation(new Vector3(x,y,0) , transform.forward);
        //}

        Vector2 movementDirection = new Vector2(x, y);

        TiltBot(movementDirection);

        if (movementDirection == Vector2.zero)
        {
            return;
        }

        bodyAnimator.SetFloat("Horizontal", movementDirection.x);
        bodyAnimator.SetFloat("Vertical", movementDirection.y);
        bodyAnimator.SetFloat("Speed", movementDirection.sqrMagnitude);


    }

    private float DistanceFrom(Vector2Int start, Vector2Int end)
    {
        int dstX = Mathf.Abs(end.x - start.x);
        int dstY = Mathf.Abs(end.y - start.y);

        return Mathf.Sqrt((dstX * dstX) + (dstY * dstY));
    }

    IEnumerator FindPath(Vector2Int target)
    {
        findingPath = true;

        List<GridNode> open = new List<GridNode>();
        HashSet<GridNode> closed = new HashSet<GridNode>();

        GridNode targetNode = navGrid.NodeFromGridSpace(target);
        GridNode startNode = navGrid.NodeFromGridSpace(currentGridPosition);
        open.Add(startNode);

        while (open.Count > 0)
        {
            GridNode currentNode = open[0];

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
                yield return new WaitForSeconds(pathUpdateRate);
                findingPath = false;
                yield break;
            }

            foreach (GridNode neighbourNode in GetNeighbouringGridSpaces(currentNode))
            {
                if (closed.Contains(neighbourNode) || neighbourNode.pathable == false)
                {
                    if (neighbourNode != targetNode)
                    {
                        continue;
                    }
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
                        yield return new WaitForSeconds(pathUpdateRate);
                        findingPath = false;
                        yield break;
                    }
                }
            }
        }

        yield return new WaitForSeconds(pathUpdateRate);
        findingPath = false;
        yield break;
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
        SimplifyPath();
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

            RaycastHit2D hit;
            int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
            layerMask = layerMask & ~(1 << LayerMask.NameToLayer("AI"));

            while (cleanHit == false && j > i)
            {
                if (hit = Physics2D.CircleCast(transform.position, colliderRadius, currentRoute[j].worldPosition - transform.position, Vector3.Distance(transform.position, currentRoute[j].worldPosition), layerMask))
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

    private List<GridNode> GetNeighbouringGridSpaces(GridNode node)
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

        return neighbours;
    }

    private bool CheckLineToTarget(Vector3 position)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Walls");

        RaycastHit2D hit;

        if (hit = Physics2D.CircleCast(transform.position, colliderRadius, position - transform.position, Vector3.Distance(transform.position, position), layerMask))
        {
            return false;
        }

        return true;
    }

    // not pathfinding related
    public override void Die()
    {
        Destroy(Instantiate(deathExplosion, transform.position, Quaternion.identity), 3);
        FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Body/Explosion");

        AiController cont;
        if (transform.parent.TryGetComponent(out cont))
        {
            cont.KillEnemy(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmosSelected()
    {

        if (currentRoute == null)
        {
            return;
        }

        if (currentRoute.Count > 0)
        {
            foreach (GridNode node in currentRoute)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(node.worldPosition, 0.1f);
            }
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
