using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
	public GameProgress Progress { get; private set; } = new GameProgress();

	public string FilePath { get; private set; }

	private void Awake()
	{
		FilePath = Path.Combine(Application.persistentDataPath, "progress.json");
	}

	private void OnEnable()
	{
		if (File.Exists(FilePath))
			Progress.Load(FilePath);
	}

	private void OnDisable()
	{
		Progress.Save(FilePath);
	}
}
