﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
	/// <summary>
	/// Max Health
	/// </summary>
	public float maxHp = 100;

	/// <summary>
	/// Current Health
	/// </summary>
	public float hp = 100;

	/// <summary>
	/// HP regeneration per second
	/// </summary>
	public float hpRegen = 0; // Might decide otherwise idk

	/// <summary>
	/// Image to update fill amount for UI stuff & HUD
	/// </summary>
	public Image image;

	/// <summary>
	/// Text element to update as the 
	/// </summary>
	public TextMeshProUGUI text;

	/// <summary>
	/// Format to use while updating the text element
	/// </summary>
	public string format = "{0:F0}/{1:F0}";

	private void Start()
	{
		dmgTimeTable = new Dictionary<string, float>();
		lastHp = (int)hp;
	}

	private void FixedUpdate()
	{
		hp = Mathf.Clamp(hp + hpRegen * Time.fixedDeltaTime, 0, maxHp);
	}

	private int lastHp;

	private void Update()
	{
		if (image != null)
			image.fillAmount = hp / maxHp;

		if (lastHp != (int)hp && text != null)
		{
			// FIXME: This produces so much garbage
			text.text = string.Format(format, hp, maxHp);
			lastHp = (int)hp;
		}
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
					hp = Mathf.Clamp(hp - damage, 0, maxHp);
					dmgTimeTable[key] = Time.time;
					return true;
				}
			}
			else
			{
				hp = Mathf.Clamp(hp - damage, 0, maxHp);
				dmgTimeTable.Add(key, Time.time);
				return true;
			}
		}
		else
		{
			hp = Mathf.Clamp(hp - damage, 0, maxHp);
			return true;
		}
		
		return false;
	}
}
