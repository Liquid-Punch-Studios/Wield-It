﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class GameSettings
{
	[XmlIgnore]
	public readonly Dictionary<Language, string[]> displayModes = new Dictionary<Language, string[]>()
	{
		{Language.English , new string[] {"Fullscreen", "Windowed"} },
		{Language.Turkish , new string[] {"Tam Ekran" , "Pencere" } },
	};
	[XmlIgnore]
	public readonly Dictionary<Language, string> languages = new Dictionary<Language, string>()
	{
		{Language.English , "English"},
		{Language.Turkish , "Türkçe"},
	};

	[XmlIgnore]
	public readonly Dictionary<Language, string[]> difficulties = new Dictionary<Language, string[]>()
	{
		{Language.English , new string[] { "Easy", "Medium", "Hard" }},
		{Language.Turkish , new string[] { "Kolay", "Normal", "Zor" }},
	};

	[XmlIgnore]
	public readonly Dictionary<Language, string[]> qualities = new Dictionary<Language, string[]>()
	{
		{Language.English , new string[] { "Ultra", "Very High", "High", "Medium", "Low", "Very Low" } },
		{Language.Turkish , new string[] { "Üstün", "Çok Yüksek", "Yüksek", "Orta", "Düşük", "Çok Düşük" } },
	};

	private bool saved = false;
	[XmlIgnore]
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

	private Language language = Language.English;

	public Language Language
    {
        get { return language; }
        set 
		{
			var old = language;
			if(old != value)
            {
				language = value;
				Saved = false;
				MultiLanguage.changed = !MultiLanguage.changed;
			}
		}
    }

	public event EventHandler LanguageChanged;

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
	private float masterVolume = 1f;
	public float MasterVolume
	{
		get { return masterVolume; }
		set
		{
			var old = masterVolume;
			if (old != value)
			{
				masterVolume = Mathf.Clamp(value, 0f, 1f);
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
				effectsVolume = Mathf.Clamp(value, 0f, 1f);
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
	private float musicVolume = 0.5f;
	public float MusicVolume
	{
		get { return musicVolume; }
		set
		{
			var old = musicVolume;
			if (old != value)
			{
				musicVolume = Mathf.Clamp(value, 0f, 1f);
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
				sensitivity = Mathf.Clamp(value, 0.25f, 4f);
				Saved = false;
				SensitivityChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
	public event EventHandler SensitivityChanged;
	#endregion

	public void Save(string filePath)
	{
		XmlSerializer xs = new XmlSerializer(typeof(GameSettings));
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xs.Serialize(sw, this);
		}
		Saved = true;
	}

	public static GameSettings Load(string filePath)
	{
		XmlSerializer xs = new XmlSerializer(typeof(GameSettings));
		using (StreamReader sr = new StreamReader(filePath))
		{
			var obj = (GameSettings)xs.Deserialize(sr);
			obj.saved = true;
			return obj;
		}
	}
}

public enum Difficulty
{
	Easy,
	Normal,
	Hard,
}

public enum Language
{
	English,
	Turkish
}
