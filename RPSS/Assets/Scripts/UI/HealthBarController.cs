using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public AiUnit ai;
    private Slider bar;

    float maxHealth;

    [SerializeField] Color standardHealthColour;
    [SerializeField] Color hitHealthColour;

    [SerializeField] Image fillArea;

    private RectTransform rt;
    private Camera mCamera;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        mCamera = Camera.main;
        bar = GetComponentInChildren<Slider>();

        maxHealth = ai.maxHealth;
        bar.maxValue = maxHealth;
        bar.value = maxHealth;
        fillArea.color = standardHealthColour;
        bar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ai.health < maxHealth)
        {
            bar.gameObject.SetActive(true);
        }
        else
        {
            return;
        }
        if(ai == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            rt.position = ai.transform.position;
            if (bar.value != ai.health)
            {
                fillArea.color = hitHealthColour;
                bar.value = Mathf.MoveTowards(bar.value, ai.health, Time.deltaTime * 5);
            }
            else
            {
                fillArea.color = standardHealthColour;
            }
        }

    }

}
