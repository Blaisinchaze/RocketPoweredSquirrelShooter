using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade
{
    SPEED,
    HEAL,
    DAMAGE,
    FIRERATE,
}

public class Pickup : MonoBehaviour
{
    internal HealSpawner manager;
    public Upgrade type;
    public float[] values;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AiController.Instance.player.gameObject == collision.gameObject)
        {
            manager.pickups.Remove(gameObject);

            switch (type)
            {
                case Upgrade.SPEED:
                    AiController.Instance.player.Components.playerMovement.moveSpeed += values[0];
                    break;

                case Upgrade.HEAL:
                    AiController.Instance.player.Components.playerCombat.health += (int)values[0];
                    break;

                case Upgrade.DAMAGE:
                    AiController.Instance.player.Components.gun.bulletDamage += values[0];
                    AiController.Instance.player.Components.gun.bulletSpeed += values[1];
                    break;

                case Upgrade.FIRERATE:
                    AiController.Instance.player.Components.gun.firingDelay -= values[0];
                    break;

                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
