using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasManager>() == false)
        {
            Debug.LogError("No Main Canvas - Healthbars wil not spawn");
            return;
        }
        CanvasManager cm = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasManager>();
        GameObject healthBar = Instantiate(Resources.Load("UI/EnemyHealthBar", typeof(GameObject)),this.transform.position,Quaternion.identity, cm.TempCanvas.transform) as GameObject;
        healthBar.GetComponent<HealthBarController>().ai = GetComponent<AiUnit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
