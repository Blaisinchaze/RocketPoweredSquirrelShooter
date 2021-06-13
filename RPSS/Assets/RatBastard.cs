using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBastard : MonoBehaviour
{

    public float movementSpeed = 2f;
    GameObject mainBody;
    void Start()
    {
        mainBody = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, mainBody.transform.position) < 0.3f)
        {
            mainBody.GetComponent<PlayerMovement>().ReloadGun();
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, mainBody.transform.position, movementSpeed * Time.deltaTime);
    }
}
