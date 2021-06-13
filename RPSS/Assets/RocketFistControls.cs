using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketFistControls : Combatant, IHittable
{
    public int MaxAmountOfBullets = 100;
    private int currentAmountOfBullets;
    Vector2 mousePos;
    public float moveSpeed = 0.5f;
    public float turnSpeed = 2;
    Rigidbody2D rb;
    Vector2 forwardVector = new Vector2(0, 0);
    public bool firing = false;
    public GameObject ratPrefab;

    public GameObject BulletPrefab;
    [SerializeField]
    private GameObject bulletSpawnPoint;
    private float firingDelay = 0.1f;
    private float timer = 0;

    public Player player;
    private void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        currentAmountOfBullets = MaxAmountOfBullets;
    }

    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        forwardVector = transform.right;
        timer += Time.deltaTime;
        Mathf.Clamp(timer, 0, 2);
        if (firing)
        {
            if (timer >= firingDelay)
            {
                Quaternion firingAngle = transform.rotation * Quaternion.Euler(0, 0, 90);
                switch (player.currentState)
                {
                    case Player.PlayerStates.Combined:
                        {
                            GameObject go = Instantiate(BulletPrefab, bulletSpawnPoint.transform.position, firingAngle);
                            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                            rb.AddForce(bulletSpawnPoint.transform.right * 20f, ForceMode2D.Impulse);
                            currentAmountOfBullets--;
                        }

                        break;

                    case Player.PlayerStates.Split:
                        {
                            if (currentAmountOfBullets > 0)
                            {
                                GameObject go = Instantiate(BulletPrefab, bulletSpawnPoint.transform.position, firingAngle);
                                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                                rb.AddForce(bulletSpawnPoint.transform.right * 20f, ForceMode2D.Impulse);
                                currentAmountOfBullets--;
                            }
                        }
                        break;
                    default:
                        break;
                }
                timer = 0;


            }
        }

    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        switch (player.currentState)
        {
            case Player.PlayerStates.Combined:
                break;
            case Player.PlayerStates.Split:
                if (isAlive)
                {
                    rb.MovePosition(rb.position + forwardVector * moveSpeed * Time.deltaTime);

                    Vector2 lookDir = mousePos - rb.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
                }
                break;
            default:
                break;
        }

    }

    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("PEW");
        if (context.started)
        {
            firing = true;
        }
        if (context.canceled)
        {
            firing = false;
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
        //Instantiate explosion
        Instantiate(ratPrefab, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        isAlive = false;
    }

    public void fixGun()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        isAlive = true;
        Reload();
    }

    public void Reload()
    {
        currentAmountOfBullets = MaxAmountOfBullets;
    }
}
