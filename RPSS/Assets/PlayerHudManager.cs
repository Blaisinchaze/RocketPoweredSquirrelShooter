using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHudManager : MonoBehaviour
{

    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI RoundNumber;
    [SerializeField] private TextMeshProUGUI AmmoNumber;
    private Player Player;
    private RocketFistControls rocketFist;
    private AiController roundController;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rocketFist = GameObject.FindGameObjectWithTag("Hand").GetComponent<RocketFistControls>();
        roundController = FindObjectOfType<AiController>();
        ResetHud();
    }

    void Update()
    {
        RoundNumber.text = roundController.currentWave.ToString();
        if(healthBar.value != Player.Components.playerCombat.health)
        {
            healthBar.value = Mathf.MoveTowards(healthBar.value, Player.Components.playerCombat.health, Time.deltaTime * 20);
        }
        switch (Player.currentState)
        {
            case Player.PlayerStates.Combined:            
                AmmoNumber.text = "Max";
                break;
            case Player.PlayerStates.Split:
                AmmoNumber.text = (rocketFist.currentAmountOfBullets.ToString() + " / " + (rocketFist.MaxAmountOfBullets.ToString()));
                break;
            default:
                break;
        }
    }

    public void ResetHud()
    {
        healthBar.maxValue = Player.Components.playerCombat.maxHealth;
        healthBar.value = healthBar.maxValue;
    }
}
