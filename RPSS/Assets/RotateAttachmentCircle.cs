using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RotateAttachmentCircle : MonoBehaviour
{
    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        //transform.rotation = Quaternion.Euler(-70, transform.eulerAngles.x, transform.eulerAngles.y);
        transform.eulerAngles = new Vector3(
    transform.eulerAngles.x - 55,
    transform.eulerAngles.y,
    transform.eulerAngles.z
);
    }
}
