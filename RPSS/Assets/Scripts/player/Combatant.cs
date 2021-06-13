using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class Combatant : MonoBehaviour
{
   // Components
   public Renderer myRenderer { get; private set; }
   public Transform myTransform { get; private set; }
   
   // Combat stats
   
   public bool isAlive;
   public int health;
   public int maxHealth = 5;
   
   // For testing and messing around
   
   [Header("Debug Toggles")] 
   public bool kill;
   public bool invulnerable;

   // Audio
   private string hitSound = "event:/Enemies/Hit";
   private List<EventInstance> hitSoundsInstances = new List<EventInstance>();
   private int hiterator = 0;

   protected void Awake()
   {
      myRenderer = GetComponent<Renderer>();
      myTransform = GetComponent<Transform>();
      if (health > maxHealth) maxHealth = health;

   }
   
   protected virtual void Update()
   {
      CheckToggles();
      CheckHealth();
      InitSounds();
   }

   private void InitSounds()
   {
      if (hitSoundsInstances.Count > 0) return;
      for (int i = 0; i < maxHealth; i++)
      {
         var sound = FMODUnity.RuntimeManager.CreateInstance(hitSound);
         hitSoundsInstances.Add(sound);
      }
   }

   private void PlaySound()
   {
      if (hiterator > hitSoundsInstances.Count - 1) hiterator = 0;
      hitSoundsInstances[hiterator].start();
      hiterator++;
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
      PlaySound();
  
      if (invulnerable) return;
      health -= damageValue;
   }

   public void GetHit(Bullet bullet)
   {
      PlaySound();
      if (invulnerable) return;
      health -= bullet.damageAmount;

   }

}
