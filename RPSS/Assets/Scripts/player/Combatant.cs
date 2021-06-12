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
   
   [HideInInspector]
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
   }

   internal void CheckToggles()
   {
      if (kill) Die();
      if (invulnerable) health = maxHealth;
   }

   /// <summary>
   /// Modify this to kill the unit. By default, destroys gameObject
   /// </summary>
   public virtual void Die()
   {
      Destroy(gameObject);
   }
}
