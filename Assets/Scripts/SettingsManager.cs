using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : Singleton<SettingsManager>
{
	public GameSettings Settings { get; private set; } = new GameSettings();
	public bool GraphicsApplied { get; private set; }
	public bool AudioApplied { get; private set; }

	public AudioMixer audioMixer;

	public string FilePath { get; private set; }

	public void ApplyGraphics()
	{
		if (GraphicsApplied)
			return;

		Screen.SetResolution(Settings.Resolution.width, Settings.Resolution.height, Settings.DisplayMode, Settings.Resolution.refreshRate);
		QualitySettings.SetQualityLevel(Settings.GraphicsQuality, true);

		GraphicsApplied = true;
	}

	private void Awake()
	{
		FilePath = Path.Combine(Application.persistentDataPath, "settings.xml");
		if (File.Exists(FilePath))
		{
			Settings = GameSettings.Load(FilePath);
			Debug.Log("Loaded settings from " + FilePath);
		}
	}

	private void OnDestroy()
	{
		Settings.Save(FilePath);
		Debug.Log("Saved settings to " + FilePath);
	}

	private void OnEnable()
	{
		Settings.GraphicsQuality = QualitySettings.GetQualityLevel();

		Settings.GraphicsQualityChanged += Settings_GraphicsQualityChanged;
		Settings.GraphicsQualityChanged += Settings_GraphicsChanged;
		Settings.ResolutionChanged		+= Settings_GraphicsChanged;
		Settings.DisplayModeChanged		+= Settings_GraphicsChanged;

		Settings.MasterSoundChanged		+= Settings_MasterSoundChanged;
		Settings.EffectsSoundChanged	+= Settings_EffectsSoundChanged;
		Settings.MusicSoundChanged		+= Settings_MusicSoundChanged;
	}

	private void OnDisable()
	{
		Settings.MusicSoundChanged		-= Settings_MusicSoundChanged;
		Settings.EffectsSoundChanged	-= Settings_EffectsSoundChanged;
		Settings.MasterSoundChanged		-= Settings_MasterSoundChanged;

		Settings.DisplayModeChanged		-= Settings_GraphicsChanged;
		Settings.ResolutionChanged		-= Settings_GraphicsChanged;
		Settings.GraphicsQualityChanged -= Settings_GraphicsChanged;
		Settings.GraphicsQualityChanged -= Settings_GraphicsQualityChanged;
	}

	private void Settings_GraphicsQualityChanged(object sender, EventArgs e)
	{
		QualitySettings.SetQualityLevel(Settings.GraphicsQuality, false);
	}

	private void Settings_GraphicsChanged(object sender, EventArgs e)
	{
		GraphicsApplied = false;
	}

	private float ToDecibel(float value)
	{
		return value * 80 - 80;
	}

	private void Settings_MasterSoundChanged(object sender, EventArgs e)
	{
		audioMixer.SetFloat("MasterVolume", ToDecibel(Settings.MasterSound));
	}

	private void Settings_EffectsSoundChanged(object sender, EventArgs e)
	{
		audioMixer.SetFloat("EffectsVolume", ToDecibel(Settings.EffectsSound));
	}

	private void Settings_MusicSoundChanged(object sender, EventArgs e)
	{
		audioMixer.SetFloat("MusicVolume", ToDecibel(Settings.MusicSound));
	}
}
