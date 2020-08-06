using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettings
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
	public event EventHandler SavedChanged;

	#region Gameplay
	private Difficulty difficulty = Difficulty.Normal;
	public Difficulty Difficulty
	{
		get { return difficulty; }
		set
		{
			var old = difficulty;
			if (old != value)
			{
				difficulty = value;
				Saved = false;
				DifficultyChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler DifficultyChanged;
	#endregion

	#region Graphics
	private Resolution resolution;
	public Resolution Resolution
	{
		get { return resolution; }
		set
		{
			var old = resolution;
			if (old.width != value.width ||
				old.height != value.height ||
				old.refreshRate != value.refreshRate)
			{
				resolution = value;
				Saved = false;
				ResolutionChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler ResolutionChanged;

	private FullScreenMode displayMode;
	public FullScreenMode DisplayMode
	{
		get { return displayMode; }
		set
		{
			var old = displayMode;
			if (old != value)
			{
				displayMode = value;
				Saved = false;
				DisplayModeChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler DisplayModeChanged;

	private int vsync;
	public int Vsync
	{
		get { return vsync; }
		set
		{
			var old = vsync;
			if (old != value)
			{
				vsync = value;
				Saved = false;
				VsyncChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler VsyncChanged;

	private int graphicsQuality;
	public int GraphicsQuality
	{
		get { return graphicsQuality; }
		set
		{
			var old = graphicsQuality;
			if (old != value)
			{
				graphicsQuality = value;
				Saved = false;
				GraphicsQualityChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler GraphicsQualityChanged;
	#endregion

	#region Audio
	[Range(0f, 1f)]
	private float masterVolume = 1;
	public float MasterVolume
	{
		get { return masterVolume; }
		set
		{
			var old = masterVolume;
			if (old != value)
			{
				masterVolume = value;
				Saved = false;
				MasterVolumeChanged?.Invoke(this, EventArgs.Empty);
				MasterSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MasterVolumeChanged;

	private bool masterMuted = false;
	public bool MasterMuted
	{
		get { return masterMuted; }
		set
		{
			var old = masterMuted;
			if (old != value)
			{
				masterMuted = value;
				Saved = false;
				MasterMutedChanged?.Invoke(this, EventArgs.Empty);
				MasterSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MasterMutedChanged;

	public float MasterSound { get => MasterMuted ? 0 : MasterVolume; }
	public event EventHandler MasterSoundChanged;
	
	[Range(0f, 1f)]
	private float effectsVolume = 1;
	public float EffectsVolume
	{
		get { return effectsVolume; }
		set
		{
			var old = effectsVolume;
			if (old != value)
			{
				effectsVolume = value;
				Saved = false;
				EffectsVolumeChanged?.Invoke(this, EventArgs.Empty);
				EffectsSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler EffectsVolumeChanged;

	private bool effectsMuted = false;
	public bool EffectsMuted
	{
		get { return effectsMuted; }
		set
		{
			var old = effectsMuted;
			if (old != value)
			{
				effectsMuted = value;
				Saved = false;
				EffectsMutedChanged?.Invoke(this, EventArgs.Empty);
				EffectsSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler EffectsMutedChanged;

	public float EffectsSound { get => EffectsMuted ? 0 : EffectsVolume; }
	public event EventHandler EffectsSoundChanged;

	[Range(0f, 1f)]
	private float musicVolume = 1;
	public float MusicVolume
	{
		get { return musicVolume; }
		set
		{
			var old = musicVolume;
			if (old != value)
			{
				musicVolume = value;
				Saved = false;
				MusicVolumeChanged?.Invoke(this, EventArgs.Empty);
				MusicSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MusicVolumeChanged;

	private bool musicMuted = false;
	public bool MusicMuted
	{
		get { return musicMuted; }
		set
		{
			var old = musicMuted;
			if (old != value)
			{
				musicMuted = value;
				Saved = false;
				MusicMutedChanged?.Invoke(this, EventArgs.Empty);
				MusicSoundChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler MusicMutedChanged;

	public float MusicSound { get => MusicMuted ? 0 : MusicVolume; }
	public event EventHandler MusicSoundChanged;
	#endregion

	#region Controls
	[Range(0.25f, 4f)]
	private float sensitivity = 1;
	public float Sensitivity
	{
		get { return sensitivity; }
		set
		{
			var old = sensitivity;
			if (old != value)
			{
				sensitivity = value;
				Saved = false;
				SensitivityChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler SensitivityChanged;
	#endregion

	public void Save(string filePath)
	{
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			sw.Write(JsonUtility.ToJson(this, true));
		}
		Saved = true;
	}

	public void Load(string filePath)
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

public enum Difficulty
{
	Easy = -1,
	Normal = 0,
	Hard = 1,
	Master = 2,
}
