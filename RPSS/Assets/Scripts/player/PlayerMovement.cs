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
    /// Controls the current sprite state of main body
    /// </summary>
    [SerializeField]
    private Animator bodyAnimator;

    [SerializeField]
    private RocketFistControls fistControls;

    /// <summary>
    /// The maximum distance the player and hand can be apart and still combine
    /// </summary>
    [SerializeField] private float combineDistance = 5f;
    
    [Header("Customisable")] 
    public float moveSpeed;
    
    [SerializeField] float tiltAmount = 0;

    public void CheckInitialisedValues()
    {
        if (moveSpeed.Equals(0))
        {
            moveSpeed = 40f;
            Debug.LogWarning("Player moveSpeed not assigned! Resetting to default value...");
        }
        if (combineDistance.Equals(0))
        {
            combineDistance = 5f;
            Debug.LogWarning("Player combineDistance not assigned! Resetting to default value...");
        }

        moveSpeed *= 10f;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    
    private void FixedUpdate()
    {
        if (GameManager.instance.currentState == GameStates.INGAME)
            Movement();
    }

    private void HandleAnimation()
    {
        TiltBot(movementDirection);
        if (movementDirection == Vector2.zero)
        {
            return;
        }
        bodyAnimator.SetFloat("Horizontal", movementDirection.x);
        bodyAnimator.SetFloat("Vertical", movementDirection.y);
        bodyAnimator.SetFloat("Speed", movementDirection.sqrMagnitude);
    }

    /// <summary>
    /// Handles player movement based on the input vector2.
    /// </summary>
    private void Movement()
    {
        HandleAnimation();
        SnapMovementToNeutral();
        if (movementDirection == Vector2.zero) return;
        var rb  = player.Components.PlayerRb;
        var adjustedSpeed = moveSpeed * Time.deltaTime;
        rb.velocity = movementDirection * adjustedSpeed;
    }

    /// <summary>
    /// Resets velocity to neutral when opposing inputs are pressed to prevent sliding
    /// </summary>
    private void SnapMovementToNeutral()
    {
        var rb = player.Components.PlayerRb;
        if (rb.velocity.x < 0 && movementDirection.x > 0) 
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (rb.velocity.y < 0 && movementDirection.y > 0) 
            rb.velocity = new Vector2(rb.velocity.x, 0);
        if (!rb.velocity.x.Equals(0) && movementDirection.x.Equals(0)) 
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (!rb.velocity.y.Equals(0) && movementDirection.y.Equals(0)) 
            rb.velocity = new Vector2(rb.velocity.x, 0);
        

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

        var check = Vector3.Distance(transform.position, player.Components.RocketHand.transform.position) < combineDistance;
        if (check)
        {
            player.TogglePlayerState();
            return true;
        }

        return false;
    }

    public void ReloadGun()
    {
        player.Components.RocketHand.GetComponent<RocketFistControls>().fixGun();
        player.SetPlayerState(Player.PlayerStates.Combined);
    }
    
    public void TiltBot(Vector2 tilt)
    {
        tiltAmount = 0;
        if(tilt.x > 0)
        {            
            tiltAmount = -15;
            if (tilt.y != 0)
                tiltAmount = -5;

        }
        if (tilt.x < 0)
        {
            tiltAmount = 15;
            if (tilt.y != 0)
                tiltAmount = 5;
        }
        Vector3 desiredTilt = new Vector3(0,0,tiltAmount);
        Quaternion q = Quaternion.Euler(desiredTilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 5);
    }
    #region InputEvents
    
    public void Move(InputAction.CallbackContext movement)
    {
        movementDirection = movement.ReadValue<Vector2>();
    }

    public void Separate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (player.currentState == Player.PlayerStates.Combined)
        {
            if (fistControls.currentEnergyValue < fistControls.maxEnergyValue / 2)
            {
                return;
            }
        }
        TryTogglePlayerState();
    }
    
    #endregion

}
