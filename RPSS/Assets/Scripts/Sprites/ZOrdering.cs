using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ZOrdering : MonoBehaviour
{
    [SerializeField] bool constantlyUpdate;
    [SerializeField] int offset;
    [SerializeField] List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in sprites)
        {
            item.sortingOrder = ((int)item.gameObject.transform.position.y * -10) + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(constantlyUpdate)
        {
            foreach (var item in sprites)
            {
                item.sortingOrder = (int)(item.gameObject.transform.position.y * -10) + offset;
            }
        }

    }
}
