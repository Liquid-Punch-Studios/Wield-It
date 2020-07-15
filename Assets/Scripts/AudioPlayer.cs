using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
	public AudioSource[] audioList;
	public bool playOnAwake = false;

	private void Awake()
	{
		if (playOnAwake)
			PlayRandom();
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
}
