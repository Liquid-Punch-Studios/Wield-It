using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
	/// <summary>
	/// Max Health
	/// </summary>
	[SerializeField]
	private float maxHp = 100;
	public float MaxHp
	{
		get { return maxHp; }
		set
		{
			var old = maxHp;
			if (old != value)
			{
				maxHp = value;
				MaxHpChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MaxHpChanged;

	/// <summary>
	/// Current Health
	/// </summary>
	[SerializeField]
	private float hp = 100;
	public float Hp
	{
		get { return hp; }
		set
		{
			var old = hp;
			if (old != value)
			{
				hp = value;
				HpChanged?.Invoke(this, EventArgs.Empty);
				if ((int)old != (int)value)
					HpIntChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler HpChanged;
	public event EventHandler HpIntChanged;

	/// <summary>
	/// Format to use while updating the text element
	/// </summary>
	//public string format = "{0:F0}/{1:F0}";

	private void Start()
	{
		dmgTimeTable = new Dictionary<string, float>();
	}

	private Dictionary<string, float> dmgTimeTable;

	/// <summary>
	/// Systematically receive damage to decrease hp and return
	/// whether the damage is successfully dealt.
	/// </summary>
	/// <param name="damage">Amount of damage to receive</param>
	public bool ReceiveDamage(float damage, string key = null, float cooldown = 0)
	{
		if (key != null)
		{
			if (dmgTimeTable.TryGetValue(key, out float lastDmgTime))
			{
				if (Time.time - lastDmgTime > cooldown)
				{
					Hp = Mathf.Clamp(Hp - damage, 0, maxHp);
					dmgTimeTable[key] = Time.time;
					return true;
				}
			}
			else
			{
				Hp = Mathf.Clamp(Hp - damage, 0, maxHp);
				dmgTimeTable.Add(key, Time.time);
				return true;
			}
		}
		else
		{
			Hp = Mathf.Clamp(Hp - damage, 0, maxHp);
			return true;
		}
		
		return false;
	}
}
