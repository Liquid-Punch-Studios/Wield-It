using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
	public Transform spawnpoint;
	public Transform[] checkpoints;

	private void Start()
	{
		if (spawnpoint == null)
			Debug.LogError("No spawn point assigned.");
		if (checkpoints?.Length == 0)
			Debug.LogWarning("No check points assigned in the level.");
	}
}
