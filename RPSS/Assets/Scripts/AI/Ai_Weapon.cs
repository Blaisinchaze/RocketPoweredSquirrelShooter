using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Weapon : MonoBehaviour
{
    public Enemies type;
    public float projLifetime;
    public float projSpeed;
    public float turnSpeed;
    public float weaponHeldDistanceModifier;
    [Space]
    public Transform relativeSpawnPoint;
    public GameObject spawnPrefab;

    private Transform body;
    private Transform player;
    [SerializeField]
    private Vector3 firingDirection;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        body = transform.parent;
        anim = GetComponentInChildren<Animator>();
        switch (type)
        {
            case Enemies.NULL:
                Debug.Log("SET WEAPON TYPE ON ENEMY");
                break;

            default:
                player = AiController.Instance.player.transform;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        firingDirection = player.transform.position - transform.position;
        Vector3 desiredPos = body.position + (firingDirection.normalized * weaponHeldDistanceModifier);
        transform.position = Vector3.Lerp(transform.position, desiredPos, 0.1f);

        float targetAngle = Mathf.Atan2(firingDirection.y, firingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle + 270), turnSpeed);

        //blend state update for rotating sprite
        switch (type)
        {
            case Enemies.NULL:
            case Enemies.WALK:
            case Enemies.GUN:
                break;

            case Enemies.SHIELD:
                if ((Vector2)firingDirection == Vector2.zero)
                {
                    return;
                }

                anim.SetFloat("Horizontal", firingDirection.x);
                anim.SetFloat("Vertical", firingDirection.y);
                anim.SetFloat("Speed", firingDirection.sqrMagnitude);
                break;
        }
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(spawnPrefab, relativeSpawnPoint.position, Quaternion.Euler(0, 0, 0), body.parent);

        Bullet proj = projectile.GetComponent<Bullet>();
        Vector2 playerDirection = new Vector2(
            projectile.transform.position.x - player.transform.position.x,
            projectile.transform.position.y - player.transform.position.y).normalized;

        if (type == Enemies.GUN)
        {
            playerDirection = transform.up;
        }

        proj.movement = playerDirection * projSpeed;
        proj.maxTime = projLifetime;

        //Debug.Log(proj.movement + " " + proj.maxTime);
    }
}
