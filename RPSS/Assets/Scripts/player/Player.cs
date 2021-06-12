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

    private void Awake()
    {
        Components.PlayerController = GetComponent<CharacterController>();
        Components.PlayerTransform = transform;
        Components.Inputs = GetComponent<PlayerInput>();
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
                break;
            case PlayerStates.Split:
                Components.Inputs.SwitchCurrentActionMap("Gameplay Separate");
                break;
        }
    }
    
}
public struct PlayerComponents
{
    public Transform PlayerTransform;
    public CharacterController PlayerController;
    public GameObject RocketHand;
    public PlayerInput Inputs;
}
