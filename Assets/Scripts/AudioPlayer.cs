using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
	public enum AudioType // your custom enumeration
	{
		Music,
		Sound,
	};
	public AudioType audioType;

	public AudioSource[] audioList;
	public bool playOnAwake = false;

	public static bool load = false;

	private void Awake()
	{
		LoadVolume();
		if (playOnAwake)
			PlayRandom();
	}

    private void FixedUpdate()
    {
        if (load)
			LoadVolume();
    }

    public void PlayRandom()
	{
		var audio = audioList[Random.Range(0, audioList.Length)];
		//if (!audio.isPlaying)
			audio.Play();
	}

	public void Play(int audioIndex)
	{
		if (audioIndex < audioList.Length)
			audioList[audioIndex].Play();
		else
			Debug.LogError("Invalid audio index: " + audioIndex);
	}

	public void LoadVolume()
    {
		SettingsData settings = SaveSystem.LoadSettings();
		foreach(AudioSource audio in audioList)
        {
			if (audioType == AudioType.Music)
				audio.volume = settings.musicVal * 0.2f / 100f;
			if (audioType == AudioType.Sound)
				audio.volume = settings.soundVal * 2 / 100f;
		}
		
    }
}
