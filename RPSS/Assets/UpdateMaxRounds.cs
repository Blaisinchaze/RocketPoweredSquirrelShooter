using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateMaxRounds : MonoBehaviour
{
    public TextMeshProUGUI roundNumber;
    public AiController aiController;

    void Update()
    {
        roundNumber.text = aiController.currentWave.ToString();
    }
}
