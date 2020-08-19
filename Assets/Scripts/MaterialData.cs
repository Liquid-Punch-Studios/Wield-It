using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Material
{
	Wood,
	Stone,
	Metal,
	Flesh,
}

public sealed class MaterialData : MonoBehaviour
{
	public Material material;

	public bool CanBeStabbed
	{
		get => material == Material.Flesh || material == Material.Wood;
	}
}
