using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount;
    public Vector2 movement;
    public float maxTime;
    private Player player;
    public bool enemyBullet;

    private float lifetime;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8,8);

        if(enemyBullet) player = AiController.Instance.player.GetComponent<Player>();

        Destroy(gameObject, 15);
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime >= maxTime)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(movement * Time.deltaTime);
        }
        
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
