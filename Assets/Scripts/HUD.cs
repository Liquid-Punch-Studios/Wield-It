using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public Image healthBar;
	public Image staminaBar;

	private Health health;
	private Stamina stamina;

	private void Start()
	{
		health = GameManager.Instance.player.GetComponent<Health>();
		stamina = GameManager.Instance.player.GetComponent<Stamina>();
	}

	private void OnEnable()
	{
		health.Died += Health_Died;
		health.HpChanged += Health_HpChanged;
		stamina.SpChanged += Stamina_SpChanged;
	}

	private void OnDisable()
	{
		stamina.SpChanged -= Stamina_SpChanged;
		health.HpChanged -= Health_HpChanged;
		health.Died -= Health_Died;
	}

	private void Health_Died(object sender, System.EventArgs e)
	{
		healthBar.fillAmount = 0;
		staminaBar.fillAmount = 0;
	}

	private void Health_HpChanged(object sender, System.EventArgs e)
	{
		healthBar.fillAmount = health.Hp / health.MaxHp;
	}

	private void Stamina_SpChanged(object sender, System.EventArgs e)
	{
		staminaBar.fillAmount = stamina.Sp / stamina.MaxSp;
	}
}
