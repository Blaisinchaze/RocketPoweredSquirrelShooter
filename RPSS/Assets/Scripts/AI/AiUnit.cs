using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AiUnit:Combatant
{
    enum State
    {
        NULL = -1,
        MOVE = 0,
        ATTACK = 1
    }

    NavGrid navGrid;
    [Space]
    public Enemies enemyType;
    public float range;
    public float attackDelay;
    public float moveSpeed;
    [Space]
    public float minDistance = 0.05f;
    public float pathUpdateRate;
    public float colliderRadius;
    public int maxNodesToSimplify;

    private Vector2Int currentGridPosition;
    private List<GridNode> currentRoute = new List<GridNode>();

    //private float timer = 0;
    private GameObject targetPlayer;
    private Ai_Weapon weapon;

    private List<Collider2D> colliders = new List<Collider2D>();

    //public ParticleHandle ph;
    //public ParticleHandle chasePh;
    private Animator anim;

    public enum Facing
        {
            NONE = 0,
            LEFT = 1,
            RIGHT = 2,
            UP = 3,
            DOWN = 4
        }
    internal Facing direction;
    internal bool moving;

    //private float stepTimer = 0f;
    private Vector2 prevPos;

    bool findingPath = false;
    private State state;
    private float attackTimer;

    public void Start()
    {
        //if (ph == null)
        //{
        //    ph = GetComponent<ParticleHandle>();
        //}

        targetPlayer = GameObject.FindGameObjectWithTag("Player");

        foreach (Collider2D item in GetComponentsInChildren<Collider2D>())
        {
            colliders.Add(item);
        }

        navGrid = FindObjectOfType<NavGrid>();

        anim = GetComponent<Animator>();
        prevPos = transform.position;

        weapon = GetComponentInChildren<Ai_Weapon>();
    }

    private void UpdateDirection()
    {
        moving = true;

        float x;
        float xClamped;
        float y;
        float yClamped;
        if (currentRoute.Count <= 0)
        {
            Vector3 diff = transform.position - targetPlayer.transform.position;
            x = diff.x;
            y = diff.y;
        }
        else
        {
            Vector2Int diff = currentGridPosition - currentRoute[0].gridPosition;
            x = diff.x;
            y = diff.y;
        }

        xClamped = Mathf.Clamp(x, -1, 1);
        yClamped = Mathf.Clamp(y, -1, 1);

        if (Mathf.Abs(xClamped) > Mathf.Abs(yClamped))
        {
            if (xClamped > 0)
            {
                direction = Facing.LEFT;
            }
            else if (xClamped < 0)
            {
                direction = Facing.RIGHT;
            }
        }
        else
        {
            if (yClamped > 0)
            {
                direction = Facing.DOWN;
            }
            else if (yClamped < 0)
            {
                direction = Facing.UP;
            }
        }

        //anim.SetInteger("Direction", (int)direction);

        //if (ph.transform != this.transform)
        //{
        //    ph.transform.localRotation = Quaternion.LookRotation(new Vector3(x,y,0) , transform.forward);
        //}
    }

    void Update()
    {
        CheckToggles();

        currentGridPosition = navGrid.NodeFromWorld(transform.position).gridPosition;

        UpdateAction();

        UpdateDirection();
        //if (moving == false)
        //{
        //    ph.SetVariable(0);
        //}
        //else
        //{
        //    ph.SetVariable(1);
        //}

        float velocity = ((Vector2)transform.position - prevPos).magnitude / Time.deltaTime;
        ProcessFootStepsFX(gameObject.transform, Mathf.Ceil(velocity));
        prevPos = transform.position;

    }

    void UpdateAction()
    {
        Vector3 moveToPos = Vector3.zero;

        bool canSeePlayer = CheckLineToTarget(targetPlayer.transform.position);

        if (Vector3.Distance(transform.position, targetPlayer.transform.position) <= range && canSeePlayer)
        {
            state = State.ATTACK;
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
                if (canSeePlayer)
                {
                    moveToPos = targetPlayer.transform.position;
                    currentRoute.Clear();
                }
                else if (!findingPath)
                {
                    StartCoroutine(FindPath(navGrid.NodeFromWorld(targetPlayer.transform.position).gridPosition));
                }
                else
                {
                    if (currentRoute.Count <= 0)
                    {
                        break;
                    }

                    if (Vector3.Distance(transform.position, currentRoute[0].worldPosition) < minDistance)
                    {
                        currentRoute.RemoveAt(0);
                    }
                    SimplifyPath();
                    moveToPos = currentRoute[0].worldPosition;
                }

                if (currentRoute.Count > 0)
                {
                    currentGridPosition = navGrid.NodeFromWorld(transform.position).gridPosition;
                    moveToPos = currentRoute[0].worldPosition;
                }

                transform.position = Vector3.MoveTowards(transform.position, moveToPos, moveSpeed * Time.deltaTime);
                break;

            case State.ATTACK:
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                    break;
                }

                weapon.Fire();

                attackTimer = attackDelay;
                break;
        }

        
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
                if (open[i].fCost < currentNode.fCost ||
                    open[i].fCost == currentNode.fCost)
                {
                    if (open[i].hCost < currentNode.hCost)
                    {
                        currentNode = open[i];
                    }
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

                int newMovementCostToNeighbour = currentNode.gCost + (int)DistanceFrom(currentNode.gridPosition, neighbourNode.gridPosition);

                if (neighbourNode.gridPosition.x != currentNode.gridPosition.x && neighbourNode.gridPosition.y != currentNode.gridPosition.y)
                {
                    newMovementCostToNeighbour += 2;
                }

                if (newMovementCostToNeighbour < neighbourNode.gCost || !open.Contains(neighbourNode))
                {
                    neighbourNode.gCost = newMovementCostToNeighbour;
                    neighbourNode.hCost = (int)DistanceFrom(neighbourNode.gridPosition, target);
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

        //SetCollidersActive(false);

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

        //SetCollidersActive(true);
    }

    private List<GridNode> GetNeighbouringGridSpaces(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //ignore the diagonals
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

    private void SetCollidersActive(bool active)
        {
            foreach (Collider2D item in colliders)
            {
                item.enabled = active;
            }
        }

    private bool CheckLineToTarget(Vector3 position)
    {
        //int layerMask = ~(1 << LayerMask.NameToLayer("Background"));
        //layerMask = layerMask & ~(1 << LayerMask.NameToLayer("AI"));

        int layerMask = 1 << LayerMask.NameToLayer("Walls");

        RaycastHit2D hit;

        if (hit = Physics2D.CircleCast(transform.position, colliderRadius, transform.position - position, Vector3.Distance(transform.position, position), layerMask))
        {
            return false;
        }

        //if (hit = Physics2D.Raycast(transform.position, position,
        //    Vector3.Distance(transform.position, position), layerMask))
        //{
        //    //Debug.Log(hit.collider.gameObject);
        //    return false;
        //}

        return true;
    }

    public override void Die()
    {
        Debug.Log("DIE");

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

    private void ProcessFootStepsFX(Transform guard, float velocity)
        {
            
        }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Temp")
        {
            Die();
        }
    }
}
