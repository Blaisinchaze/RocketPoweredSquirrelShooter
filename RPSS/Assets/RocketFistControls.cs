using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketFistControls : Combatant, IHittable
{
    public int MaxAmountOfBullets = 100;
    public int currentAmountOfBullets;
    Vector2 mousePos;
    public float moveSpeed = 0.5f;
    public float turnSpeed = 2;
    public float currentTurnSpeed = 2;
    Rigidbody2D rb;
    Vector2 forwardVector = new Vector2(0, 0);
    public bool firing = false;
    public GameObject ratPrefab;
    public GameObject fistExplosion;

    public GameObject BulletPrefab;
    [SerializeField]
    private GameObject bulletSpawnPoint;
    private float firingDelay = 0.1f;
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
        if (firing && GameManager.instance.currentState == GameStates.INGAME)
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
                            FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Hand/Laser");
                        }

                        break;

                    case Player.PlayerStates.Split:
                        {
                            if (currentAmountOfBullets > 0)
                            {
                                GameObject go = Instantiate(BulletPrefab, bulletSpawnPoint.transform.position, firingAngle);
                                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                                rb.AddForce(bulletSpawnPoint.transform.right * 20f, ForceMode2D.Impulse);
                                FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerRobot/Hand/Laser");
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
        Vector2 lookDir = Vector2.zero;
        switch (player.currentState)
        {
            case Player.PlayerStates.Combined:
                transform.position = attachmentPoint.transform.position;
                currentTurnSpeed = turnSpeed * 10;
                lookDir = mousePos - new Vector2(attachmentCircle.transform.position.x, attachmentCircle.transform.position.y);

                break;
            case Player.PlayerStates.Split:
                rb.MovePosition(rb.position + forwardVector * moveSpeed * Time.deltaTime);
                currentTurnSpeed = turnSpeed;        
                lookDir = mousePos - rb.position;
                break;
            default:
                break;
        }

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, currentTurnSpeed * Time.deltaTime);
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
        GameObject go = Instantiate(fistExplosion, transform.position, Quaternion.identity);
        Destroy(go, 10);
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
