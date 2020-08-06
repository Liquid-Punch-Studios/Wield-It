using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
	public AudioSource[] audioList;
	public bool playOnAwake = false;

	public event System.EventHandler<int> AudioPlaying;

	private void Awake()
	{
		if (playOnAwake)
			PlayRandom();
	}

    public void PlayRandom()
	{
		int index = Random.Range(0, audioList.Length);
		var audio = audioList[index];
		//if (!audio.isPlaying) // TODO: Check if sound replays or plays a new one
			audio.Play();
		AudioPlaying?.Invoke(this, index);
	}

	public void Play(int audioIndex)
	{
		if (audioIndex < audioList.Length)
		{
			audioList[audioIndex].Play(); // TODO: Check if sound replays or plays a new one
			AudioPlaying?.Invoke(this, audioIndex);
		}
		else
			Debug.LogError("Invalid audio index: " + audioIndex);
	}
}
