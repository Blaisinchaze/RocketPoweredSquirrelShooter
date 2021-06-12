using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovmenet : MonoBehaviour
{
    Vector2 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementDirection.x/10,movementDirection.y/10,0);
    }

    public void Move(InputAction.CallbackContext movement)
    {
        movementDirection = movement.ReadValue<Vector2>();
    }
}
