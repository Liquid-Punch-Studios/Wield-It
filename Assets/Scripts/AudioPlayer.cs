using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 
/// </summary>
public class AudioPlayer : MonoBehaviour
{
	public AudioSource[] audioList;

	public bool playOnAwake = false;

	public event System.EventHandler<int> Triggered;

	private void Awake()
	{
		if (playOnAwake)
			PlayRandom();
	}

	public void PlayRandom()
	{
		int index = Random.Range(0, audioList.Length);
		var audio = audioList[index];
		audio.Play(); // TODO: Check if sound replays or plays a new one
		Triggered?.Invoke(this, index);
	}

	public void Play(int audioIndex)
	{
		if (audioIndex < audioList.Length)
		{
			audioList[audioIndex].Play(); // TODO: Check if sound replays or plays a new one
			Triggered?.Invoke(this, audioIndex);
		}
		else
			Debug.LogError("Invalid audio index: " + audioIndex);
	}
}
