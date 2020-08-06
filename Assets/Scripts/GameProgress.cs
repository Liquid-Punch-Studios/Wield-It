using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class GameProgress
{
	private bool saved = false;
	public bool Saved
	{
		get { return saved; }
		private set
		{
			var old = saved;
			if (old != value)
			{
				saved = value;
				SavedChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	private Dictionary<int, Difficulty> completedLevels = new Dictionary<int, Difficulty>();
	public Dictionary<int, Difficulty> CompletedLevels { get => completedLevels; set => completedLevels = value; }

	public event EventHandler SavedChanged;

	public void CompleteLevel(int level, Difficulty difficulty)
	{
		if (completedLevels == null)
			completedLevels = new Dictionary<int, Difficulty>();
		if (!completedLevels.TryGetValue(level, out Difficulty diff) || diff < difficulty)
			completedLevels[level] = difficulty;
	}

	public bool IsLevelCompleted(int level)
	{
		return completedLevels.ContainsKey(level);
	}

	public bool IsLevelCompleted(int level, out Difficulty difficulty)
	{
		return completedLevels.TryGetValue(level, out difficulty);
	}

	public static readonly string filePath = Path.Combine(Application.persistentDataPath, "progress.json");

	public void Save()
	{
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			sw.Write(JsonUtility.ToJson(this, true));
		}
		Saved = true;
	}

	public void Load()
	{
		try
		{
			using (StreamReader sr = new StreamReader(filePath))
			{
				JsonUtility.FromJsonOverwrite(sr.ReadToEnd(), this);
			}
			Saved = true;
		}
		catch (Exception)
		{
			Saved = false;
			throw;
		}
	}
}
