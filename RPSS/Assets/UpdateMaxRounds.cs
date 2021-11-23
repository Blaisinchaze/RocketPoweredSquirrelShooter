using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateMaxRounds : MonoBehaviour
{
    public AiController aiController;

    public TextMeshProUGUI roundNumber;
    void UpdateRoundNum()
    {
        roundNumber.text = aiController.currentWave.ToString();
        Debug.Log(roundNumber.text + " " + aiController.currentWave);
    }
}
