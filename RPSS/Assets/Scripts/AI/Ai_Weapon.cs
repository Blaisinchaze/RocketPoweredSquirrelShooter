using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_Weapon : MonoBehaviour
{
    public Enemies type;
    public float projRange;
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

            case Enemies.WALK:
            case Enemies.SHIELD:
                player = GameObject.FindGameObjectWithTag("Player").transform;
                break;

            case Enemies.GUN:
                //need to find the hand object
                player = GameObject.FindGameObjectWithTag("Player").transform;
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
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        GameObject projectile = Instantiate(spawnPrefab, relativeSpawnPoint.position, Quaternion.Euler(0,0,0), body.parent);

        firingDirection = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(firingDirection.y, firingDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, targetAngle + 270);


        Vector2 playerDirection = new Vector2(projectile.transform.position.x - player.transform.position.x, projectile.transform.position.y - player.transform.position.y).normalized;

        switch (type)
        {
            case Enemies.NULL:
                Debug.Log("SET WEAPON TYPE ON ENEMY");
                break;

            case Enemies.WALK:
                {
                    float distance = 0;
                    while (distance < projRange)
                    {
                        if (projectile == null)
                        {
                            yield break;
                        }
                        projectile.transform.Translate(Vector3.right * projSpeed * -1);
                        distance = Vector3.Distance(projectile.transform.position, relativeSpawnPoint.position);
                        yield return new WaitForSeconds(0.01f);
                    }
                    Destroy(projectile);
                }
                break;

            case Enemies.SHIELD:
                {
                    float distance = 0;
                    while (distance < projRange)
                    {
                        if (projectile == null)
                        {
                            yield break;
                        }
                        projectile.transform.Translate(Vector3.up * projSpeed);
                        distance = Vector3.Distance(projectile.transform.position, relativeSpawnPoint.position);
                        yield return new WaitForSeconds(0.01f);
                    }
                    while (distance > 0)
                    {
                        if (projectile == null)
                        {
                            yield break;
                        }
                        projectile.transform.Translate(Vector3.down * projSpeed);
                        distance -= projSpeed;
                        yield return new WaitForSeconds(0.05f);
                    }

                    Destroy(projectile);
                }
                break;

            case Enemies.GUN:
                {
                    projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projSpeed, ForceMode2D.Impulse);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Hand/Laser");
                }
                break;
        }

        yield break;
    }
}
