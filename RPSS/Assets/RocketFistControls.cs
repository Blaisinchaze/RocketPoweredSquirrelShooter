using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketFistControls : Combatant, IHittable
{
    public LayerMask explosionLayerMask;
    public int MaxAmountOfBullets = 100;
    public int currentAmountOfBullets;
    public float maxEnergyValue = 6f;
    internal float currentEnergyValue;
    [Space]
    public float energyRegenRate = 2f;
    public float explosionRadius = 4f;
    public int explosionDamage = 20;
    [Space]
    public float bulletSpeed = 10;
    public float bulletDamage = 1;
    [Space]
    Vector2 mousePos;
    public float moveSpeed = 0.5f;
    public float turnSpeed = 2;
    public float currentTurnSpeed = 2;
    Rigidbody2D rb;
    Vector2 forwardVector = new Vector2(0, 0);
    [Space]
    public bool firing = false;
    public GameObject ratPrefab;
    public GameObject fistExplosion;
    public GameObject jetFlame;
    public GameObject muzzleFlash;

    public GameObject BulletPrefab;
    [SerializeField]
    private GameObject bulletSpawnPoint;
    [Space]
    public float firingDelay = 0.5f;
    private float timer = 0;

    public FMODUnity.RuntimeManager firingClip;
    public Player player;

    [SerializeField] GameObject attachmentCircle;
    [SerializeField] GameObject attachmentPoint;
    private void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        currentAmountOfBullets = MaxAmountOfBullets;
        currentEnergyValue = maxEnergyValue;
    }

    private void Update()
    {
        if (!isAlive)
        {
            transform.position = player.transform.position;
            return;
        }

        switch (player.currentState)
        {
            case Player.PlayerStates.Combined:
                {
                    if (currentEnergyValue <= maxEnergyValue)
                    {
                        currentEnergyValue += energyRegenRate * Time.deltaTime; 
                    }
                }

                break;
            case Player.PlayerStates.Split:
                {
                    if (currentEnergyValue > 0)
                    {
                        currentEnergyValue -= Time.deltaTime;
                    }
                    else
                    {
                        if (isAlive)
                        {
                            Die();
                        }
                    }
                }
                break;
            default:
                break;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (timer < firingDelay) timer += Time.deltaTime;
        if (firing && (GameManager.Instance.currentState == GameStates.INGAME || GameManager.Instance.currentState == GameStates.PREGAME))
        {
            if (timer >= firingDelay)
            {
                if (currentAmountOfBullets > 0)
                {
                    Fire();
                    if(player.currentState == Player.PlayerStates.Split) currentAmountOfBullets--;
                }

                timer = 0;


            }
        }

    }

    void Fire() 
    {
        Bullet go = Instantiate(BulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity).GetComponent<Bullet>();
        go.movement = transform.right * bulletSpeed;
        go.damageAmount = (int)bulletDamage;

        FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Hand/Laser");
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        forwardVector = transform.right;
        Vector2 lookDir = Vector2.zero;
        switch (player.currentState)
        {
            case Player.PlayerStates.Combined:
                if (jetFlame.activeInHierarchy)
                {
                    jetFlame.SetActive(false);
                }
                transform.position = attachmentPoint.transform.position;
                currentTurnSpeed = turnSpeed * 10;
                lookDir = mousePos - new Vector2(attachmentCircle.transform.position.x, attachmentCircle.transform.position.y);

                break;
            case Player.PlayerStates.Split:

                if (!jetFlame.activeInHierarchy)
                {
                    jetFlame.SetActive(true);
                }
                rb.MovePosition(rb.position + forwardVector * moveSpeed * Time.deltaTime);
                currentTurnSpeed = turnSpeed;        
                lookDir = mousePos - rb.position;
                break;

            default:
                break;
        }

        Quaternion rotation = Quaternion.AngleAxis((Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg), Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, currentTurnSpeed * Time.deltaTime);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("PEW");
        if (context.started)
        {
            firing = true;
            muzzleFlash.SetActive(true);
        }
        if (context.canceled)
        {
            firing = false;
            muzzleFlash.SetActive(false);
        }
    }

    public void DetonateFist(InputAction.CallbackContext context)
    {
        if (context.started && isAlive)
        {
            Die();
        }
    }

    public override void Die()
    {
        isAlive = false;
        GameObject go = Instantiate(fistExplosion, transform.position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Body/Explosion");
        var colliders = Physics2D.OverlapCircleAll(go.transform.position, explosionRadius, explosionLayerMask);
        foreach (var itemCOllided in colliders)
        {
            var com = itemCOllided.GetComponent<Combatant>();
            if (com != null)
            {
                com.GetHit(explosionDamage);
            }
        }
        Destroy(go, 10);
        RatBastard rat = Instantiate(ratPrefab, transform.position, Quaternion.identity).GetComponent<RatBastard>();
        rat.movementSpeed = player.Components.playerMovement.moveSpeed * 0.025f;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        currentEnergyValue = 0;
        jetFlame.SetActive(false);

    }

    public void fixGun()
    {
        isAlive = true;
        currentEnergyValue = 1;
        Reload();
        transform.position = player.gameObject.transform.position;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Reload()
    {
        currentAmountOfBullets = MaxAmountOfBullets;
    }
}
