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
	public float maxHp = 100;

	/// <summary>
	/// Current Health
	/// </summary>
	private float hp = 100;
	public float Hp
	{
		get
		{
			return hp;
		}
		set
		{
			hp = value;

			if (image != null)
				image.fillAmount = Hp / maxHp;

			if (lastHp != (int)Hp && text != null)
			{
				// FIXME: This produces so much garbage
				text.text = string.Format(format, Hp, maxHp);
				lastHp = (int)Hp;
			}
		}
	}

	private int lastHp;

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
		lastHp = (int)Hp;
	}

	private void FixedUpdate()
	{
		Hp = Mathf.Clamp(Hp + hpRegen * Time.fixedDeltaTime, 0, maxHp);
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
