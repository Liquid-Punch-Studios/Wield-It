using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stamina : MonoBehaviour
{
	/// <summary>
	/// Max Stamina
	/// </summary>
	[SerializeField]
	private float maxSp = 100;
	public float MaxSp
	{
		get { return maxSp; }
		set
		{
			var old = maxSp;
			if (old != value)
			{
				maxSp = value;
				MaxSpChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MaxSpChanged;

	/// <summary>
	/// Current Stamina
	/// </summary>
	[SerializeField]
	private float sp = 100;
	public float Sp
	{
		get { return sp; }
		set
		{
			var old = sp;
			if (old != value)
			{
				sp = value;
				SpChanged?.Invoke(this, EventArgs.Empty);
				if ((int)old != (int)value)
					SpIntChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler SpChanged;
	public event EventHandler SpIntChanged;

	/// <summary>
	/// Stamina regeneration per second
	/// </summary>
	[SerializeField]
	private float spRegen = 50f;
	public float SpRegen
	{
		get { return spRegen; }
		set
		{
			var old = spRegen;
			if (old != value)
			{
				spRegen = value;
				SpRegenChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler SpRegenChanged;

	/// <summary>
	/// Stamina regeneration per second while not on ground
	/// </summary>
	[SerializeField]
	private float spRegenAir = 25f;
	public float SpRegenAir
	{
		get { return spRegenAir; }
		set
		{
			var old = spRegenAir;
			if (old != value)
			{
				spRegenAir = value;
				SpRegenAirChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler SpRegenAirChanged;

	/// <summary>
	/// Format to use while updating the text element
	/// </summary>
	//public string format = "{0:F0}/{1:F0}";

	private Movement movement;

	private void Start()
	{
		movement = GetComponent<Movement>();
	}

	private void FixedUpdate()
	{
		if (movement != null)
			Sp = Mathf.Clamp(sp + (movement.OnGround ? spRegen : spRegenAir) * Time.fixedDeltaTime, 0, maxSp);
		else
			Sp = Mathf.Clamp(sp + spRegen * Time.fixedDeltaTime, 0, maxSp);
	}

	/// <summary>
	/// Systematically try using stamina and return whether
	/// it was successful (there is enough stamina)
	/// </summary>
	/// <param name="amount">Amount of stamina to use</param>
	public bool UseStamina(float amount)
	{
		if (amount > sp)
			return false;

		Sp = Mathf.Clamp(sp - amount, 0, maxSp);
		return true;
	}
}
