using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
   // Components
   public Renderer myRenderer { get; private set; }
   public Transform myTransform { get; private set; }
   
   // Combat stats
   
   public bool isAlive;
   public int health;
   public int maxHealth;
   
   // For testing and messing around
   
   [Header("Debug Toggles")] 
   public bool kill;
   public bool invulnerable;


   private void Awake()
   {
      myRenderer = GetComponent<Renderer>();
      myTransform = GetComponent<Transform>();
   }

   private void Update()
   {
      CheckToggles();
      CheckHealth();
   }

   internal void CheckToggles()
   {
      if (kill) Die();
      if (invulnerable) health = maxHealth;
   }

   internal void CheckHealth()
   {
      if (health <= 0 && !invulnerable)
         Die();
   }

   /// <summary>
   /// Modify this to kill the unit. By default, destroys gameObject
   /// </summary>
   public virtual void Die()
   {
      Destroy(gameObject);
   }
   
   /// <summary>
   /// What happens when I'm hit by a projectile or melee
   /// </summary>
   public virtual void GetHit(int damageValue)
   {
      if (invulnerable) return;
      health -= damageValue;
   }

   public void GetHit(Bullet bullet)
   {
      if (invulnerable) return;
      health -= bullet.damageAmount;
   }

}
