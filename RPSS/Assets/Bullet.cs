using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount;
    private Player player;
    public bool enemyBullet;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8,8);

        player = FindObjectOfType<Player>();

        Destroy(gameObject, 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemy bullets don't hit enemies
        // friendly no hitty friend
        if ((enemyBullet && collision.gameObject.layer == 7) || (!enemyBullet && collision.gameObject.layer == 3) || (collision.tag == "Hand" && player.currentState != Player.PlayerStates.Split))
        {
            return;
        }

        var hit = collision.GetComponent<Combatant>();
        if (hit != null)
        {
            hit.GetHit(damageAmount);
        }


        if (enemyBullet)
        {
            Destroy(GetComponent<Collider>());
            Destroy(gameObject, 0.05f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
