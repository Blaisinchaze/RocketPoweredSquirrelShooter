using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHudManager : MonoBehaviour
{

    [SerializeField] private Slider energyBar;
    [SerializeField] private Slider energyBar2;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image energyBarColor;
    [SerializeField] private Image energyBar2Color;
    [SerializeField] private TextMeshProUGUI RoundNumber;
    [SerializeField] private TextMeshProUGUI AmmoNumber;
    private Player Player;
    private RocketFistControls rocketFist;
    private AiController roundController;
    private Color startingColor;
    public Color dangerColor;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rocketFist = GameObject.FindGameObjectWithTag("Hand").GetComponent<RocketFistControls>();
        roundController = FindObjectOfType<AiController>();
        ResetHud();
        startingColor = energyBarColor.color;
    }

    void Update()
    {
        RoundNumber.text = roundController.currentWave.ToString();
        energyBar.value = rocketFist.currentEnergyValue;
        energyBar2.value = rocketFist.currentEnergyValue;
        if (energyBar.value < energyBar.maxValue/2)
        {
            energyBarColor.color = dangerColor;
            energyBar2Color.color = dangerColor;
        }
        else
        {
            energyBarColor.color = startingColor;
            energyBar2Color.color = startingColor;
        }

        if (healthBar.value != Player.Components.playerCombat.health)
        {
            healthBar.value = Mathf.MoveTowards(healthBar.value, Player.Components.playerCombat.health, Time.deltaTime * 20);
        }
        switch (Player.currentState)
        {
            case Player.PlayerStates.Combined:            
                AmmoNumber.text = "Max";
                energyBar2.gameObject.SetActive(rocketFist.currentEnergyValue <= rocketFist.maxEnergyValue);
                break;
            case Player.PlayerStates.Split:
                energyBar2.gameObject.SetActive(rocketFist.currentEnergyValue != 0);
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
        energyBar.maxValue = rocketFist.maxEnergyValue;
        energyBar2.maxValue = rocketFist.maxEnergyValue;
        energyBar.value = rocketFist.maxEnergyValue;
        energyBar2.value = rocketFist.maxEnergyValue;
    }
}
