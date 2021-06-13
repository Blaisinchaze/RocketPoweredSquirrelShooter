using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : Combatant
{
    private Player player;

    private void Awake()
    {
        GameObject.FindWithTag("Player");
    }
    
    /// <summary>
    /// Override here to make custom death stuff
    /// </summary>
    public override void Die()
    {
        base.Die();
    }

    public override void GetHit(int damageValue)
    {
        base.GetHit(damageValue);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemies/Hit");

    }
}
