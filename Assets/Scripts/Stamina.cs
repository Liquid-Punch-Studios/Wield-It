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
	public float maxSp = 100;

	/// <summary>
	/// Current Stamina
	/// </summary>
	public float sp = 100;

	/// <summary>
	/// Stamina regeneration per second
	/// </summary>
	public float spRegen = 50f;

	/// <summary>
	/// Stamina regeneration per second while not on ground
	/// </summary>
	public float spRegenAir = 25f;

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

	private Movement movement;

	private void Start()
	{
		movement = GetComponent<Movement>();
		lastSp = (int)sp;
	}

	private void FixedUpdate()
	{
		sp = Mathf.Clamp(sp + (movement.OnGround ? spRegen : spRegenAir) * Time.fixedDeltaTime, 0, maxSp);
	}

	private int lastSp;

	private void Update()
	{
		if (image != null)
			image.fillAmount = sp / maxSp;

		if (lastSp != (int)sp && text != null)
		{
			// FIXME: This produces so much garbage
			text.text = string.Format(format, sp, maxSp);
			lastSp = (int)sp;
		}
	}

	/// <summary>
	/// Systematically try using stamina and return whether
	/// it was successful (there is enough stamina)
	/// </summary>
	/// <param name="amount">Amount stamina to use</param>
	public bool UseStamina(float amount)
	{
		if (amount > sp)
			return false;

		sp = Mathf.Clamp(sp - amount, 0, maxSp);
		return true;
	}
}
