using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
	public GameProgress Progress { get; private set; } = new GameProgress();

	private void OnEnable()
	{
		Progress.Load();
	}

	private void OnDisable()
	{
		Progress.Save();
	}
}
