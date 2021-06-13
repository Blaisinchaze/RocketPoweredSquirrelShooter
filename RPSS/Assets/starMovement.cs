using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMovement : MonoBehaviour
{
    SpriteRenderer stars;
    [SerializeField] private float starSpeed;
    // Start is called before the first frame update
    void Start()
    {
        stars = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float width = ((starSpeed * Time.deltaTime) + stars.size.x);
        stars.size = new Vector2(width,stars.size.y);
    }
}
