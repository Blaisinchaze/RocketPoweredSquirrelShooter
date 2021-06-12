using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatant : Combatant
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    protected override void Die()
    {
        base.Die();
    }
}
