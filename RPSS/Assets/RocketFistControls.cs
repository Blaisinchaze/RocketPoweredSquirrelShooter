using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketFistControls : MonoBehaviour, IHittable
{
    Vector2 mousePos;
    public float moveSpeed = 0.5f;
    public float turnSpeed = 2;
    Rigidbody2D rb;
    Vector2 forwardVector = new Vector2(0, 0);
    public bool firing = false;

    public GameObject BulletPrefab;
    [SerializeField]
    private GameObject bulletSpawnPoint;
    private float firingDelay = 0.1f;
    private float timer = 0;

    public Player player;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        forwardVector = transform.right;
        timer += Time.deltaTime;
        Mathf.Clamp(timer, 0, 2);
        if (firing)
        {
            if (timer >= firingDelay)
            {

                Quaternion firingAngle = transform.rotation * Quaternion.Euler(0,0,90);
                GameObject go = Instantiate(BulletPrefab, bulletSpawnPoint.transform.position, firingAngle);
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletSpawnPoint.transform.right * 20f, ForceMode2D.Impulse);
                timer = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + forwardVector * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        //rb.rotation = Mathf.MoveTowards(rb.rotation,angle, 1f);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            firing = true;
        }
        if (context.canceled)
        {
            firing = false;
        }
    }

    public void GetHit(int damageAmount)
    {
        Debug.Log("plAyer hit");
    }


}
