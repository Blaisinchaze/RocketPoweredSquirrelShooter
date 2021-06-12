using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFistControls : MonoBehaviour
{
    Vector2 mousePos;
    public float moveSpeed = 0.5f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0, 0);
    Vector2 forwardVector = new Vector2(0, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        forwardVector = transform.right;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + forwardVector * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
