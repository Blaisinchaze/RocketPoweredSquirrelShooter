using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public enum PlayerStates
    {
        Combined,
        Split
    }

    /// <summary>
    /// Can be used to retrieve relevant components without using GetComponent
    /// </summary>
    public PlayerComponents Components;
    public PlayerStates currentState { get; private set; }
    
    [Header("Must be set")]
    public GameObject rocketHandPrefab;
    public Transform rocketHandSpawnPoint;

    private void Awake()
    {
        Components.PlayerRb = GetComponent<Rigidbody2D>();
        Components.PlayerTransform = transform;
        Components.Inputs = GetComponentInParent<PlayerInput>();
        Components.playerMovement = GetComponent<PlayerMovement>();
        Components.playerCombat = GetComponent<PlayerCombatant>();
        Components.gun = Components.playerMovement.fistControls;
        Setup();
    }

    public void Setup()
    {
        Components.playerCombat.Reset(); 
        Components.playerMovement.CheckInitialisedValues();
    }

    /// <summary>
    /// Use this if you want to set swap between player modes.
    /// </summary>
    /// <param name="newState">The new desired state</param>
    public void SetPlayerState(PlayerStates newState)
    {
        currentState = newState;
        UpdatePlayerState();
    }

    /// <summary>
    /// Swaps the player state. Only used by the Swap State Input.
    /// </summary>
    public void TogglePlayerState()
    {
        currentState = currentState == PlayerStates.Combined ? PlayerStates.Split : PlayerStates.Combined;
        UpdatePlayerState();
    }

    private void UpdatePlayerState()
    {
        switch (currentState)
        {
            case PlayerStates.Combined:
                Components.Inputs.SwitchCurrentActionMap("Gameplay Combined");
                Components.RocketHand.GetComponent<RocketFistControls>().Reload();
                break;
            case PlayerStates.Split:
                Components.Inputs.SwitchCurrentActionMap("Gameplay Separate");
                break;
        }
    }
    
}
[Serializable]
public struct PlayerComponents
{
    public Transform PlayerTransform;
    public Rigidbody2D PlayerRb;
    public GameObject RocketHand;
    public RocketFistControls gun;
    public PlayerInput Inputs;
    public PlayerCombatant playerCombat;
    public PlayerMovement playerMovement;
}
