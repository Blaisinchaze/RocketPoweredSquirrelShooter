using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StraightToGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene() 
    {
        if (GameManager.Instance != null) Destroy(GameManager.Instance.gameObject);
        if (AiController.Instance != null) Destroy(AiController.Instance.gameObject);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("GameScene");
    }
}
