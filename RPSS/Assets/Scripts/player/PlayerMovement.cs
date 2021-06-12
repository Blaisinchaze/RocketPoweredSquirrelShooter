using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The master player class that handles states
    /// </summary>
    private Player player;
    /// <summary>
    /// Direction the player moves tracked via input
    /// </summary>
    private Vector2 movementDirection;

    /// <summary>
    /// The maximum distance the player and hand can be apart and still combine
    /// </summary>
    [SerializeField] private float combineDistance = 10f;
    
    [Header("Customisable")] 
    public float moveSpeed = 10f;
    
    private void CheckInitialisedValues()
    {
        if (moveSpeed.Equals(0))
        {
            moveSpeed = 10f;
            Debug.LogWarning("Player moveSpeed not assigned! Resetting to default value...");
        }

        if (combineDistance.Equals(0))
        {
            combineDistance = 10f;
            Debug.LogWarning("Player combineDistance not assigned! Resetting to default value...");
        }
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        CheckInitialisedValues();
    }

    private void Update()
    {
        Movement();
    }


    /// <summary>
    /// Handles player movement based on the input vector2.
    /// </summary>
    private void Movement()
    {
        if (movementDirection == Vector2.zero) return;
        
        var controller = player.Components.PlayerController;
        var adjustedSpeed = moveSpeed * Time.deltaTime;
        controller.Move(movementDirection * adjustedSpeed);
        //transform.position += new Vector3(movementDirection.x/moveSpeed,movementDirection.y/moveSpeed,0);
    }
    
    /// <summary>
    /// If the player is combined, swap states. Otherwise, make sure they are close enough before they combine.
    /// </summary>
    private bool TryTogglePlayerState()
    {
        if (player.currentState == Player.PlayerStates.Combined)
        {
            player.TogglePlayerState();
            return true;
        }

        var check = true; //Vector3.Distance(transform.position, player.Components.RocketHand.transform.position) < combineDistance;
        if (check)
        {
            player.TogglePlayerState();
            return true;
        }

        return false;
    }
    
    #region InputEvents
    
    public void Move(InputAction.CallbackContext movement)
    {
        movementDirection = movement.ReadValue<Vector2>();
    }

    public void Separate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        TryTogglePlayerState();
    }
    
    #endregion

}
