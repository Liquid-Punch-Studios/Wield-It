using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
	private Player player;

	private float uiHealth;
	private float healthVelocity;

	public Image healthImg;
	public TextMeshProUGUI healthText;

	private float uiStamina;
	private float staminaVelocity;

	public Image staminaImg;
	//public TextMeshProUGUI staminaText;

	private void Start()
	{
		player = GameObject.Find("Player").GetComponent<Player>();

		uiHealth = player.health;
		uiStamina = player.stamina;
	}

	private void Update()
	{
		uiHealth = Mathf.SmoothDamp(uiHealth, player.health, ref healthVelocity, 0.1f, 100, Time.deltaTime);
		healthImg.fillAmount = player.health / player.maxHealth;
		healthText.text = ((int)player.health).ToString();

		uiStamina = Mathf.SmoothDamp(uiStamina, player.stamina, ref staminaVelocity, 0.1f, 100, Time.deltaTime);
		staminaImg.fillAmount = player.stamina / player.maxStamina;
		//staminaText.text = ((int)player.stamina).ToString();
	}
}
