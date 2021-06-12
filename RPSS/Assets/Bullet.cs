using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.GetComponent<IHittable>();
        if (hit != null)
        {
            hit.GetHit(damageAmount);
        }
        Destroy(this.gameObject);
    }
}
