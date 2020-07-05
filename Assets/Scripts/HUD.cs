using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Image healthImg;
    Image staminaImg;

    TextMeshProUGUI healthText;

    public float maxHealth = 100;
    public float targetHealth;
    private float health;
    private float healthVelocity;
    
    public float maxStamina = 100;
    public float targetStamina;
    private float stamina;
    private float staminaVelocity;

    private void Start()
    {
        healthImg = GameObject.Find("Health Bar").GetComponent<Image>();
        staminaImg = GameObject.Find("Stamina Bar").GetComponent<Image>();

        healthText = GameObject.Find("Health Text").GetComponent<TextMeshProUGUI>();

        health = maxHealth;
        stamina = maxStamina;

        healthImg.fillAmount = 1;
        staminaImg.fillAmount = 1;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void Update()
    {
        Mathf.SmoothDamp(health, targetHealth, ref healthVelocity, 0.1f , 1);
        health = (int) health  == (int) targetHealth ? targetHealth: health + healthVelocity;
        healthImg.fillAmount = health / maxHealth;
        healthText.text = ((int)health).ToString() + "/" + ((int)maxHealth).ToString();
        
        Mathf.SmoothDamp(stamina, targetStamina, ref staminaVelocity, 0.1f, 1);
        stamina = (int)stamina == (int)targetStamina ? targetStamina : stamina + staminaVelocity;
        staminaImg.fillAmount = stamina / maxStamina;
    }

}
